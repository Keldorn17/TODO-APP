
using CommunityToolkit.Mvvm.ComponentModel;

namespace TODO.Model
{
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

    }
}
