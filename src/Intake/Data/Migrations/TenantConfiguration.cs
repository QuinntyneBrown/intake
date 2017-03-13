using System.Data.Entity.Migrations;
using Intake.Data;
using Intake.Data.Model;

namespace Intake.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(IntakeContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
