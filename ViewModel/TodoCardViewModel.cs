using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.Model;

namespace TODO.ViewModel;

public partial class TodoCardViewModel : AbstractViewModel
{
    private readonly Action _checkboxAction;

    [ObservableProperty]
    private TodoItem _todoItem;

    public RelayCommand OpenEditWindowCommand { get; }

    private int _currentCategoryIndex;
    private TodoItem? _lastTodoItem;

    public string CurrentCategory =>
        TodoItem.Category.Count > 0 ? TodoItem.Category[_currentCategoryIndex] : string.Empty;

    public TodoCardViewModel(TodoItem todoItem, Action openEditWindowAction, Action checkboxAction)
    {
        TodoItem = todoItem;
        OpenEditWindowCommand = new RelayCommand(openEditWindowAction);
        _checkboxAction = checkboxAction;
        TodoItem.PropertyChanged += HandleTodoItemChanged;
    }

    public void CycleCategory(bool forward)
    {
        if (TodoItem?.Category is not { Count: > 1 }) return;
        _currentCategoryIndex = forward
            ? (_currentCategoryIndex + 1) % TodoItem.Category.Count
            : (_currentCategoryIndex - 1 + TodoItem.Category.Count) % TodoItem.Category.Count;
        
        OnPropertyChanged(nameof(CurrentCategory));
    }
    
    partial void OnTodoItemChanged(TodoItem value)
    {
        if (_lastTodoItem != null)
        {
            _lastTodoItem.Category.CollectionChanged -= OnCategoryCollectionChanged;
        }

        value.Category.CollectionChanged += OnCategoryCollectionChanged;
        _lastTodoItem = value;
        
        ResetCategoryIndex();
    }
    
    private void OnCategoryCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ResetCategoryIndex();

    private void ResetCategoryIndex()
    {
        _currentCategoryIndex = 0;
        OnPropertyChanged(nameof(CurrentCategory));
    }

    private void HandleTodoItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            _checkboxAction.Invoke();
        }
    }
}