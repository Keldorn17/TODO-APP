using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.MVVM.Model
{
    /// <summary>
    /// Represents a to-do item with properties such as title, description, deadline, and priority.
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Category Category { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Parent { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public bool Shared { get; set; }
    }

    /// <summary>
    /// Represents the priority level of a to-do item.
    /// </summary>
    public class Priority
    {
        private int level;
        public int Level {
            get => level;
            set 
            {
                if (value >= 0 && value <= 4)
                {
                    level = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Priority level must be between 0 and 4");
                }
            } 
        }
        public string Description { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents the category of a to-do item.
    /// </summary>
    public class Category 
    { 
        public string Name { get; set; }
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
            todoItem.Deadline = DateTimeConverter(deadline);
            return this;
        }

        public TodoItemBuilder SetCategory(string name)
        {
            todoItem.Category = new Category { Name = name }; 
            return this;
        }

        public TodoItemBuilder SetCreatedAt(string createdAt)
        {
            todoItem.CreatedAt = DateTimeConverter(createdAt);
            return this;
        }

        public TodoItemBuilder SetUpdatedAt(string updatedAt)
        {
            todoItem.UpdatedAt = DateTimeConverter(updatedAt);
            return this;
        }

        public TodoItemBuilder SetParent(string parent)
        {
            todoItem.Parent = parent;
            return this;
        }

        public TodoItemBuilder SetPriority(int level, string name = "Not required", string description = "")
        {
            if (level < 0 || level > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(level), "Priority level must be between 0 and 4.");
            }
            todoItem.Priority = new Priority { Level = level, Name = name, Description = description };
            return this;
        }

        public TodoItemBuilder SetIsCompleted(bool isCompleted)
        {
            todoItem.IsCompleted = isCompleted;
            return this;
        }

        public TodoItemBuilder SetShared(bool shared)
        {
            todoItem.Shared = shared;
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
                todoItem.Priority = new Priority { Level = 0, Name = "Not required" };
            }
            if (todoItem.Category == null)
            {
                todoItem.Category = new Category { Name = "General" };
            }
            if (todoItem.Deadline == default(DateTimeOffset))
            { 
                todoItem.Deadline = DateTimeOffset.Now;
            }
            else if (todoItem.Deadline < DateTimeOffset.Now)
            {
                throw new InvalidOperationException("Deadline cannot be in the past");
            }
            if (todoItem.CreatedAt == default(DateTimeOffset))
            {
                todoItem.CreatedAt = DateTimeOffset.Now;
            }
            if (todoItem.UpdatedAt == default(DateTimeOffset))
            {
                todoItem.UpdatedAt = DateTimeOffset.Now;
            }
            return todoItem;
        }

        public DateTimeOffset DateTimeConverter(string zonedTimeData) 
        {
            if (string.IsNullOrEmpty(zonedTimeData))
            {
                throw new ArgumentNullException(nameof(zonedTimeData), "Date-time string cannot be null or empty.");
            }
            if (DateTimeOffset.TryParse(zonedTimeData, out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset.LocalDateTime;
            }
            else
            {
                throw new FormatException("Invalid date-time format.");
            }
        }
    }
}
