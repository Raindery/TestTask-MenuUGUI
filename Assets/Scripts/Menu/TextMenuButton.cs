using Zenject;

public class TextMenuButton : MenuButton
{
    private ScreenText _screenText;
    private IErrorHandlingService _errorHandlingService;
    private string _textValue;

    
    [Inject]
    private void Construct(ScreenText screenText, IErrorHandlingService errorHandlingService)
    {
        _screenText = screenText;
        _errorHandlingService = errorHandlingService;
    }

    
    protected override void OnClick()
    {
        try
        {
            _screenText.ChangeText(_textValue);
        }
        catch
        {
            _errorHandlingService.ShowError();
        }
    }

    public void SetData(string textValue)
    {
        _textValue = textValue;
        Text.text = textValue;
    }
}