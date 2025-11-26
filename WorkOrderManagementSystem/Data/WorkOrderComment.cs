using System;
using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class WorkOrderComment
{
    public int Id { get; set; }
    
    public int WorkOrderId { get; set; }
    public WorkOrder? WorkOrder { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;
    
    public string Username { get; set; } = string.Empty;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
