using Microsoft.EntityFrameworkCore.Design;

namespace YSecOps.Data.EfCore.Contexts;
internal class YSecOpsDbFactory : IDesignTimeDbContextFactory<YoumaconSecurityOpsContext>
{
    private const string CONNECTION_STRING = "Server=(local);Initial Catalog=YsecOpsDb;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;";

    public YoumaconSecurityOpsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<YoumaconSecurityOpsContext>();
        optionsBuilder.UseSqlServer(CONNECTION_STRING)
            .EnableServiceProviderCaching();

        return new YoumaconSecurityOpsContext(optionsBuilder.Options);
    }
}
