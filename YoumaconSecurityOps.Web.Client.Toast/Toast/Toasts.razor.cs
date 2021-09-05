using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using YoumaconSecurityOps.Web.Client.Toast.Core;

namespace YoumaconSecurityOps.Web.Client.Toast.Toast
{
    public partial class Toasts : ComponentBase
    {
        #region Injected Fields
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        #endregion

        #region Toast Configuration Parameters
        [Parameter] public string DiceRollClass { get; set; }
        [Parameter] public string DiceIcon { get; set; }
        [Parameter] public string InfoClass { get; set; }
        [Parameter] public string InfoIcon { get; set; }
        [Parameter] public string SuccessClass { get; set; }
        [Parameter] public string SuccessIcon { get; set; }
        [Parameter] public string WarningClass { get; set; }
        [Parameter] public string WarningIcon { get; set; }
        [Parameter] public string ErrorClass { get; set; }
        [Parameter] public string ErrorIcon { get; set; }
        [Parameter] public ToastPosition Position { get; set; } = ToastPosition.TopRight;
        [Parameter] public int Timeout { get; set; } = 5;
        [Parameter] public bool RemoveToastsOnNavigation { get; set; }
        [Parameter] public bool ShowProgressBar { get; set; }
        #endregion

        private string PositionClass { get; set; } = string.Empty;

        internal IList<ToastInstance> ToastList { get; set; } = new List<ToastInstance>();

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;

            if (RemoveToastsOnNavigation)
            {
                NavigationManager.LocationChanged += ClearToasts;
            }

            PositionClass = $"position-{Position.ToString().ToLower()}";
        }

        public void RemoveToast(Guid toastId)
        {
            InvokeAsync(() =>
            {
                var toastInstance = ToastList.SingleOrDefault(x => x.Id == toastId);

                ToastList.Remove(toastInstance);

                StateHasChanged();
            });
        }

        #region Private Methods
        private void ClearToasts(object sender, LocationChangedEventArgs args)
        {
            InvokeAsync(() =>
            {
                ToastList.Clear();

                StateHasChanged();
            });
        }

        private ToastSettings BuildToastSettings(ToastLevel level, RenderFragment message, string heading)
        {
            return level switch
            {
                ToastLevel.Error => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Error" : heading, message,
                    "bg-danger", ErrorClass, ErrorIcon, ShowProgressBar),
                ToastLevel.Info => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Info" : heading, message,
                    "bg-info", InfoClass, InfoIcon, ShowProgressBar),
                ToastLevel.Success => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Success" : heading,
                    message, "bg-success", SuccessClass, SuccessIcon, ShowProgressBar),
                ToastLevel.Warning => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Warning" : heading,
                    message, "bg-warning", WarningClass, WarningIcon, ShowProgressBar),
                ToastLevel.DiceRoll => new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Dice Roll Result" : heading,
                    message, "bg-dark", DiceRollClass, DiceIcon, ShowProgressBar),
                _ => throw new InvalidOperationException()
            };
        }

        private void ShowToast(ToastLevel level, RenderFragment message, string heading)
        {
            InvokeAsync(() =>
            {
                var settings = BuildToastSettings(level, message, heading);

                var toast = new ToastInstance()
                {
                    Id = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    ToastSettings = settings
                };

                ToastList.Add(toast);

                StateHasChanged();
            });
        }
        #endregion
    }
}
