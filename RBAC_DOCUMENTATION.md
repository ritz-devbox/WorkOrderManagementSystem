# Work Order Management System - Role-Based Access Control (RBAC)

## 🎭 User Roles & Permissions

### 1. **👤 Normal Users (Default Role)**
**Purpose**: Create and track their own work orders

**Permissions**:
- ✅ Create new work orders
- ✅ View their own work orders only
- ✅ Add comments to their work orders
- ❌ Cannot edit status or priority
- ❌ Cannot assign tasks
- ❌ Cannot access Analytics
- ❌ Cannot manage users
- ❌ Cannot export reports

**UI Behavior**:
- Status and Priority fields are hidden in the form
- Can only see work orders they created
- Edit/Delete buttons are disabled
- Navigation shows: Home, Work Orders only

---

### 2. **🔧 Workers**
**Purpose**: Complete assigned tasks and update work order status

**Permissions**:
- ✅ View all work orders
- ✅ View work orders assigned to them
- ✅ Update Status (New → In Progress → Completed)
- ✅ Update Priority
- ✅ Add comments
- ✅ Access Analytics Dashboard (limited view)
- ❌ Cannot delete work orders
- ❌ Cannot manage categories
- ❌ Cannot manage users
- ❌ Cannot export reports (Admin only)

**UI Behavior**:
- Can see and modify Status/Priority fields
- Can edit work orders (but not delete)
- See all work orders in the list
- Navigation shows: Home, Work Orders, Analytics

---

### 3. **👑 Admin**
**Purpose**: Full system management and oversight

**Permissions**:
- ✅ **Full CRUD** on work orders
- ✅ Create, Edit, Delete work orders
- ✅ Manage categories
- ✅ Assign tasks to workers
- ✅ Change status and priority
- ✅ Access **Analytics Dashboard** with full metrics
- ✅ **Export Reports** (CSV & HTML)
- ✅ Manage user permissions
- ✅ View all work orders
- ✅ Add/Edit/Delete comments

**UI Behavior**:
- Full access to all features
- Can manage categories
- Export buttons visible
- Navigation shows: Home, Work Orders, Analytics, Manage Permissions
- User management page accessible

---

## 🔐 Access Control Implementation

### Database Structure
```csharp
public class UserRole
{
    public int Id { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; } // Admin, Worker, or User
}

public enum Role
{
    User,    // Default - everyone
    Worker,  // Assigned by Admin
    Admin    // Full access
}
```

### Default Behavior
- **Everyone** can access the system by default as a `User`
- Only users in the `UserRoles` table have elevated privileges
- Current Windows user is seeded as Admin on first run

### Task Assignment
```csharp
public class WorkOrder
{
    // ... other properties
    public string? AssignedTo { get; set; }      // Worker username
    public DateTime? AssignedDate { get; set; }  // When assigned
}
```

---

## 📊 Feature Access Matrix

| Feature | User | Worker | Admin |
|---------|------|--------|-------|
| Create Work Orders | ✅ | ✅ | ✅ |
| View Own Orders | ✅ | ✅ | ✅ |
| View All Orders | ❌ | ✅ | ✅ |
| Edit Status/Priority | ❌ | ✅ | ✅ |
| Delete Orders | ❌ | ❌ | ✅ |
| Manage Categories | ❌ | ❌ | ✅ |
| Assign Tasks | ❌ | ❌ | ✅ |
| Analytics Dashboard | ❌ | ✅* | ✅ |
| Export Reports | ❌ | ❌ | ✅ |
| Manage Users | ❌ | ❌ | ✅ |

*Workers have limited analytics view

---

## 🎯 Workflow Example

### Scenario: Broken HVAC System

1. **User (John)** creates a work order:
   - Description: "AC not working in Room 301"
   - Location: "Building A, 3rd Floor"
   - Category: HVAC
   - ❌ Cannot set priority or status

2. **Admin (Sarah)** reviews and assigns:
   - Sets Priority: Critical
   - Sets Status: New
   - Assigns to Worker: "Mike (HVAC Technician)"
   - AssignedDate: 2025-11-19

3. **Worker (Mike)** receives assignment:
   - Views assigned task in Work Orders
   - Updates Status: In Progress
   - Adds comment: "Checking compressor"
   - Later updates Status: Completed
   - Adds comment: "Replaced faulty capacitor"

4. **Admin (Sarah)** monitors:
   - Views Analytics Dashboard
   - Sees completion metrics
   - Exports monthly report
   - Reviews worker performance

---

## 🚀 Navigation & UI

### User View
```
├── Home (Dashboard - basic metrics)
└── Work Orders (own orders only)
```

### Worker View
```
├── Home (Dashboard - basic metrics)
├── Work Orders (all orders, can update)
└── Analytics (performance metrics)
```

### Admin View
```
├── Home (Dashboard - full metrics)
├── Work Orders (full CRUD + export)
├── Analytics (complete dashboard)
└── Manage Permissions (user roles)
```

---

## 💡 Key Features

### For Users
- Simple, focused interface
- Easy work order creation
- Track personal requests
- Comment on own orders

### For Workers
- Task assignment notifications
- Status update workflow
- Performance tracking
- View workload

### For Admins
- Complete oversight
- Advanced analytics
- Report generation
- User management
- Category management
- Task assignment

---

## 🔧 Technical Implementation

### Role Checking Pattern
```csharp
protected override async Task OnInitializedAsync()
{
    var username = WindowsIdentity.GetCurrent()?.Name;
    var userRole = await Db.UserRoles
        .FirstOrDefaultAsync(u => u.Username == username);
    currentRole = userRole?.Role ?? Role.User;
}
```

### Conditional UI Rendering
```razor
@if (currentRole == Role.Admin)
{
    <button @onclick="DeleteWorkOrder">Delete</button>
}

@if (currentRole != Role.User)
{
    <InputSelect @bind-Value="workOrder.Status">
        <!-- Status options -->
    </InputSelect>
}
```

### Navigation Guard
```razor
@if (currentRole != Role.Admin)
{
    <div>Access Restricted</div>
    return;
}
<!-- Admin-only content -->
```

---

## 📈 Benefits

1. **Security**: Proper access control prevents unauthorized actions
2. **Clarity**: Each role has clear responsibilities
3. **Efficiency**: Workers focus on tasks, admins on management
4. **Audit Trail**: Track who did what and when
5. **Scalability**: Easy to add new roles or permissions
6. **User Experience**: Simplified UI for each role type

---

## 🎨 Visual Indicators

- **Role Badges** in navigation (color-coded)
- **Admin**: Red gradient badge with shield icon
- **Worker**: Blue gradient badge with wrench icon
- **User**: Gray gradient badge with person icon

---

This RBAC system provides enterprise-grade access control while maintaining a clean, intuitive user experience for all user types! 🚀
