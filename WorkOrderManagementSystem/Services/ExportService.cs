using WorkOrderManagementSystem.Data;
using System.Text;

namespace WorkOrderManagementSystem.Services;

public class ExportService
{
    public byte[] ExportToCSV(List<WorkOrder> workOrders)
    {
        var sb = new StringBuilder();
        
        // Header
        sb.AppendLine("ID,Description,Category,Location,Status,Priority,Username,Created Date");
        
        // Data
        foreach (var wo in workOrders)
        {
            sb.AppendLine($"{wo.Id},\"{wo.Description}\",\"{wo.Category?.Name}\",\"{wo.Location}\",{wo.Status},{wo.Priority},\"{wo.Username}\",{wo.Timestamp:yyyy-MM-dd HH:mm}");
        }
        
        return Encoding.UTF8.GetBytes(sb.ToString());
    }
    
    public string GenerateHTMLReport(List<WorkOrder> workOrders, Dictionary<string, object> metrics)
    {
        var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Work Order Report</title>
    <style>
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 40px; background: #f5f5f5; }}
        .header {{ background: linear-gradient(135deg, #6366f1 0%, #818cf8 100%); color: white; padding: 30px; border-radius: 10px; margin-bottom: 30px; }}
        .metrics {{ display: grid; grid-template-columns: repeat(4, 1fr); gap: 20px; margin-bottom: 30px; }}
        .metric-card {{ background: white; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .metric-value {{ font-size: 32px; font-weight: bold; color: #6366f1; }}
        .metric-label {{ color: #64748b; font-size: 14px; margin-top: 5px; }}
        table {{ width: 100%; border-collapse: collapse; background: white; border-radius: 10px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        th {{ background: #6366f1; color: white; padding: 15px; text-align: left; }}
        td {{ padding: 12px 15px; border-bottom: 1px solid #e2e8f0; }}
        tr:hover {{ background: #f8fafc; }}
        .badge {{ padding: 5px 12px; border-radius: 20px; font-size: 12px; font-weight: 600; }}
        .badge-new {{ background: #dbeafe; color: #1e40af; }}
        .badge-progress {{ background: #dbeafe; color: #0369a1; }}
        .badge-completed {{ background: #d1fae5; color: #065f46; }}
        .badge-critical {{ background: #fee2e2; color: #991b1b; }}
        .badge-high {{ background: #fed7aa; color: #9a3412; }}
        .badge-medium {{ background: #fef3c7; color: #92400e; }}
        .badge-low {{ background: #e0e7ff; color: #3730a3; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Work Order Management Report</h1>
        <p>Generated on {DateTime.Now:MMMM dd, yyyy 'at' HH:mm}</p>
    </div>
    
    <div class='metrics'>
        <div class='metric-card'>
            <div class='metric-value'>{metrics.GetValueOrDefault("total", 0)}</div>
            <div class='metric-label'>Total Orders</div>
        </div>
        <div class='metric-card'>
            <div class='metric-value'>{metrics.GetValueOrDefault("completed", 0)}</div>
            <div class='metric-label'>Completed</div>
        </div>
        <div class='metric-card'>
            <div class='metric-value'>{metrics.GetValueOrDefault("inProgress", 0)}</div>
            <div class='metric-label'>In Progress</div>
        </div>
        <div class='metric-card'>
            <div class='metric-value'>{metrics.GetValueOrDefault("critical", 0)}</div>
            <div class='metric-label'>Critical</div>
        </div>
    </div>
    
    <table>
        <thead>
            <tr>
                <th>ID</th>
                <th>Description</th>
                <th>Category</th>
                <th>Location</th>
                <th>Status</th>
                <th>Priority</th>
                <th>Created By</th>
                <th>Date</th>
            </tr>
        </thead>
        <tbody>";

        foreach (var wo in workOrders)
        {
            var statusBadge = wo.Status switch
            {
                WorkOrderStatus.New => "badge-new",
                WorkOrderStatus.InProgress => "badge-progress",
                WorkOrderStatus.Completed => "badge-completed",
                _ => "badge-new"
            };
            
            var priorityBadge = wo.Priority switch
            {
                WorkOrderPriority.Critical => "badge-critical",
                WorkOrderPriority.High => "badge-high",
                WorkOrderPriority.Medium => "badge-medium",
                WorkOrderPriority.Low => "badge-low",
                _ => "badge-medium"
            };

            html += $@"
            <tr>
                <td>#{wo.Id}</td>
                <td>{wo.Description}</td>
                <td>{wo.Category?.Name}</td>
                <td>{wo.Location}</td>
                <td><span class='badge {statusBadge}'>{wo.Status}</span></td>
                <td><span class='badge {priorityBadge}'>{wo.Priority}</span></td>
                <td>{wo.Username}</td>
                <td>{wo.Timestamp:MMM dd, yyyy}</td>
            </tr>";
        }

        html += @"
        </tbody>
    </table>
</body>
</html>";

        return html;
    }
}
