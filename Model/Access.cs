using TODO.Domain;

namespace TODO.Model
{
    public class Access
    {
        private string? _name;
        private int _level = 0;
        public string? Name { get => _name; }
        public int Level 
        { 
            get => _level;
            set
            {
                if (value < 0 || value > 3)
                {
                    throw new ArgumentOutOfRangeException($"Access level must be between 0 and 3.");
                }
                _level = value;
                _name = AccessLevel.GetByIndex(value).Name;
            }
        }
    }
}
