using TODO.Domain;

namespace TODO.Utils;

public static class QueryBuilderUtils
{
    public static string BuildQuery(string? text, QueryMode queryMode, Pageable pageable)
    {
        if (string.IsNullOrEmpty(text)) return $"?mode={queryMode.Mode}&{BuildPageQuery(pageable)}";
        return $"?search=title=ilike='{text}' or description=ilike='{text}'&mode={queryMode.Mode}&{BuildPageQuery(pageable)}";
    }
    
    private static string BuildPageQuery(Pageable pageable)
    {
        return $"pageNumber={pageable.PageNumber}&pageSize={pageable.PageSize}";
    }
}