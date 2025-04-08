using TODO.Domain;

namespace TODO.Model
{
    public class Access
    {
        private string? name;
        private int level = 0;
        public string? Name { get => name; }
        public int Level 
        { 
            get => level;
            set
            {
                if (value < 0 || value > 3)
                {
                    throw new ArgumentOutOfRangeException("Access level must be between 0 and 3.");
                }
                level = value;
                name = AccessLevel.GetByIndex(value).Name;
            }
        }
    }
}
