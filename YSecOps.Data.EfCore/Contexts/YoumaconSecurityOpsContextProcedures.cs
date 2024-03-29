﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;

namespace YSecOps.Data.EfCore.Contexts;

public partial class YoumaconSecurityOpsContext
{
    private IYoumaconSecurityOpsContextProcedures _procedures;

    public virtual IYoumaconSecurityOpsContextProcedures Procedures
    {
        get
        {
            if (_procedures is null) _procedures = new YoumaconSecurityOpsContextProcedures(this);
            return _procedures;
        }
        set
        {
            _procedures = value;
        }
    }

    public IYoumaconSecurityOpsContextProcedures GetProcedures()
    {
        return Procedures;
    }

    protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<p_AddStartingLocationsResult>().HasNoKey().ToView(null);
    }
}

public partial class YoumaconSecurityOpsContextProcedures : IYoumaconSecurityOpsContextProcedures
{
    private readonly YoumaconSecurityOpsContext _context;

    public YoumaconSecurityOpsContextProcedures(YoumaconSecurityOpsContext context)
    {
        _context = context;
    }

    public virtual async Task<int> p_AddPronounsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new []
        {
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[p_AddPronouns]", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public virtual async Task<int> p_AddStaffRolesAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new []
        {
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[p_AddStaffRoles]", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public virtual async Task<int> p_AddStaffTypesAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new []
        {
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[p_AddStaffTypes]", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public virtual async Task<List<p_AddStartingLocationsResult>> p_AddStartingLocationsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new []
        {
            parameterreturnValue,
        };
        var _ = await _context.SqlQueryAsync<p_AddStartingLocationsResult>("EXEC @returnValue = [dbo].[p_AddStartingLocations]", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
}