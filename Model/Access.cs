using TODO.Domain;

namespace TODO.Model;

public class Access
{
    private string? _name;
    private int _level;
    public string? Name => _name;

    public int Level 
    { 
        get => _level;
        set
        {
            if (value is < 0 or > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Access level must be between 0 and 3.");
            }
            _level = value;
            _name = AccessLevel.GetByIndex(value).Name;
        }
    }

    protected bool Equals(Access other)
    {
        return Name == other.Name && Level == other.Level;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Access)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Level);
    }
    
}