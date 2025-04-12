using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using TODO.Domain;
using TODO.Model;
using TODO.Utils;
using TODO.Core;

namespace TODO.ViewModel
{
    public partial class EditTodoViewModel : AbstractViewMode
    {
        public List<PriorityLevel> PriorityList { get; } = PriorityLevel.GetPriorities();
        public List<AccessLevel> AccessLevelList { get; } = AccessLevel.GetAccessLevels(2);

        [ObservableProperty]
        private TodoItem _currentTodo;

        [ObservableProperty]
        private TodoItem _copyTodo;

        [ObservableProperty]
        private string? _newSharedEmail;

        [ObservableProperty]
        private Access _newAccess = new() { Level = 0 };

        [ObservableProperty]
        private Shared? _selectedSharedItem;

        [ObservableProperty]
        private Access? _selectedSharedAccess;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool? _dialogResult;

        [ObservableProperty]
        private MainViewModel _mainViewModel;

        [ObservableProperty]
        private string _title;

        private readonly bool _isEditing;

        private readonly Window? _editWindow;

        private ObservableCollection<Shared> _sharedItems = null!;
        public ObservableCollection<Shared> SharedItems
        {
            get => _sharedItems ??= CopyTodo.Shared;
            set => SetProperty(ref _sharedItems, value);
        }

        public EditTodoViewModel(TodoItem todoItem, Window editWindow, MainViewModel mainViewModel, bool isEditing)
        {
            _isEditing = isEditing;
            Title = isEditing ? "Edit Todo" : "Add Todo";
            CurrentTodo = todoItem;
            CopyTodo = CurrentTodo.Clone();
            MainViewModel = mainViewModel;
            _editWindow = editWindow;
        }

        partial void OnSelectedSharedItemChanged(Shared? value)
        {
            SelectedSharedAccess = value?.SharedAccess;
        }

        [RelayCommand]
        private void AddSharedItem()
        {
            if (string.IsNullOrWhiteSpace(NewSharedEmail))
            {
                ErrorMessage = "Email cannot be empty";
                return;
            }

            if (!EmailValidator.IsValidEmail(NewSharedEmail))
            {
                ErrorMessage = "Please enter a valid email address";
                return;
            }

            if (CopyTodo.Shared.Any(s =>
                s.Email.Equals(NewSharedEmail, StringComparison.OrdinalIgnoreCase)))
            {
                ErrorMessage = "This email is already shared";
                return;
            }

            var newShared = new Shared(NewSharedEmail.Trim(), new Access { Level = NewAccess.Level });
            CopyTodo.Shared.Add(newShared);

            NewSharedEmail = string.Empty;
            NewAccess = new Access { Level = 0 };
            ErrorMessage = string.Empty;

            OnPropertyChanged(nameof(SharedItems));
        }

        [RelayCommand]
        private void RemoveSharedItem()
        {
            if (SelectedSharedItem == null) return;
            CopyTodo.Shared.Remove(SelectedSharedItem);
            SelectedSharedItem = null;
        }

        [RelayCommand]
        private void Save()
        {
            if (string.IsNullOrEmpty(CopyTodo.Title) && string.IsNullOrEmpty(CopyTodo.Description))
            {
                if (_isEditing)
                {
                    MainViewModel.TodoItems.Remove(CurrentTodo);
                }
                WindowHelper.CloseWindow(_editWindow);
                return;
            }

            if (_isEditing)
            {
                CurrentTodo.Title = CopyTodo.Title;
                CurrentTodo.Description = CopyTodo.Description;
                CurrentTodo.Deadline = CopyTodo.Deadline;
                CurrentTodo.Priority = CopyTodo.Priority;
                CurrentTodo.Shared = CopyTodo.Shared;
                CurrentTodo.UpdatedAt = DateTime.Now;
                CurrentTodo.Shared = CopyTodo.Shared;
                CurrentTodo.IsCompleted = CopyTodo.IsCompleted;
            }
            else
            { 
                MainViewModel.TodoItems.Add(CopyTodo);
            }

            WindowHelper.CloseWindow(_editWindow);
        }

        [RelayCommand]
        private void Cancel()
        {
            WindowHelper.CloseWindow(_editWindow);
        }

        [RelayCommand]
        private void Delete()
        {
            MainViewModel.TodoItems.Remove(CurrentTodo);
            WindowHelper.CloseWindow(_editWindow);
        }

        [RelayCommand]
        private void CompletedChanged()
        {
            CopyTodo.IsCompleted = !CopyTodo.IsCompleted;
        }

    }
}
