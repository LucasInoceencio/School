namespace School.Domain.Entities.Core;

public class Entity
{
    public int Id { get; set; }
    public DateTime DateHourRegister { get; set; }
    public string RegisteredBy { get; set; }
    public DateTime? DateHourChange { get; set; }
    public string ChangedBy { get; set; }
}