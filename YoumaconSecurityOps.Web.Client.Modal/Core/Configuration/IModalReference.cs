using System.Threading.Tasks;

namespace YoumaconSecurityOps.Web.Client.Modal.Core.Configuration
{
    public interface IModalReference
    {
        Task<ModalResult> Result { get; }

        void Close();

        void Close(ModalResult result);
    }
}
