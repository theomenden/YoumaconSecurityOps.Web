#pragma checksum "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "93205c5335f03ace76dd0e8ba64cb355910ac4c3"
// <auto-generated/>
#pragma warning disable 1591
namespace YoumaconSecurityOps.Web.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Icons;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Icons.FontAwesome;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.DataGrid;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Animate;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.SpinKit;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Blazorise.Snackbar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Web.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Web.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Web.Client.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Core.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Core.Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Core.Shared.Models.Readers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\_Imports.razor"
using YoumaconSecurityOps.Core.Shared.Parameters;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Blazorise.Jumbotron>(0);
            __builder.AddAttribute(1, "Background", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.Background>(
#nullable restore
#line 2 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor"
                               Background.Dark

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Border", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.IFluentBorder>(
#nullable restore
#line 2 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor"
                                                        Border.Rounded

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(3, "TextColor", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.TextColor>(
#nullable restore
#line 2 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor"
                                                                                   TextColor.Light

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "Margin", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.IFluentSpacing>(
#nullable restore
#line 2 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor"
                                                                                                            Margin.Is2.FromTop.OnDesktop.Is1.FromTop.OnMobile

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<Blazorise.JumbotronTitle>(6);
                __builder2.AddAttribute(7, "Size", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.JumbotronTitleSize>(
#nullable restore
#line 3 "C:\Users\andre\source\repos\YoumaconSecurityOps.Web.BlazorServer\YoumaconSecurityOps.Web.Client\Pages\Index.razor"
                                  JumbotronTitleSize.Is4

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(8, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(9, "Hello, Security Staff!");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(10, "\r\n            ");
                __builder2.OpenComponent<Blazorise.JumbotronSubtitle>(11);
                __builder2.AddAttribute(12, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(13, "\r\n                Welcome to Sec Ops!\r\n            ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(14, "\r\n            ");
                __builder2.OpenComponent<Blazorise.Divider>(15);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(16, "\r\n            ");
                __builder2.OpenComponent<Blazorise.Paragraph>(17);
                __builder2.AddAttribute(18, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(19, "\r\n                Pick any navigation on the side-menu to get started\r\n            ");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
