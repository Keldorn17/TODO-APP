using CommunityToolkit.Mvvm.ComponentModel;

namespace TODO.Model;

public partial class Shared : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private Access _sharedAccess;

    public Shared(string email, Access access)
    {
        Email = email;
        SharedAccess = access;
    }

    protected bool Equals(Shared other)
    {
        return Email == other.Email && SharedAccess.Equals(other.SharedAccess);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Shared)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Email, SharedAccess);
    }
}