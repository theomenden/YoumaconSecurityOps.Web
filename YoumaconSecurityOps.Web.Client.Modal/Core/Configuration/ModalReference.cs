using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Modal.Core.Configuration
{
    public class ModalReference : IModalReference
    {
        private readonly TaskCompletionSource<ModalResult> _resultCompletion = new();

        private readonly Action<ModalResult> _closed;

        private readonly ModalService _modalService;

        public ModalReference(Guid modalInstanceId, RenderFragment modalInstance, ModalService modalService)
        {
            Id = modalInstanceId;

            ModalInstance = modalInstance;

            _closed = HandleClosed;

            _modalService = modalService;
        }

        internal Guid Id { get; }

        internal RenderFragment ModalInstance { get; }

        public Task<ModalResult> Result => _resultCompletion.Task;

        public void Close()
        {
            _modalService.Close(this);
        }

        public void Close(ModalResult result)
        {
            _modalService.Close(this, result);
        }

        private void HandleClosed(ModalResult obj)
        {
            _ = _resultCompletion.TrySetResult(obj);
        }

        internal void Dismiss(ModalResult result)
        {
            _closed.Invoke(result);
        }
    }
}
