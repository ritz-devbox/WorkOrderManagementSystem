using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public enum Role
{
    User,
    Worker,
    Admin
}

public class UserRole
{
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    public Role Role { get; set; } = Role.User;
}
