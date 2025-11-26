using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagementSystem.Data;
using Xunit;

namespace WorkOrderManagementSystem.Tests;

public class WorkOrderTests
{
 private WorkOrderContext CreateInMemoryContext()
 {
 var options = new DbContextOptionsBuilder<WorkOrderContext>()
 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
 .Options;
 return new WorkOrderContext(options);
 }

 [Fact]
 public async Task CanAddAndRetrieveWorkOrder()
 {
 using var db = CreateInMemoryContext();
 // seed categories
 db.Categories.Add(new Category { Id =1, Name = "Electrical" });
 db.Categories.Add(new Category { Id =2, Name = "Plumbing" });
 await db.SaveChangesAsync();

 var wo = new WorkOrder
 {
 Username = "TESTUSR",
 CategoryId =1,
 Location = "Test Location",
 Description = "Test Description",
 Timestamp = DateTime.UtcNow
 };

 db.WorkOrders.Add(wo);
 await db.SaveChangesAsync();

 var saved = await db.WorkOrders.Include(w => w.Category).FirstOrDefaultAsync(w => w.Id == wo.Id);
 Assert.NotNull(saved);
 Assert.Equal("TESTUSR", saved!.Username);
 Assert.Equal(1, saved.CategoryId);
 Assert.Equal("Test Location", saved.Location);
 Assert.Equal("Test Description", saved.Description);
 }
}
