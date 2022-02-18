using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Components
{
    public partial class ErrorComponent : ComponentBase
    {
        [Parameter] public ApiResponse ApiError { get; set; }
    }
}
