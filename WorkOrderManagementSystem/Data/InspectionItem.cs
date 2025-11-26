using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class InspectionItem
{
    public int Id { get; set; }

    public int InspectionTemplateId { get; set; }
    public InspectionTemplate? InspectionTemplate { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public InspectionItemType Type { get; set; } = InspectionItemType.PassFail;
}

public enum InspectionItemType
{
    PassFail,
    Text,
    Number
}
