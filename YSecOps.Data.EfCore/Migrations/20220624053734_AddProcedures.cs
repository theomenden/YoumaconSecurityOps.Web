using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
#nullable disable

namespace YSecOps.Data.EfCore.Migrations
{
    public partial class AddProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var sqlFiles = assembly.GetManifestResourceNames().
                Where(file => file.EndsWith(".sql"));
            
            foreach (var sqlFile in sqlFiles)
            {
                using var stream = assembly.GetManifestResourceStream(sqlFile);
                using var reader = new StreamReader(stream);
                var sqlScript = reader.ReadToEnd();
                migrationBuilder.Sql(sqlScript);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
