using System.Collections.Generic;
using Intake.Data.Helpers;

namespace Intake.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? OrderIndex { get; set; }
        public ICollection<Option> Options { get; set; } = new HashSet<Option>();
        public bool IsDeleted { get; set; }
    }
}
