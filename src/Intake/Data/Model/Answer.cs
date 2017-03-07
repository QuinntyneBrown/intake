using Intake.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intake.Data.Model
{
    public class Answer
    {
        [SoftDelete("IsDeleted")]
        public int Id { get; set; }
        [ForeignKey("Option")]
        public int? OptionId { get; set; }
        public virtual Option Option { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
