using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string youmaDbConnectionString)
        {

            services.AddDbContext<YoumaconSecurityDbContext>(options => options.UseSqlServer(youmaDbConnectionString));

            services
                .AddScoped<IContactAccessor, ContactRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<IShiftAccessor, ShiftRepository>()
                .AddScoped<IShiftRepository, ShiftRepository>()
                .AddScoped<IStaffAccessor, StaffRepository>()
                .AddScoped<IStaffRepository, StaffRepository>()
                .AddScoped<ILocationAccessor, LocationRepository>()
                .AddScoped<ILocationRepository, LocationRepository>();

            return services;
        }
    }

}
