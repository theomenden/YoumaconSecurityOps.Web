using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public sealed class StaffService : IStaffService
    {
        private readonly IApiService _client;

        public StaffService(IApiService client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<IEnumerable<StaffReader>> GetRaces()
        {
            try
            {
                var races = await _client.GetContentAsync<IEnumerable<StaffReader>>("Staff");

                return races;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
