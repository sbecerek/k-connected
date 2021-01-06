using System.ComponentModel.DataAnnotations;

namespace k_connected.API.Models
{
    public class Skill
    {

        public Skill()
        {
            
        }
        public Skill(string param)
        {
            Name = param;
        }

        [Key]
        public string Name { get; set; }
    }
}