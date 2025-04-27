using TODO.Domain;

namespace TODO.Service;

public interface IProfileService
{
    
    void OpenProfilePage();

    void SaveProfile(string idToken);
    
    Profile GetProfile();
    
}