using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.Model;

namespace TODO.ViewModel;

public partial class TodoCardViewModel : AbstractViewModel
{
    [ObservableProperty]
    private TodoItem _todoItem;

    public RelayCommand OpenEditWindowCommand { get; }

    private int _currentCategoryIndex;
    private TodoItem? _lastTodoItem;

    public string CurrentCategory =>
        TodoItem.Category.Count > 0 ? TodoItem.Category[_currentCategoryIndex] : string.Empty;

    public TodoCardViewModel(TodoItem todoItem, Action openEditWindowAction)
    {
        TodoItem = todoItem;
        OpenEditWindowCommand = new RelayCommand(openEditWindowAction);
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
}