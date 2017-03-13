using System.Data.Entity.Migrations;
using Intake.Data;
using Intake.Data.Model;
using System.Linq;
using Intake.Security;
using System.Collections.Generic;

namespace Intake.Migrations
{
    public class UserConfiguration
    {
        public static void Seed(IntakeContext context) {

            var systemRole = context.Roles.First(x => x.Name == Roles.SYSTEM);
            var roles = new List<Role>();
            var tenant = context.Tenants.Single(x => x.Name == "Default");

            roles.Add(systemRole);
            context.Users.AddOrUpdate(x => x.Username, new User()
            {
                Username = "system",
                Password = new EncryptionService().TransformPassword("system"),
                Roles = roles,
                TenantId = tenant.Id
            });

            context.SaveChanges();
        }
    }
}
