using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Model;
using TODO.Service;

namespace TODO.ViewModel
{
    partial class LoginViewModel : AbstractViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        [ObservableProperty]
        private INavigationService _navigation;

        public RelayCommand NavigateMainViewCommand { get; set; }

        public LoginViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateMainViewCommand = new RelayCommand(() => { Navigation.NavigateTo<MainViewModel>(); }, () => true);
        }
    }
}
