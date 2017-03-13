using System.Data.Entity.Migrations;
using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Users;

namespace Intake.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(IntakeContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
