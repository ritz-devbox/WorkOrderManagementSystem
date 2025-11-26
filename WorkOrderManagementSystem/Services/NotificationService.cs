using WorkOrderManagementSystem.Data;

namespace WorkOrderManagementSystem.Services;

public class NotificationService
{
    public event Action<string, string, NotificationType>? OnNotification;
    
    // Send notification
    public void SendNotification(string title, string message, NotificationType type = NotificationType.Info)
    {
        OnNotification?.Invoke(title, message, type);
        
        // In a real app, you'd also:
        // - Send email via SMTP
        // - Send SMS via Twilio
        // - Push to mobile app
        // - Log to database
        
        Console.WriteLine($"[{type}] {title}: {message}");
    }
    
    // Specific notification types
    public void NotifyWorkerAssigned(WorkOrder workOrder, string workerName)
    {
        SendNotification(
            "New Assignment",
            $"{workerName} has been assigned to: {workOrder.Description}",
            NotificationType.Success
        );
    }
    
    public void NotifySLABreach(WorkOrder workOrder)
    {
        SendNotification(
            "SLA BREACH!",
            $"Work Order #{workOrder.Id} has exceeded its SLA deadline!",
            NotificationType.Error
        );
    }
    
    public void NotifySLAWarning(WorkOrder workOrder, int minutesRemaining)
    {
        SendNotification(
            "SLA Warning",
            $"Work Order #{workOrder.Id} - Only {minutesRemaining} minutes left!",
            NotificationType.Warning
        );
    }
    
    public void NotifyWorkOrderCompleted(WorkOrder workOrder)
    {
        SendNotification(
            "Work Order Completed",
            $"#{workOrder.Id}: {workOrder.Description} has been completed",
            NotificationType.Success
        );
    }
    
    public void NotifyNewWorkOrder(WorkOrder workOrder)
    {
        SendNotification(
            "New Work Order",
            $"#{workOrder.Id}: {workOrder.Description} - Priority: {workOrder.Priority}",
            NotificationType.Info
        );
    }
}

public enum NotificationType
{
    Info,
    Success,
    Warning,
    Error
}
