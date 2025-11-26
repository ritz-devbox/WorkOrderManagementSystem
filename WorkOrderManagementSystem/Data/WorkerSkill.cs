namespace WorkOrderManagementSystem.Data;

public class WorkerSkill
{
    public int Id { get; set; }

    public int WorkerAccountId { get; set; }
    public WorkerAccount? WorkerAccount { get; set; }

    public int SkillId { get; set; }
    public Skill? Skill { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
}
