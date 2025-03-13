using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.MVVM.Model
{
    class TodoItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string Deadline { get; set; } // DateTime type is not used to avoid complexity
        public string Category { get; set; }
        public string CreatedAt { get; set; } // DateTime type is not used to avoid complexity
        public string UpdatedAt { get; set; } // DateTime type is not used to avoid complexity
        public string Parent { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public bool Shared { get; set; }
    }

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
}
