using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YoumaconSecurityOps.Data.EntityFramework.Extensions;
using YoumaconSecurityOps.Data.EntityFramework.Models;

namespace YoumaconSecurityOps.Data.EntityFramework.Context
{
    public partial class YoumaconSecurityDbContext
    {
        private YoumaconSecurityDbContextProcedures _procedures;

        public YoumaconSecurityDbContextProcedures Procedures
        {
            get => _procedures ??= new YoumaconSecurityDbContextProcedures(this);
            set => _procedures = value;
        }

        public YoumaconSecurityDbContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public partial class YoumaconSecurityDbContextProcedures
    {
        private readonly YoumaconSecurityDbContext _context;

        public YoumaconSecurityDbContextProcedures(YoumaconSecurityDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> AddInitialStaffRolesAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterReturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                parameterReturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[p_AddStaffRoles]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterReturnValue.Value);

            return _;
        }

        public virtual async Task<int> AddInitialStaffTypesAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterReturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                parameterReturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[p_AddStaffTypes]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterReturnValue.Value);

            return _;
        }

        public virtual async Task<List<p_AddStartingLocationsResult>> AddInitialLocationsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterReturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                parameterReturnValue,
            };
            var _ = await _context.SqlQueryAsync<p_AddStartingLocationsResult>("EXEC @returnValue = [dbo].[p_AddStartingLocations]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterReturnValue.Value);

            return _;
        }
    }
}
