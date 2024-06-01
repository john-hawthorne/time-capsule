using System.ComponentModel.DataAnnotations;

namespace TimeCapsule.Entities.Models;

public class TaskType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
