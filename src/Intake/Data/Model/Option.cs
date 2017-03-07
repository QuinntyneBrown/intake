using Intake.Data.Helpers;
using System.Collections.Generic;

namespace Intake.Data.Model
{
    public class Option
    {
        [SoftDelete("IsDeleted")]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public int OrderIndex { get; set; }
        public bool IsDeleted { get; set; }
    }
}
