using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TODO.Core;
using TODO.MVVM.Model;
using TODO.MVVM.View;

namespace TODO.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        public ICommand OpenEditWindowCommand { get; }
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set
            {
                _todoItems = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(ObservableCollection<TodoItem> sharedTodoItems)
        {
            TodoItems = sharedTodoItems;

            for (int i = 0; i < 5; i++)
            {
                InitializeTodoItems();
            }

            OpenEditWindowCommand = new RelayCommand<TodoItem>(item => {
                System.Diagnostics.Debug.WriteLine("OpenEditWindow Command executed!");
                OpenEditWindow(item);
            });
        }

        private void OpenEditWindow(TodoItem todoItem)
        {
            if (todoItem == null) return;

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
