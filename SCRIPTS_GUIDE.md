# Work Order Management System - Helper Scripts

## 🚀 Quick Start Scripts

I've created three helper scripts to make development easier:

### 1. **`start.bat`** - Clean Start (Recommended)
```bash
start.bat
```
**What it does:**
- ✅ Kills any running instances
- ✅ Builds the application
- ✅ Starts the application
- ✅ Shows clear status messages

**Use this when:** Starting fresh or after code changes

---

### 2. **`stop.bat`** - Stop Application
```bash
stop.bat
```
**What it does:**
- ✅ Kills the running application process
- ✅ Shows confirmation message

**Use this when:** You need to stop the app quickly

---

### 3. **`restart.bat`** - Quick Restart
```bash
restart.bat
```
**What it does:**
- ✅ Stops the application
- ✅ Immediately restarts it
- ✅ No rebuild (faster)

**Use this when:** Testing without code changes

---

## 💡 Usage Tips

### **Development Workflow:**

1. **First time / After code changes:**
   ```bash
   start.bat
   ```

2. **Quick testing iterations:**
   ```bash
   restart.bat
   ```

3. **When done:**
   ```bash
   stop.bat
   ```
   Or just press `Ctrl+C` in the terminal

---

## 🔧 Manual Commands

If you prefer manual control:

```bash
# Kill the process
taskkill /F /IM WorkOrderManagementSystem.exe

# Build
dotnet build

# Run
dotnet run

# Build and Run
dotnet run --no-build
```

---

## ⚠️ Common Issues

### **Port Already in Use**
**Problem:** `Failed to bind to address http://127.0.0.1:5135`

**Solution:**
```bash
stop.bat
# Wait 2 seconds
start.bat
```

### **Database Schema Errors**
**Problem:** `SQLite Error: no such column`

**Solution:**
```bash
stop.bat
del workorders.db
start.bat
```

---

## 🎯 Testing Different Roles

1. Run the application:
   ```bash
   start.bat
   ```

2. Navigate to: `http://localhost:5135/roleswitch`

3. Click role buttons to test different permissions

4. Use `restart.bat` between tests if needed

---

## 📝 Notes

- Scripts automatically handle process cleanup
- `start.bat` includes build step for safety
- `restart.bat` is faster but doesn't rebuild
- All scripts show clear status messages
- Press `Ctrl+C` to stop the server gracefully

---

**Happy Testing! 🚀**
