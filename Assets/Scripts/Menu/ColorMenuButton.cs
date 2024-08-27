using Zenject;

public class ColorMenuButton : MenuButton
{
    private ScreenText _screenText;
    private IErrorHandlingService _errorHandlingService;
    private Color _color;
    
    
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
            _screenText.ChangeColor(_color.ColorValue);
        }
        catch
        {
            _errorHandlingService.ShowError();
        }
    }

    public void SetData(Color color)
    {
        _color = color;
        Text.text = color.Name;
    }
}