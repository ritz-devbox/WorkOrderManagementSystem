using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class Room
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int FloorId { get; set; }
    public Floor? Floor { get; set; }
}
