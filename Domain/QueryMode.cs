namespace TODO.Domain;

public sealed class QueryMode
{
    public static readonly QueryMode Own = new QueryMode("OWN");
    public static readonly QueryMode Shared = new QueryMode("SHARED");
    public static readonly QueryMode All = new QueryMode("ALL");

    public string Mode { get; }

    private QueryMode(string mode)
    {
        Mode = mode;
    }
}