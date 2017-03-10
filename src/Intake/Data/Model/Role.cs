using Intake.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Role: ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public virtual Tenant Tenant { get; set; }
    }
}
