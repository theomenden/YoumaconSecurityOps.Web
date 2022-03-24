namespace YoumaconSecurityOps.Web.Client.Components;

public partial class ThemeChangeComponent: ComponentBase
{

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (value.Equals(_value))
            {
                return;
            }
            _value = value;

            InvokeAsync(StateHasChanged);

            ValueChanged.InvokeAsync(_selectedTheme?.Name ?? value);
        }
    }
    
    private ThemeChoice _selectedTheme;

    private string _value;

    private Task OnClick(ThemeChoice value)
    {
        _selectedTheme = value;
        
        Value = value.Name;

        return Task.CompletedTask;
    }
}