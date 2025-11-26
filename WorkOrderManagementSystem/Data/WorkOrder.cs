using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class WorkOrder
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    // Foreign key to Category
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a description.")]
    public string Description { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a location.")]
    public string Location { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.New;
    public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Medium;
    
    // Task Assignment
    public string? AssignedTo { get; set; }
    public DateTime? AssignedDate { get; set; }
    
    public DateTime? CompletedDate { get; set; }
    
    // Digital Signature
    public string? SignatureData { get; set; }
    
    // Asset Tracking (QR Code)
    public int? AssetId { get; set; }
    public Asset? Asset { get; set; }
    
    // SLA Tracking
    public DateTime? SLADeadline { get; set; }
    public bool IsEscalated { get; set; } = false;
}

public enum WorkOrderStatus
{
    New,
    InProgress,
    Completed,
    Cancelled
}

public enum WorkOrderPriority
{
    Low,
    Medium,
    High,
    Critical
}
