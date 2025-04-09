using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TODO.Utils;

namespace TODO.Model
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

        public ObservableCollection<Shared> Shared { get; set; } = [];

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
        private readonly TodoItem _todoItem = new TodoItem();

        public TodoItemBuilder SetId(long id)
        {
            _todoItem.Id = id;
            return this;
        }

        public TodoItemBuilder SetTitle(string title)
        {
            _todoItem.Title = title;
            return this;
        }

        public TodoItemBuilder SetDescription(string description)
        {
            _todoItem.Description = description;
            return this;
        }

        public TodoItemBuilder SetOwner(string owner)
        {
            _todoItem.Owner = owner;
            return this;
        }

        public TodoItemBuilder SetDeadline(string deadline)
        {
            _todoItem.Deadline = DateTimeUtils.DateTimeConverter(deadline);
            return this;
        }

        public TodoItemBuilder SetCategory(Category category)
        {
            _todoItem.Category = category;
            return this;
        }

        public TodoItemBuilder SetCreatedAt(string createdAt)
        {
            _todoItem.CreatedAt = DateTimeUtils.DateTimeConverter(createdAt);
            return this;
        }

        public TodoItemBuilder SetUpdatedAt(string updatedAt)
        {
            _todoItem.UpdatedAt = DateTimeUtils.DateTimeConverter(updatedAt);
            return this;
        }

        public TodoItemBuilder SetParent(string parent)
        {
            _todoItem.Parent = parent;
            return this;
        }

        public TodoItemBuilder SetPriority(Priority priority)
        {
            _todoItem.Priority = priority;
            return this;
        }

        public TodoItemBuilder SetIsCompleted(bool isCompleted)
        {
            _todoItem.IsCompleted = isCompleted;
            return this;
        }

        public TodoItemBuilder SetShared(Shared shared)
        {
            _todoItem.Shared.Add(shared);
            return this;
        }

        public TodoItem Build()
        {
            if (_todoItem.Priority == null)
            {
                _todoItem.Priority = new Priority { Level = 0 };
            }
            if (_todoItem.Category == null)
            {
                _todoItem.Category = new Category { Name = "General" };
            }
            if (_todoItem.Deadline == default)
            {
                _todoItem.Deadline = DateTime.Now;
            }
            if (_todoItem.CreatedAt == default)
            {
                _todoItem.CreatedAt = DateTime.Now;
            }
            if (_todoItem.UpdatedAt == default)
            {
                _todoItem.UpdatedAt = DateTime.Now;
            }
            return _todoItem;
        }
    }
}