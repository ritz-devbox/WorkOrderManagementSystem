using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class WorkOrderAttachment
{
    public int Id { get; set; }
    
    public int WorkOrderId { get; set; }
    
    [Required]
    public string FileName { get; set; } = string.Empty;
    
    [Required]
    public string FilePath { get; set; } = string.Empty;
    
    public string ContentType { get; set; } = string.Empty;
    
    public long FileSize { get; set; }
    
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    public string UploadedBy { get; set; } = string.Empty;
    
    // Navigation property
    public WorkOrder? WorkOrder { get; set; }
}
