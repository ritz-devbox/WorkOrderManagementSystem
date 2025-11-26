using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class InspectionResult
{
    public int Id { get; set; }

    public int InspectionRecordId { get; set; }
    public InspectionRecord? InspectionRecord { get; set; }

    public int InspectionItemId { get; set; }
    public InspectionItem? InspectionItem { get; set; }

    public bool? IsPass { get; set; } // For PassFail type
    public string? TextValue { get; set; } // For Text type
    public double? NumberValue { get; set; } // For Number type

    public string Notes { get; set; } = string.Empty;
}
