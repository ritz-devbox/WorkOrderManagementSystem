namespace WorkOrderManagementSystem.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    // Hierarchical support
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; } = new();
}
