using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class InspectionRecord
{
    public int Id { get; set; }

    public int InspectionTemplateId { get; set; }
    public InspectionTemplate? InspectionTemplate { get; set; }

    public string WorkerUsername { get; set; } = string.Empty;

    public DateTime DateStarted { get; set; } = DateTime.UtcNow;
    public DateTime? DateCompleted { get; set; }

    // For Phase 2: Asset Tagging
    public string? AssetBarcode { get; set; }

    public List<InspectionResult> Results { get; set; } = new();
}
