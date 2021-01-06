using System;
using System.Collections.Generic;

namespace k_connected.API.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Knowledge = new HashSet<Knowledge>();
        }

        public string SkillName { get; set; }

        public virtual ICollection<Knowledge> Knowledge { get; set; }
    }
}
