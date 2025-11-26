using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class WorkOrderHistory
{
    public int Id { get; set; }
    
    public int WorkOrderId { get; set; }
    
    [Required]
    public string Action { get; set; } = string.Empty;
    
    public string? OldValue { get; set; }
    
    public string? NewValue { get; set; }
    
    public string ChangedBy { get; set; } = string.Empty;
    
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    
    public string? Notes { get; set; }
    
    // Navigation property
    public WorkOrder? WorkOrder { get; set; }
}
