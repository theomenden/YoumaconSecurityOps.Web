using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Org.BouncyCastle.Asn1.Cmp;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public interface IDirectoryReadingService
    {
        IAsyncEnumerable<ImageInformationHolder> GetAllImagesAsync(CancellationToken cancellationToken);
    }
}
