using CommunityToolkit.Mvvm.ComponentModel;

namespace TODO.ViewModel;

public abstract class AbstractViewModel : ObservableObject
{
    public virtual Task OnNavigatedTo()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatedFrom()
    {
        return Task.CompletedTask;
    }
}