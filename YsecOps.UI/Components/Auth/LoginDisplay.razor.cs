using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using YsecOps.UI.Extensions;

namespace YsecOps.UI.Components.Auth;

public partial class LoginDisplay : ComponentBase
{
    [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }
    [Inject] private UserManager<ApplicationUser> UserManager { get; init; }
    [Inject] private IUserService UserService { get; init; }

    private AuthenticationState? _authState;
    private string? _userDisplayName;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _authState = await AuthenticationStateTask.ConfigureAwait(false);

        var (success, userId) = _authState.User.TryGetUserId();

        if (success)
        {
            var contextUser = await UserService.GetUserViewModelAsync(userId);

            _userDisplayName = contextUser?.Email ?? "Guest";
        }

        _userDisplayName ??= _authState.User.Identity?.Name;
    }

    private Boolean IsAdminRole() => _authState?.User.IsInRole("Administrator") ?? false;

}