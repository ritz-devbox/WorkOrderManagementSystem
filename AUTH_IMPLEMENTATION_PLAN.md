# Authentication System Implementation Plan

## 🎯 **New Architecture Overview**

### **Three Separate Portals:**

1. **Public Portal** (`/`) - No login required
   - Anyone can create work orders
   - Track work order status by ID
   - Simple, public-facing interface

2. **Worker Portal** (`/worker`) - Login required
   - Username/password authentication
   - View assigned work orders
   - Update status to complete
   - Add comments and updates

3. **Admin Portal** (`/admin`) - Login required
   - Username/password authentication
   - Full system management
   - Assign work orders to workers
   - View analytics and reports
   - Manage worker accounts

---

## 🔐 **Authentication System**

### **Database Schema:**

```csharp
WorkerAccount
├── Id (int)
├── Username (string) - unique
├── PasswordHash (string) - SHA256 hashed
├── FullName (string)
├── Email (string)
├── Phone (string)
├── Role (enum: Worker | Admin)
├── IsActive (bool)
└── CreatedAt (DateTime)
```

### **Default Accounts (Seeded):**

| Username | Password | Role | Full Name |
|----------|----------|------|-----------|
| `admin` | `admin123` | Admin | System Administrator |
| `worker1` | `worker123` | Worker | John Worker |

---

## 📋 **Implementation Steps**

### ✅ **Completed:**
1. Created `WorkerAccount` model
2. Created `AuthService` for password hashing
3. Added `WorkerAccounts` DbSet to context
4. Seeded default admin and worker accounts
5. Registered `AuthService` in DI

### 🔨 **Next Steps:**

#### **Step 1: Create Login Pages**
- [ ] `/worker/login` - Worker login page
- [ ] `/admin/login` - Admin login page
- [ ] Session management using browser storage

#### **Step 2: Create Portal Layouts**
- [ ] `WorkerLayout.razor` - Layout for worker portal
- [ ] `AdminLayout.razor` - Layout for admin portal
- [ ] `PublicLayout.razor` - Layout for public portal

#### **Step 3: Reorganize Pages**
- [ ] `/` - Public home (create work order)
- [ ] `/track/{id}` - Track work order status
- [ ] `/worker/dashboard` - Worker dashboard
- [ ] `/worker/orders` - Worker's assigned orders
- [ ] `/admin/dashboard` - Admin dashboard with analytics
- [ ] `/admin/orders` - All work orders with assignment
- [ ] `/admin/workers` - Manage worker accounts

#### **Step 4: Implement Session Management**
- [ ] Create session state service
- [ ] Store logged-in user info
- [ ] Auto-logout on session expire
- [ ] Protect routes with authentication guards

#### **Step 5: Update Work Order Flow**
- [ ] Public users create orders (no login)
- [ ] System generates tracking ID
- [ ] Admin assigns to worker
- [ ] Worker receives notification
- [ ] Worker completes and updates
- [ ] Public user can track by ID

---

## 🎨 **UI/UX Design**

### **Public Portal:**
```
┌─────────────────────────────────────┐
│  Work Order System                  │
│  ─────────────────────────────────  │
│                                     │
│  Create a Work Order                │
│  ┌─────────────────────────────┐   │
│  │ Description                 │   │
│  │ Location                    │   │
│  │ Category                    │   │
│  └─────────────────────────────┘   │
│  [Submit]                           │
│                                     │
│  Track Your Order                   │
│  ┌─────────────┐ [Track]           │
│  │ Order ID    │                   │
│  └─────────────┘                   │
└─────────────────────────────────────┘
```

### **Worker Portal:**
```
┌─────────────────────────────────────┐
│  Worker Portal - John Worker    [↗] │
│  ─────────────────────────────────  │
│  My Assigned Orders (5)             │
│  ┌─────────────────────────────┐   │
│  │ #123 - AC not working       │   │
│  │ Building A | Critical       │   │
│  │ [Mark Complete]             │   │
│  └─────────────────────────────┘   │
└─────────────────────────────────────┘
```

### **Admin Portal:**
```
┌─────────────────────────────────────┐
│  Admin Portal - Dashboard       [↗] │
│  ─────────────────────────────────  │
│  [Analytics] [Orders] [Workers]     │
│                                     │
│  Unassigned Orders (3)              │
│  ┌─────────────────────────────┐   │
│  │ #124 - Plumbing issue       │   │
│  │ Assign to: [Select Worker▼]│   │
│  │ [Assign]                    │   │
│  └─────────────────────────────┘   │
└─────────────────────────────────────┘
```

---

## 🔄 **User Flows**

### **Flow 1: Public User Creates Order**
1. Visit `/`
2. Fill out work order form (no login)
3. Submit
4. Receive tracking ID: `WO-2024-001`
5. Can track status at `/track/WO-2024-001`

### **Flow 2: Admin Assigns Order**
1. Login at `/admin/login`
2. View unassigned orders
3. Select worker from dropdown
4. Click "Assign"
5. Worker gets notification

### **Flow 3: Worker Completes Order**
1. Login at `/worker/login`
2. View assigned orders
3. Click on order to see details
4. Add comment/update
5. Mark as complete

---

## 🛡️ **Security Considerations**

### **Password Security:**
- ✅ SHA256 hashing
- ✅ No plaintext storage
- 🔄 TODO: Add salt for extra security
- 🔄 TODO: Implement password reset

### **Session Security:**
- 🔄 TODO: Session tokens
- 🔄 TODO: Timeout after 30 minutes
- 🔄 TODO: CSRF protection

### **Route Protection:**
- 🔄 TODO: Redirect unauthorized users
- 🔄 TODO: Role-based route guards

---

## 📊 **Benefits of New System**

### **For Public Users:**
- ✅ No registration required
- ✅ Quick work order submission
- ✅ Easy status tracking
- ✅ Privacy (no account needed)

### **For Workers:**
- ✅ Dedicated portal
- ✅ See only assigned tasks
- ✅ Simple workflow
- ✅ Mobile-friendly

### **For Admins:**
- ✅ Complete oversight
- ✅ Easy task assignment
- ✅ Analytics dashboard
- ✅ Worker management

---

## 🚀 **Migration Path**

### **Option 1: Fresh Start (Recommended)**
```bash
.\stop.bat
del workorders.db
.\start.bat
```
- Clean database with new schema
- Default accounts ready
- No data migration needed

### **Option 2: Keep Existing Data**
- More complex
- Requires data migration script
- Not recommended for development

---

## 📝 **Next Actions**

Would you like me to:

1. **Implement the login pages** and session management?
2. **Create the three portal layouts** (Public, Worker, Admin)?
3. **Reorganize existing pages** into the new structure?
4. **Build the complete authentication flow** end-to-end?

Let me know which approach you prefer, and I'll implement it! 🎯
