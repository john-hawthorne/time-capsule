using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCapsule.Entities.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
