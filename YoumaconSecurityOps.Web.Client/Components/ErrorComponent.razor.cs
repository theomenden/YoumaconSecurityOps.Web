using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Components
{
    public partial class ErrorComponent : ComponentBase
    {
        [Parameter] public ApiResponse ApiError { get; set; }
    }
}
