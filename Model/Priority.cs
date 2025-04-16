using TODO.Domain;

namespace TODO.Model
{
    /// <summary>
    /// Represents the priority level of a to-do item.
    /// </summary>
    public class Priority
    {
        private int _level;

        public int Level
        {
            get => _level;
            set
            {
                if (value is >= 0 and <= 4)
                {
                    _level = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Priority level must be between 0 and 4");
                }
            }
        }
        
        public string Name { get; init; }
        
    }
}
