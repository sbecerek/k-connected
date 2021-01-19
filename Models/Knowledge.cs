using System;
using System.Collections.Generic;

#nullable disable

namespace k_connected.API.Models
{
    public partial class Knowledge
    {
        public int KnowledgeId { get; set; }
        public string Username { get; set; }
        public string SkillName { get; set; }

        public virtual Skill SkillNameNavigation { get; set; }
        public virtual Entity UsernameNavigation { get; set; }
    }
}
