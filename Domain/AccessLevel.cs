namespace TODO.Domain
{
    public sealed class AccessLevel
    {
        private static readonly List<AccessLevel> AccessLevels = [];
        public static readonly AccessLevel Read = new AccessLevel(0, "Read");
        public static readonly AccessLevel Write = new AccessLevel(1, "Write");
        public static readonly AccessLevel Manage = new AccessLevel(2, "Manage");
        public static readonly AccessLevel Owner = new AccessLevel(3, "Owner");
        public int Index { get; }
        public string Name { get; }
        private AccessLevel(int value, string name)
        {
            Index = value;
            Name = name;
            AccessLevels.Add(this);
        }
        public static AccessLevel GetByIndex(int index)
        {
            return AccessLevels.FirstOrDefault(accessIndex => accessIndex.Index == index, Read);
        }
        public static AccessLevel GetByName(string name)
        {
            return AccessLevels.FirstOrDefault(accessIndex => accessIndex.Name.Equals(name), Read);
        }
        public static List<AccessLevel> GetAccessLevels()
        {
            return AccessLevels;
        }
        public static List<AccessLevel> GetAccessLevels(int maxIndex)
        {
            return AccessLevels.Where(accessIndex => accessIndex.Index <= maxIndex).ToList();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
