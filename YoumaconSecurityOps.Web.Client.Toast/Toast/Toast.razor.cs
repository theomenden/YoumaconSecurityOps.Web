using System;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Web.Client.Toast.Core;

namespace YoumaconSecurityOps.Web.Client.Toast.Toast
{
    public partial class Toast : ComponentBase, IDisposable
    {
        [CascadingParameter] private Toasts ToastsContainer { get; set; }
        [Parameter] public Guid ToastId { get; set; }
        [Parameter] public ToastSettings ToastSettings { get; set; }
        [Parameter] public Int32 Timeout { get; set; }

        private const Double SecondsToMinutesConversion = 1.00 / 60.00;

        private CountdownTimer _countdownTimer;

        private readonly DateTime _startTime = DateTime.Now;

        private Int32 _progress = 100;

        private String _durationMessage;
        private bool disposedValue;

        protected override void OnInitialized()
        {
            _countdownTimer = new CountdownTimer(Timeout);

            _countdownTimer.OnTick += CalculateProgress;

            _countdownTimer.OnElapsed += Close;

            _countdownTimer.Start();
        }

        private async void CalculateProgress(Int32 percentComplete)
        {
            _progress = 100 - percentComplete;

            _durationMessage = GetDurationMessage();

            await InvokeAsync(StateHasChanged);
        }

        private String GetDurationMessage()
        {
            var timeToDisplay = _countdownTimer.TimeRemaining;

            return _progress > 95 ? "just now" : TimeConversion(timeToDisplay);
        }

        private String TimeConversion(Int32 time)
        {
            var timeSinceInitialized = (int)Math.Truncate((_startTime.AddSeconds(time) - _startTime).TotalSeconds);

            if (timeSinceInitialized > 120)
            {
                return $"{_countdownTimer.TimeRemaining * SecondsToMinutesConversion} minutes ago";
            }

            if (timeSinceInitialized >= 30)
            {
                return "a few moments ago";
            }

            return $"{_countdownTimer.TimeRemaining} seconds ago";

        }

        private void Close()
        {
            ToastsContainer.RemoveToast(ToastId);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                _countdownTimer.Dispose();
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
