using TODO.Utils;

namespace TODO.MVVM.Model
{
    /// <summary>
    /// Represents the priority level of a to-do item.
    /// </summary>
    public class Priority
    {
        private int level;
        private string? name;
        public int Level
        {
            get => level;
            set
            {
                if (value >= 0 && value <= 4)
                {
                    level = value;
                    name = PriorityLevel.GetByIndex(value).Name;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Priority level must be between 0 and 4");
                }
            }
        }
        public string? Description { get; set; }
        public string? Name { get => name; }
    }
}
