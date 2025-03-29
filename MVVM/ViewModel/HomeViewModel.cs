using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.MVVM.Model;
using TODO.MVVM.View;

namespace TODO.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        public HomeViewModel(ObservableCollection<TodoItem> sharedTodoItems)
        {
            TodoItems = sharedTodoItems;

            for (int i = 0; i < 5; i++)
            {
                InitializeTodoItems();
            }
        }

        [RelayCommand]
        private void OpenEditWindow(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                System.Diagnostics.Debug.WriteLine("TodoItem is null!");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"Opening edit window for: {todoItem.Title}");
            EditTodoWindow editWindow = new EditTodoWindow(todoItem);
            editWindow.ShowDialog();
        }

        private void InitializeTodoItems()
        {
            try
            {
                TodoItems.Add(new TodoItemBuilder()
                    .SetId(1)
                    .SetTitle("What is Lorem Ipsum?")
                    .SetDescription("Lorem Ipsum is simply dummy text of the printing and typesetting industry.")
                    .SetPriority(new Priority { Level = 1 })
                    .SetDeadline("2026-10-02T14:45:30.123456789+05:30")
                    .Build());

                TodoItems.Add(new TodoItemBuilder()
                    .SetId(2)
                    .SetTitle("Why do we use it?")
                    .SetDescription("It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.")
                    .SetIsCompleted(true)
                    .Build());

                TodoItems.Add(new TodoItemBuilder()
                    .SetId(3)
                    .SetTitle("Where does it come from?")
                    .SetDescription("The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.")
                    .SetPriority(new Priority { Level = 2 })
                    .Build());

                TodoItems.Add(new TodoItemBuilder()
                    .SetId(4)
                    .SetTitle("Where can I get some?")
                    .SetDescription("There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable.")
                    .SetPriority(new Priority { Level = 3 })
                    .SetIsCompleted(true)
                    .Build());

                TodoItems.Add(new TodoItemBuilder()
                    .SetId(5)
                    .SetTitle("What is Lorem Ipsum?")
                    .SetDescription("Lorem Ipsum is simply dummy text of the printing and typesetting industry.")
                    .SetPriority(new Priority { Level = 4 })
                    .Build());
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}