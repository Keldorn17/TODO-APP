using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using TODO.Client;
using TODO.Domain;
using TODO.DTO;
using TODO.Exceptions;
using TODO.Mapper;
using TODO.Model;
using TODO.Utils;

namespace TODO.ViewModel
{
    public partial class EditTodoViewModel : AbstractViewModel
    {
        public List<PriorityLevel> PriorityList { get; } = PriorityLevel.GetPriorities();
        public List<AccessLevel> AccessLevelList { get; } = AccessLevel.GetAccessLevels(false);

        [ObservableProperty] private TodoItem _currentTodo;

        [ObservableProperty] private TodoItem _copyTodo;

        [ObservableProperty] private string? _newSharedEmail;

        [ObservableProperty] private string? _newCategory;

        [ObservableProperty] private Access _newAccess = new() { Level = 0 };

        [ObservableProperty] private Shared? _selectedSharedItem;

        [ObservableProperty] private Access? _selectedSharedAccess;

        [ObservableProperty] private string? _selectedCategory;

        [ObservableProperty] private string? _errorMessage;

        [ObservableProperty] private string? _errorMessageCategories;

        [ObservableProperty] private string? _errorMessageTitleAndDescription;

        [ObservableProperty] private bool? _dialogResult;

        [ObservableProperty] private string _title;

        public bool CanRemoveSharedItem => SelectedSharedItem != null &&
                                           SelectedSharedItem?.SharedAccess.Level != AccessLevel.Owner.Index;

        public bool IsOwnerSelected => SelectedSharedItem?.SharedAccess?.Level == AccessLevel.Owner.Index;

        private readonly bool _isEditing;

        private Window? _editWindow;
        private readonly TodoClient _todoClient;
        private readonly IMessenger _messenger;
        public ObservableCollection<string> Categories => CopyTodo.Category;
        public ObservableCollection<Shared> SharedItems => CopyTodo.Shared;

        public EditTodoViewModel(TodoItem todoItem, TodoClient todoClient, IMessenger messenger, bool isEditing)
        {
            _isEditing = isEditing;
            Title = isEditing ? "Edit Todo" : "Add Todo";
            CurrentTodo = todoItem;
            CopyTodo = CurrentTodo.Clone();
            _todoClient = todoClient;
            _messenger = messenger;
        }

        partial void OnSelectedSharedItemChanged(Shared? value)
        {
            SelectedSharedAccess = value?.SharedAccess;
            OnPropertyChanged(nameof(CanRemoveSharedItem));
            OnPropertyChanged(nameof(IsOwnerSelected));
        }

        [RelayCommand]
        private async Task OnWindowLoaded()
        {
            _editWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            await GetShares();
        }

        private async Task GetShares()
        {
            if (_isEditing)
            {
                var shareResponses = await _todoClient.GetSharesForTodo(CopyTodo.Id);
                var currentShares = ShareMapper.Instance.SharedResponsesToShares(shareResponses);
                var copyShares = ShareMapper.Instance.SharedResponsesToShares(shareResponses);
                ReplaceCollection(CurrentTodo.Shared, currentShares);
                ReplaceCollection(CopyTodo.Shared, copyShares);
            }

            SelectedSharedItem = CopyTodo.Shared.Count > 0
                ? CopyTodo.Shared.First(share => share.SharedAccess.Level == AccessLevel.Owner.Index)
                : null;
            SelectedCategory = CopyTodo.Category.Count > 0 ? CopyTodo.Category[0] : null;
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

            SelectedSharedItem = CopyTodo.Shared.Count > 0
                ? CopyTodo.Shared[Math.Min(index, CopyTodo.Shared.Count - 1)]
                : null;
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
        private async Task Save()
        {
            if (string.IsNullOrEmpty(CopyTodo.Title) && string.IsNullOrEmpty(CopyTodo.Description))
            {
                ErrorMessageTitleAndDescription = "Either Title or Description cannot be empty";
                return;
            }

            if (_isEditing)
            {
                await UpdateTodo();
            }
            else
            {
                await CreateTodo();
            }
        }

        private async Task CreateTodo()
        {
            string? title = CopyTodo.Title;
            string? desc = CopyTodo.Description;
            string? deadline = CopyTodo.Deadline != null
                ? DateTimeUtils.ToJavaZonedDateTime(CopyTodo.Deadline.Value)
                : null;
            bool completed = CopyTodo.IsCompleted;
            long? parentId = null;
            int priority = CopyTodo.Priority.Level;
            List<string> categories = new List<string>(CopyTodo.Category);
            CreateTodoRequest request =
                new CreateTodoRequest(title, desc, deadline, completed, parentId, priority, categories);
            try
            {
                var createdTodo = await _todoClient.CreateTodo(request);
                CopyTodo.Id = createdTodo.Id;
                await AddShares(CopyTodo.Shared);
                _editWindow?.Close();
                _messenger.Send<TodoListChangedMessage>();
            }
            catch (TodoClientException ex)
            {
                ShowErrorWindow(ex);
            }
        }

        private async Task UpdateTodo()
        {
            bool isTodoUpdated = false;
            string? title = CurrentTodo.Title;
            string? desc = CurrentTodo.Description;
            DateTime? cur = CurrentTodo.Deadline;
            string? deadline = null;
            bool? completed = CurrentTodo.IsCompleted;
            long? parentId = CurrentTodo.Parent;
            int? priority = CurrentTodo.Priority.Level;
            List<string>? categories = null;
            UpdateIfChanged(ref title, CopyTodo.Title, ref isTodoUpdated);
            UpdateIfChanged(ref desc, CopyTodo.Description, ref isTodoUpdated);
            UpdateIfChanged(ref cur, CopyTodo.Deadline, ref isTodoUpdated);
            UpdateIfChanged(ref completed, CopyTodo.IsCompleted, ref isTodoUpdated);
            UpdateIfChanged(ref parentId, CopyTodo.Parent, ref isTodoUpdated);
            UpdateIfChanged(ref priority, CopyTodo.Priority.Level, ref isTodoUpdated);
            var currentTodoCategories = new HashSet<string>(CurrentTodo.Category);
            var copyTodoCategories = new HashSet<string>(CopyTodo.Category);
            if (!currentTodoCategories.SetEquals(copyTodoCategories))
            {
                categories = new List<string>(copyTodoCategories);
                isTodoUpdated = true;
            }
            
            bool isSharesUpdated = false;
            var oldShares = new HashSet<Shared>(CurrentTodo.Shared);
            var newShares = new HashSet<Shared>(CopyTodo.Shared);
            newShares.ExceptWith(oldShares);
            var deletedShares = GetDeletedShares(oldShares);
            if (deletedShares.Count > 0) isSharesUpdated = true;
            if (newShares.Count > 0) isSharesUpdated = true;

            if (!isTodoUpdated && !isSharesUpdated)
            {
                _editWindow?.Close();
                return;
            }

            var updateRequest = new UpdateTodoRequest(title, desc, deadline, completed, parentId, priority, categories);
            try
            {
                if (isTodoUpdated)
                {
                    await _todoClient.PatchTodo(CopyTodo.Id, updateRequest);
                }

                if (isSharesUpdated)
                {
                    await AddShares(new List<Shared>(newShares));
                    await RemoveShares(deletedShares);
                }
                _editWindow?.Close();
                _messenger.Send<TodoListChangedMessage>();
            }
            catch (TodoClientException ex)
            {
                ShowErrorWindow(ex);
            }
        }
        
        private void UpdateIfChanged<T>(ref T? currentValue, T newValue, ref bool isUpdated)
        {
            if (!EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                isUpdated = true;
            }
            else
            {
                currentValue = default;
            }
        }

        private List<string> GetDeletedShares(HashSet<Shared> oldShares)
        {
            var comparer = EqualityComparer<Shared>.Create((s1, s2) =>
            {
                if (s1 == null && s2 == null) return true;
                if (s1 == null || s2 == null) return false;
                return s1.Email.Equals(s2.Email);
            });
            oldShares.RemoveWhere(share => CopyTodo.Shared.Contains(share, comparer));
            return oldShares.Select(s => s.Email).ToList();
        }

        private async Task AddShares(IList<Shared> shares)
        {
            foreach (var share in shares)
            {
                if (share.SharedAccess.Level == AccessLevel.Owner.Index) continue;
                var request = new TodoShareRequest(share.Email, share.SharedAccess.Level);
                await _todoClient.CreateShareForTodo(CopyTodo.Id, request);
            }
        }

        private async Task RemoveShares(IList<string> emails)
        {
            foreach (var email in emails)
            {
                var request = new TodoShareDeleteRequest(email);
                await _todoClient.DeleteShareForTodo(CopyTodo.Id, request);
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            _editWindow?.Close();
        }

        [RelayCommand]
        private async Task Delete()
        {
            try
            {
                await _todoClient.DeleteTodoById(CopyTodo.Id);
                _editWindow?.Close();
                _messenger.Send<TodoListChangedMessage>();
            }
            catch (TodoClientException ex)
            {
                ShowErrorWindow(ex);
            }
        }

        private void ShowErrorWindow(TodoClientException ex)
        {
            var details = ex.ErrorDetails;
            MessageBox.Show(details.Details, details.Title, MessageBoxButton.OK, MessageBoxImage.Error);
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