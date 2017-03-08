using System.Collections.Generic;
using Intake.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Question
    {
        public int Id { get; set; }
        [ForeignKey("Survey")]
        public int? SurveyId { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? OrderIndex { get; set; }
        public ICollection<Option> Options { get; set; } = new HashSet<Option>();
        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
