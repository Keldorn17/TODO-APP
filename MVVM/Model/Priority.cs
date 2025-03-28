using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.MVVM.Model
{
    /// <summary>
    /// Represents the priority level of a to-do item.
    /// </summary>
    public class Priority
    {
        private int level;
        public int Level
        {
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
        public string? Description { get; set; }
        public string? Name { get; set; }
    }
}
