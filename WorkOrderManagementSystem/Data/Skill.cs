using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class Skill
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Many-to-many with WorkerAccount
    public List<WorkerSkill> WorkerSkills { get; set; } = new();
}
