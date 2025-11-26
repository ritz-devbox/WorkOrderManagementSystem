using WorkOrderManagementSystem.Data;

namespace WorkOrderManagementSystem.Services;

public class SmartTriageService
{
    // AI-Powered Priority Detection
    public WorkOrderPriority PredictPriority(string description)
    {
        var desc = description.ToLower();
        
        // Critical keywords
        if (desc.Contains("fire") || desc.Contains("smoke") || desc.Contains("gas") || 
            desc.Contains("flood") || desc.Contains("emergency") || desc.Contains("danger"))
            return WorkOrderPriority.Critical;
            
        // High priority keywords
        if (desc.Contains("leak") || desc.Contains("broken") || desc.Contains("not working") ||
            desc.Contains("urgent") || desc.Contains("asap"))
            return WorkOrderPriority.High;
            
        // Low priority keywords
        if (desc.Contains("cosmetic") || desc.Contains("minor") || desc.Contains("when available"))
            return WorkOrderPriority.Low;
            
        return WorkOrderPriority.Medium;
    }
    
    // AI-Powered Category Detection
    public int? PredictCategory(string description, List<Category> categories)
    {
        var desc = description.ToLower();
        
        foreach (var category in categories)
        {
            var catName = category.Name.ToLower();
            
            // Direct match
            if (desc.Contains(catName))
                return category.Id;
                
            // Keyword matching
            if (catName == "electrical" && (desc.Contains("light") || desc.Contains("power") || 
                desc.Contains("outlet") || desc.Contains("wire")))
                return category.Id;
                
            if (catName == "plumbing" && (desc.Contains("water") || desc.Contains("leak") || 
                desc.Contains("pipe") || desc.Contains("drain") || desc.Contains("toilet")))
                return category.Id;
                
            if (catName == "hvac" && (desc.Contains("heat") || desc.Contains("cool") || 
                desc.Contains("ac") || desc.Contains("air") || desc.Contains("temperature")))
                return category.Id;
        }
        
        return null;
    }
    
    // Calculate SLA Deadline based on priority
    public DateTime CalculateSLADeadline(WorkOrderPriority priority, DateTime createdDate)
    {
        return priority switch
        {
            WorkOrderPriority.Critical => createdDate.AddHours(2),   // 2 hours
            WorkOrderPriority.High => createdDate.AddHours(24),      // 1 day
            WorkOrderPriority.Medium => createdDate.AddDays(3),      // 3 days
            WorkOrderPriority.Low => createdDate.AddDays(7),         // 1 week
            _ => createdDate.AddDays(3)
        };
    }
    
    // Check if SLA is breached
    public bool IsSLABreached(DateTime? slaDeadline)
    {
        if (slaDeadline == null) return false;
        return DateTime.UtcNow > slaDeadline.Value;
    }
    
    // Get time remaining for SLA
    public TimeSpan GetTimeRemaining(DateTime? slaDeadline)
    {
        if (slaDeadline == null) return TimeSpan.Zero;
        var remaining = slaDeadline.Value - DateTime.UtcNow;
        return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
    }
    
    // Smart Worker Recommendation
    public string? RecommendWorker(WorkOrder workOrder, List<WorkerAccount> workers)
    {
        // Filter by skills matching the category
        var eligibleWorkers = workers.Where(w => w.IsActive).ToList();
        
        if (!eligibleWorkers.Any()) return null;
        
        // Simple round-robin for now (can be enhanced with workload balancing)
        var random = new Random();
        return eligibleWorkers[random.Next(eligibleWorkers.Count)].Username;
    }
}
