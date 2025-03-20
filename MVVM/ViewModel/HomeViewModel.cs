using System;
using System.Collections.ObjectModel;
using TODO.Core;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
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

        public HomeViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            for (int i = 0; i < 5; i++)
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
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Validation error: {e.Message}");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Invalid format: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected error occurred: {e.Message}");
                }
            }
        }
    }
}
