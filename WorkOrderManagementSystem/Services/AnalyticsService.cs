using Microsoft.EntityFrameworkCore;
using WorkOrderManagementSystem.Data;

namespace WorkOrderManagementSystem.Services
{
    public class AnalyticsDashboardData
    {
        public int TotalWorkOrders { get; set; }
        public int PendingWorkOrders { get; set; }
        public int CompletedWorkOrders { get; set; }
        public int CriticalWorkOrders { get; set; }
        public double CompletionRate { get; set; }
        public TimeSpan AverageResolutionTime { get; set; }
        public List<CategoryStat> OrdersByCategory { get; set; } = new();
        public List<PriorityStat> OrdersByPriority { get; set; } = new();
        public List<WorkerStat> TopWorkers { get; set; } = new();
        public List<DailyStat> OrdersLast7Days { get; set; } = new();
    }

    public class CategoryStat
    {
        public string CategoryName { get; set; } = "";
        public int Count { get; set; }
    }

    public class PriorityStat
    {
        public WorkOrderPriority Priority { get; set; }
        public int Count { get; set; }
    }

    public class WorkerStat
    {
        public string WorkerName { get; set; } = "";
        public int CompletedCount { get; set; }
    }

    public class DailyStat
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class AnalyticsService
    {
        private readonly WorkOrderContext _context;

        public AnalyticsService(WorkOrderContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsDashboardData> GetDashboardDataAsync()
        {
            var data = new AnalyticsDashboardData();

            // 1. High Level KPIs
            var allOrders = await _context.WorkOrders
                .Include(w => w.Category)
                .ToListAsync();

            data.TotalWorkOrders = allOrders.Count;
            data.PendingWorkOrders = allOrders.Count(w => w.Status != WorkOrderStatus.Completed);
            data.CompletedWorkOrders = allOrders.Count(w => w.Status == WorkOrderStatus.Completed);
            data.CriticalWorkOrders = allOrders.Count(w => w.Priority == WorkOrderPriority.Critical && w.Status != WorkOrderStatus.Completed);

            if (data.TotalWorkOrders > 0)
            {
                data.CompletionRate = (double)data.CompletedWorkOrders / data.TotalWorkOrders * 100;
            }

            // 2. Average Resolution Time (for completed orders)
            var completedOrders = allOrders
                .Where(w => w.Status == WorkOrderStatus.Completed && w.CompletedDate.HasValue)
                .ToList();

            if (completedOrders.Any())
            {
                var totalDuration = completedOrders
                    .Select(w => w.CompletedDate!.Value - w.Timestamp)
                    .Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal.Add(t));
                
                data.AverageResolutionTime = totalDuration / completedOrders.Count;
            }

            // 3. Orders by Category
            data.OrdersByCategory = allOrders
                .GroupBy(w => w.Category?.Name ?? "Uncategorized")
                .Select(g => new CategoryStat { CategoryName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // 4. Orders by Priority
            data.OrdersByPriority = allOrders
                .GroupBy(w => w.Priority)
                .Select(g => new PriorityStat { Priority = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Priority) // Critical first usually
                .ToList();

            // 5. Top Workers
            data.TopWorkers = allOrders
                .Where(w => w.Status == WorkOrderStatus.Completed && !string.IsNullOrEmpty(w.AssignedTo))
                .GroupBy(w => w.AssignedTo!)
                .Select(g => new WorkerStat { WorkerName = g.Key, CompletedCount = g.Count() })
                .OrderByDescending(x => x.CompletedCount)
                .Take(5)
                .ToList();

            // 6. Last 7 Days Trend
            var sevenDaysAgo = DateTime.Now.Date.AddDays(-6);
            data.OrdersLast7Days = allOrders
                .Where(w => w.Timestamp >= sevenDaysAgo)
                .GroupBy(w => w.Timestamp.Date)
                .Select(g => new DailyStat { Date = g.Key, Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            // Fill in missing days with 0
            for (int i = 0; i < 7; i++)
            {
                var date = sevenDaysAgo.AddDays(i);
                if (!data.OrdersLast7Days.Any(d => d.Date == date))
                {
                    data.OrdersLast7Days.Add(new DailyStat { Date = date, Count = 0 });
                }
            }
            data.OrdersLast7Days = data.OrdersLast7Days.OrderBy(x => x.Date).ToList();

            return data;
        }
    }
}
