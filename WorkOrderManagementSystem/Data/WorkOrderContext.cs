using Microsoft.EntityFrameworkCore;

namespace WorkOrderManagementSystem.Data;

public class WorkOrderContext : DbContext
{
    public WorkOrderContext(DbContextOptions<WorkOrderContext> options) : base(options) { }

    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<WorkerAccount> WorkerAccounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<WorkOrderAttachment> WorkOrderAttachments { get; set; }
    public DbSet<WorkOrderComment> WorkOrderComments { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<WorkOrderHistory> WorkOrderHistories { get; set; }
    
    // Inspection Module
    public DbSet<InspectionTemplate> InspectionTemplates { get; set; }
    public DbSet<InspectionItem> InspectionItems { get; set; }
    public DbSet<InspectionRecord> InspectionRecords { get; set; }
    public DbSet<InspectionResult> InspectionResults { get; set; }
    

    // Master Data - Locations
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    // Master Data - Skills
    public DbSet<Skill> Skills { get; set; }
    public DbSet<WorkerSkill> WorkerSkills { get; set; }
    
    // Assets & QR Codes
    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electrical" },
            new Category { Id = 2, Name = "Plumbing" },
            new Category { Id = 3, Name = "HVAC" }
        );

        // Seed default admin and worker accounts
        // Password for admin: "admin123"
        // Password for worker: "worker123"
        var authService = new WorkOrderManagementSystem.Services.AuthService();
        
        modelBuilder.Entity<WorkerAccount>().HasData(
            new WorkerAccount 
            { 
                Id = 1, 
                Username = "admin", 
                PasswordHash = authService.HashPassword("admin123"),
                FullName = "System Administrator",
                Email = "admin@workorder.local",
                Role = Role.Admin,
                IsActive = true
            },
            new WorkerAccount 
            { 
                Id = 2, 
                Username = "worker1", 
                PasswordHash = authService.HashPassword("worker123"),
                FullName = "John Worker",
                Email = "worker@workorder.local",
                Role = Role.Worker,
                IsActive = true
            }
        );
    }
}
