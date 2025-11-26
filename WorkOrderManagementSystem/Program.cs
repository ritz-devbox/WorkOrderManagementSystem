using WorkOrderManagementSystem.Components;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagementSystem.Data;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add EF Core Sqlite DbContext
builder.Services.AddDbContext<WorkOrderContext>(options =>
    options.UseSqlite("Data Source=workorders.db"));

// Add custom services
builder.Services.AddScoped<WorkOrderManagementSystem.Services.ExportService>();
builder.Services.AddScoped<WorkOrderManagementSystem.Services.AuthService>();
builder.Services.AddScoped<WorkOrderManagementSystem.Services.SessionService>();
builder.Services.AddScoped<WorkOrderManagementSystem.Services.FileService>();
builder.Services.AddScoped<WorkOrderManagementSystem.Services.SmartTriageService>();
builder.Services.AddScoped<WorkOrderManagementSystem.Services.QRCodeService>();
builder.Services.AddSingleton<WorkOrderManagementSystem.Services.NotificationService>();

// Add Negotiate (Windows) authentication
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

var app = builder.Build();

// Ensure database created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorkOrderContext>();
    db.Database.EnsureCreated();

    // Explicitly seed data if missing (fixes issue where EnsureCreated doesn't seed if DB exists but is empty/old)
    if (!db.WorkerAccounts.Any())
    {
        Console.WriteLine("Seeding WorkerAccounts...");
        var authService = new WorkOrderManagementSystem.Services.AuthService();
        
        db.WorkerAccounts.AddRange(
            new WorkerAccount 
            { 
                Username = "admin", 
                PasswordHash = authService.HashPassword("admin123"),
                FullName = "System Administrator",
                Email = "admin@workorder.local",
                Role = Role.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new WorkerAccount 
            { 
                Username = "worker1", 
                PasswordHash = authService.HashPassword("worker123"),
                FullName = "John Worker",
                Email = "worker@workorder.local",
                Role = Role.Worker,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );
        db.SaveChanges();
        Console.WriteLine("WorkerAccounts seeded successfully.");
    }
    else
    {
        Console.WriteLine("WorkerAccounts already exist. Skipping seed.");
    }

    if (!db.Categories.Any())
    {
        Console.WriteLine("Seeding Categories...");
        db.Categories.AddRange(
            new Category { Name = "Electrical" },
            new Category { Name = "Plumbing" },
            new Category { Name = "HVAC" },
            new Category { Name = "General Maintenance" },
            new Category { Name = "IT Support" }
        );
        db.SaveChanges();
        Console.WriteLine("Categories seeded successfully.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
