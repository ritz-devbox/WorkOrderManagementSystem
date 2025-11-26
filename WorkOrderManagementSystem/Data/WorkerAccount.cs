using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class WorkerAccount
{
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Role Role { get; set; } = Role.Worker;
    
    // Skills
    public List<WorkerSkill> WorkerSkills { get; set; } = new();
}
