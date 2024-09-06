namespace InoveFk.Core.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt {get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt {get; set; }
    public string? UpdatedBy { get; set; }
    public bool Active { get; private set; }

    public bool Activate() => Active = true;
    public bool Deactivate() => Active = false;
    
}
