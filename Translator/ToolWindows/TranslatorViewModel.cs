using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Translator.Tools;

namespace Translator.ToolWindows;

public class TranslatorViewModel : INotifyPropertyChanged
{
    private string _sourceLanguage;
    private string _targetLanguage;

    public TranslatorViewModel()
    {
        Translate = new RelayCommand(OnTranslate);

        List<string> supportedLanguages = LibreTranslator
            .SupportedLanguages
            .Select(l => l.Name)
            .ToList();

        _sourceLanguage = supportedLanguages[0];
        _targetLanguage = supportedLanguages[1];

        SourceLanguages = new ObservableCollection<string>(supportedLanguages);
        TargetLanguages = new ObservableCollection<string>(supportedLanguages);
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<string> SourceLanguages { get; }

    public ObservableCollection<string> TargetLanguages { get; }

    public string SourceLanguage
    {
        get => _sourceLanguage;
        set
        {
            if (_targetLanguage == value)
                SetProperty(ref _targetLanguage, _sourceLanguage, nameof(TargetLanguage));
            
            SetProperty(ref _sourceLanguage, value);
        }
    }

    public string TargetLanguage
    {
        get => _targetLanguage;
        set
        {
            if (_sourceLanguage == value)
                SetProperty(ref _sourceLanguage, _targetLanguage, nameof(SourceLanguages));

            SetProperty(ref _targetLanguage, value);
        }
    }

    public ICommand Translate { get; }

    private void OnTranslate(object obj)
    {
        var commandArgument = $"{_sourceLanguage} {_targetLanguage}";
        VS.Commands.ExecuteAsync(PackageGuids.Translator, PackageIds.Translate, commandArgument).FireAndForget();
    }

    private void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (Equals(field, newValue))
            return;
        
        field = newValue;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}