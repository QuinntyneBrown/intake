using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    public class Project
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Benefit { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ProjectSponsor")]
        public int? ProjectSponserId { get; set; }
        [ForeignKey("ProjectRequestor")]
        public int? ProjectRequestorId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ProjectSponsor ProjectSponsor { get; set; }
        public virtual ProjectRequestor ProjectRequestor { get; set; }

    }
}
