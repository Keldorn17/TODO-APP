using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Utils;

namespace TODO.MVVM.Model
{
    /// <summary>
    /// Represents a to-do item with properties such as title, description, deadline, and priority.
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Owner { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Category? Category { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? Parent { get; set; }
        public Priority? Priority { get; set; }
        public bool IsCompleted { get; set; }
        public bool Shared { get; set; }
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
            if (todoItem.Deadline == default)
            {
                todoItem.Deadline = DateTimeOffset.Now;
            }
            if (todoItem.CreatedAt == default)
            {
                todoItem.CreatedAt = DateTimeOffset.Now;
            }
            if (todoItem.UpdatedAt == default)
            {
                todoItem.UpdatedAt = DateTimeOffset.Now;
            }
            return todoItem;
        }
    }
}
