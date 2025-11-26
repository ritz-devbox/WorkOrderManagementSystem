using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class Asset
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string QRCode { get; set; } = string.Empty; // Unique identifier

    public string? Description { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? LocationBuildingId { get; set; }
    public Building? LocationBuilding { get; set; }

    public int? LocationFloorId { get; set; }
    public Floor? LocationFloor { get; set; }

    public int? LocationRoomId { get; set; }
    public Room? LocationRoom { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastMaintenanceDate { get; set; }

    // Repair history
    public List<WorkOrder> WorkOrders { get; set; } = new();
    public List<InspectionRecord> InspectionRecords { get; set; } = new();
}
