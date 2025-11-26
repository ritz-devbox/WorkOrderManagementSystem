using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class Floor
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int BuildingId { get; set; }
    public Building? Building { get; set; }

    public List<Room> Rooms { get; set; } = new();
}
