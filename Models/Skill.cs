using System;
using System.Collections.Generic;

#nullable disable

namespace k_connected.API.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Knowledges = new HashSet<Knowledge>();
        }

        public string SkillName { get; set; }

        public virtual ICollection<Knowledge> Knowledges { get; set; }
    }
}
