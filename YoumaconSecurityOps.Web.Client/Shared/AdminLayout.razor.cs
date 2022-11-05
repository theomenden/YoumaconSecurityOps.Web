using Microsoft.AspNetCore.Authorization;

namespace YoumaconSecurityOps.Web.Client.Shared;

[Authorize("Administrator")]
public partial class AdminLayout: LayoutComponentBase
{
    private bool topbarVisible = false;
}
