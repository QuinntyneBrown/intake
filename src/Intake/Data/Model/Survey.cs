using System.Collections.Generic;
using Intake.Data.Helpers;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public bool IsDeleted { get; set; }
    }
}
