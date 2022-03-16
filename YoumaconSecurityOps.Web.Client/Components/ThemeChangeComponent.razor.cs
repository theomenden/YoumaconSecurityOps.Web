using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

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
            if (value == _value)
            {
                return;
            }
            _value = value;

            InvokeAsync(StateHasChanged);

            ValueChanged.InvokeAsync(value);
        }
    }

    private string ClassNames(string value) => $"theme-color-table-cell{(value == Value ? " selected" : "")}";


    private string _value;

    private Task OnClick(string value)
    {
        Value = value;

        return Task.CompletedTask;
    }
}