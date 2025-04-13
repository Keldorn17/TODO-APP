using TODO.Domain;

namespace TODO.Model
{
    public class Access
    {
        private string? _name;
        private int _level = 0;
        public string? Name => _name;

        public int Level 
        { 
            get => _level;
            set
            {
                if (value is < 0 or > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Access level must be between 0 and 3.");
                }
                _level = value;
                _name = AccessLevel.GetByIndex(value).Name;
            }
        }
    }
}
