using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    public class Role
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public virtual Tenant Tenant { get; set; }
    }
}
