using Intake.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Response: ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        [ForeignKey("Option")]
        public int? OptionId { get; set; }        
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Option Option { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
