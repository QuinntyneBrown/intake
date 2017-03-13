using System.Collections.Generic;
using Intake.Data.Helpers;
using static Intake.Constants;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Survey: ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        [Index("NameIndex", IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
