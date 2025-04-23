using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using TODO.Domain;
using TODO.Model;
using TODO.Utils;

namespace TODO.ViewModel
{
    public partial class EditTodoViewModel : AbstractViewModel
    {
        public List<PriorityLevel> PriorityList { get; } = PriorityLevel.GetPriorities();
        public List<AccessLevel> AccessLevelList { get; } = AccessLevel.GetAccessLevels(false);

        [ObservableProperty]
        private TodoItem _currentTodo;

        [ObservableProperty]
        private TodoItem _copyTodo;

        [ObservableProperty]
        private string? _newSharedEmail;

        [ObservableProperty] 
        private string? _newCategory;

        [ObservableProperty]
        private Access _newAccess = new() { Level = 0 };

        [ObservableProperty]
        private Shared? _selectedSharedItem;

        [ObservableProperty]
        private Access? _selectedSharedAccess;
        
        [ObservableProperty]
        private string? _selectedCategory;

        [ObservableProperty]
        private string? _errorMessage;
        
        [ObservableProperty]
        private string? _errorMessageCategories;

        [ObservableProperty]
        private bool? _dialogResult;
        
        [ObservableProperty]
        private string _title;
        
        public bool CanRemoveSharedItem => SelectedSharedItem != null &&
                                           SelectedSharedItem?.SharedAccess.Level != AccessLevel.Owner.Index;
        public bool IsOwnerSelected => SelectedSharedItem?.SharedAccess?.Level == AccessLevel.Owner.Index;

        private readonly bool _isEditing;

        private readonly Window? _editWindow;
        public ObservableCollection<string> Categories => CopyTodo.Category;
        public ObservableCollection<Shared> SharedItems => CopyTodo.Shared;

        public EditTodoViewModel(TodoItem todoItem, Window editWindow, bool isEditing)
        {
            _isEditing = isEditing;
            Title = isEditing ? "Edit Todo" : "Add Todo";
            CurrentTodo = todoItem;
            CopyTodo = CurrentTodo.Clone();
            _editWindow = editWindow;

            SelectedSharedItem = CopyTodo.Shared.Count > 0 ? CopyTodo.Shared[0] : null;
            SelectedCategory = CopyTodo.Category.Count > 0 ? CopyTodo.Category[0] : null;
        }

        partial void OnSelectedSharedItemChanged(Shared? value)
        {
            SelectedSharedAccess = value?.SharedAccess;
            OnPropertyChanged(nameof(CanRemoveSharedItem));
            OnPropertyChanged(nameof(IsOwnerSelected));
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

            SelectedSharedItem = newShared;
            NewSharedEmail = string.Empty;
            NewAccess = new Access { Level = 0 };
            ErrorMessage = string.Empty;

            OnPropertyChanged(nameof(SharedItems));
        }

        [RelayCommand]
        private void RemoveSharedItem()
        {
            if (SelectedSharedItem == null) return;
            var index = CopyTodo.Shared.IndexOf(SelectedSharedItem);
            CopyTodo.Shared.Remove(SelectedSharedItem);
            
            SelectedSharedItem = CopyTodo.Shared.Count > 0 ? CopyTodo.Shared[Math.Min(index, CopyTodo.Shared.Count - 1)] : null;
        }
        
        [RelayCommand]
        private void AddCategory()
        {
            if (string.IsNullOrWhiteSpace(NewCategory))
            {
                ErrorMessageCategories = "Category cannot be empty";
                return;
            }

            if (CopyTodo.Category.Any(c => 
                    c.Equals(NewCategory, StringComparison.OrdinalIgnoreCase)))
            {
                ErrorMessageCategories = "This category already exists";
                return;
            }

            CopyTodo.Category.Add(NewCategory.Trim());
            SelectedCategory = NewCategory;
            NewCategory = string.Empty;
            ErrorMessageCategories = string.Empty;
        }

        [RelayCommand]
        private void RemoveCategory()
        {
            if (SelectedCategory == null) return;
            CopyTodo.Category.Remove(SelectedCategory);
            SelectedCategory = Categories.FirstOrDefault();
        }

        [RelayCommand]
        private void Save()
        {
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
                ReplaceCollection(CurrentTodo.Category, CopyTodo.Category);
                // Todo Edit logic
            }
            else
            { 
                // TODO Add logic
            }

            _editWindow?.Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            _editWindow?.Close();
        }

        [RelayCommand]
        private void Delete()
        {
            // TODO Delete logic
            _editWindow?.Close();
        }

        [RelayCommand]
        private void CompletedChanged()
        {
            CopyTodo.IsCompleted = !CopyTodo.IsCompleted;
        }

        private static void ReplaceCollection<T>(ObservableCollection<T> target, IEnumerable<T> source)
        {
            target.Clear();
            foreach (var item in source)
            {
                target.Add(item);
            }
        }
    }
}
