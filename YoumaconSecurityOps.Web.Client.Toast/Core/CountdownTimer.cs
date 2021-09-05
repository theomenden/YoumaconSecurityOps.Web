using System;
using System.Timers;

namespace YoumaconSecurityOps.Web.Client.Toast.Core
{
    internal class CountdownTimer : IDisposable
    {
        #region Timer Configuration Fields
        private Timer _timer;

        private readonly int _timeout;

        private int _percentComplete;

        private double _timeElapsed;

        private DateTime? _startTime;
        #endregion

        #region Timer Actions
        internal Action<int> OnTick;

        internal Action OnElapsed;
        private bool disposedValue;
        #endregion

        internal CountdownTimer(int timeout)
        {
            _timeout = timeout * 10;

            _percentComplete = 0;

            SetupTimer();
        }

        public int TimeRemaining => GetTimeRemaining();

        internal void Start()
        {
            _timer.Start();
            _startTime ??= DateTime.Now;
        }

        private void SetupTimer()
        {
            _timer = new Timer(_timeout);

            _timer.Elapsed += HandleTick;

            _timer.AutoReset = false;
        }

        private void HandleTick(object sender, ElapsedEventArgs args)
        {
            _percentComplete++;

            GetTimeRemaining();

            OnTick?.Invoke(_percentComplete);

            if (_percentComplete == 100)
            {
                OnElapsed?.Invoke();
            }
            else
            {
                SetupTimer();
                Start();
            }
        }

        private int GetTimeRemaining()
        {

            _timeElapsed = (DateTime.Now - _startTime.GetValueOrDefault(DateTime.Now)).TotalMilliseconds * 0.001;

            return (int)Math.Truncate(_timeElapsed);
        }

        #region IDisposable Implementation
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                _timer.Dispose();

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CountdownTimer()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
