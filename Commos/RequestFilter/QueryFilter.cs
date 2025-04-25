namespace Commons.RequestFilter
{
    public class QueryFilter
    {
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 10;
        public string? Ids { get; set; } = null;
        public string? Filter { get; set; } = null;
        public string? Type { get; set; } = null;
    }
}
