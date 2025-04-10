namespace TODO.Domain
{
    public sealed class PriorityLevel
    {
        private static readonly List<PriorityLevel> Priorities = [];
        public static readonly PriorityLevel NotRequired = new PriorityLevel(0, "Not Required");
        public static readonly PriorityLevel Low = new PriorityLevel(1, "Low");
        public static readonly PriorityLevel Normal = new PriorityLevel(2, "Normal");
        public static readonly PriorityLevel High = new PriorityLevel(3, "High");
        public static readonly PriorityLevel Critical = new PriorityLevel(4, "Critical");

        public int Index { get; }
        public string Name { get; }
        private PriorityLevel(int value, string name)
        {
            Index = value;
            Name = name;
            Priorities.Add(this);
        }

        public static PriorityLevel GetByIndex(int index)
        {
            return Priorities.FirstOrDefault(priorityIndex => priorityIndex.Index == index, NotRequired);
        }

        public static PriorityLevel GetByName(string name)
        {
            return Priorities.FirstOrDefault(priorityIndex => priorityIndex.Name.Equals(name), NotRequired);
        }
        public static List<PriorityLevel> GetPriorities()
        {
            return Priorities;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
