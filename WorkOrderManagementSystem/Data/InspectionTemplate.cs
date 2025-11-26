using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class InspectionTemplate
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public InspectionFrequency Frequency { get; set; } = InspectionFrequency.OnDemand;

    public List<InspectionItem> Items { get; set; } = new();
}

public enum InspectionFrequency
{
    OnDemand,
    Daily,
    Weekly,
    Monthly,
    Yearly
}
