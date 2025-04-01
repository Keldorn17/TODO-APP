using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TODO.Utils;

namespace TODO.MVVM.Model
{
    /// <summary>
    /// Represents a to-do item with properties such as title, description, deadline, and priority.
    /// </summary>
    public partial class TodoItem : ObservableObject
    {
        [ObservableProperty]
        private long _id;

        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private string? _owner;

        [ObservableProperty]
        private DateTime _deadline;

        [ObservableProperty]
        private Category? _category;

        [ObservableProperty]
        private DateTime _createdAt;

        [ObservableProperty]
        private DateTime _updatedAt;

        [ObservableProperty]
        private string? _parent;

        [ObservableProperty]
        private Priority? _priority;

        [ObservableProperty]
        private bool _isCompleted;

        public ObservableCollection<Shared> Shared { get; set; } = new ObservableCollection<Shared>();

        /// <summary>
        /// Creates a deep copy of the TodoItem
        /// </summary>
        public TodoItem Clone()
        {
            var clone = new TodoItem
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Owner = this.Owner,
                Deadline = this.Deadline,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt,
                Parent = this.Parent,
                IsCompleted = this.IsCompleted
            };

            if (this.Category != null)
            {
                clone.Category = new Category { Name = this.Category.Name };
            }

            if (this.Priority != null)
            {
                clone.Priority = new Priority
                {
                    Level = this.Priority.Level,
                    Description = this.Priority.Description
                };
            }

            foreach (var sharedItem in this.Shared)
            {
                clone.Shared.Add(new Shared(
                    sharedItem.Email,
                    new Access { Level = sharedItem.SharedAccess.Level }
                ));
            }

            return clone;
        }
    }

    /// <summary>
    /// Builder class for constructing <see cref="TodoItem"/> objects.
    /// </summary>
    public class TodoItemBuilder
    {
        private readonly TodoItem todoItem = new TodoItem();

        public TodoItemBuilder SetId(long id)
        {
            todoItem.Id = id;
            return this;
        }

        public TodoItemBuilder SetTitle(string title)
        {
            todoItem.Title = title;
            return this;
        }

        public TodoItemBuilder SetDescription(string description)
        {
            todoItem.Description = description;
            return this;
        }

        public TodoItemBuilder SetOwner(string owner)
        {
            todoItem.Owner = owner;
            return this;
        }

        public TodoItemBuilder SetDeadline(string deadline)
        {
            todoItem.Deadline = DateTimeUtils.DateTimeConverter(deadline);
            return this;
        }

        public TodoItemBuilder SetCategory(Category category)
        {
            todoItem.Category = category;
            return this;
        }

        public TodoItemBuilder SetCreatedAt(string createdAt)
        {
            todoItem.CreatedAt = DateTimeUtils.DateTimeConverter(createdAt);
            return this;
        }

        public TodoItemBuilder SetUpdatedAt(string updatedAt)
        {
            todoItem.UpdatedAt = DateTimeUtils.DateTimeConverter(updatedAt);
            return this;
        }

        public TodoItemBuilder SetParent(string parent)
        {
            todoItem.Parent = parent;
            return this;
        }

        public TodoItemBuilder SetPriority(Priority priority)
        {
            todoItem.Priority = priority;
            return this;
        }

        public TodoItemBuilder SetIsCompleted(bool isCompleted)
        {
            todoItem.IsCompleted = isCompleted;
            return this;
        }

        public TodoItemBuilder SetShared(Shared shared)
        {
            todoItem.Shared.Add(shared);
            return this;
        }

        public TodoItem Build()
        {
            if (string.IsNullOrEmpty(todoItem.Title))
            {
                throw new InvalidOperationException("Title is required");
            }
            if (string.IsNullOrEmpty(todoItem.Description))
            {
                throw new InvalidOperationException("Description is required");
            }
            if (todoItem.Priority == null)
            {
                todoItem.Priority = new Priority { Level = 0 };
            }
            if (todoItem.Category == null)
            {
                todoItem.Category = new Category { Name = "General" };
            }
            if (todoItem.Deadline == default)
            {
                todoItem.Deadline = DateTime.Now;
            }
            if (todoItem.CreatedAt == default)
            {
                todoItem.CreatedAt = DateTime.Now;
            }
            if (todoItem.UpdatedAt == default)
            {
                todoItem.UpdatedAt = DateTime.Now;
            }
            return todoItem;
        }
    }
}