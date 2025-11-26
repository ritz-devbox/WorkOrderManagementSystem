# 🏗️ Design Proposal: Inspections & Smart Categories

## 1. Hierarchical Categories (The Foundation)
As requested, we need to split broad categories into specific subtypes.
*   **Structure**: `Parent` -> `Child`
    *   *Example*: `Electrical` -> `Fire Alarms`, `Lighting`, `Generators`
    *   *Example*: `Plumbing` -> `Restrooms`, `Piping`, `Sprinklers`
*   **UI**: An Admin page to build this tree structure visually.

## 2. Smart Inspections (The Feature)
Instead of just "fixing" things, we "prevent" them.
*   **Templates**: Admins create checklists linked to Categories (e.g., "Monthly Fire Alarm Test").
*   **The "Runner"**: A mobile-friendly view for workers to tap "Pass", "Fail", or "N/A".
*   **⚡ The Creative Spark (Auto-Triage)**:
    *   If a worker marks "Fail" on "Battery Check", the system **automatically**:
        1.  Creates a new Work Order.
        2.  Categorizes it as "Electrical > Fire Alarms".
        3.  Sets Priority to "High".
        4.  Links it to the Inspection for audit trails.

## 3. Discussion Points
*   **Trigger**: Do we want **QR Codes** to trigger these inspections? (e.g. Scan door -> Open "Door Inspection" form).
*   **Scheduling**: Should inspections be **scheduled** (appear on dashboard automatically every Monday) or **on-demand**?
