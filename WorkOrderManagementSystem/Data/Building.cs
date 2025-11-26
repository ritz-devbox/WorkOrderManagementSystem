using System.ComponentModel.DataAnnotations;

namespace WorkOrderManagementSystem.Data;

public class Building
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public List<Floor> Floors { get; set; } = new();
}
