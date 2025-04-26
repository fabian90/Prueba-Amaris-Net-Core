using Amazon.DynamoDBv2.DocumentModel;
using Commons.Response;

namespace Commons.Paging
{
    public static class PagingExtension
    {
        public static async Task<RecordsResponse<T>> GetPagedAsync<T>(
            Table table,
            int page,
            int take,
            Func<Document, T> mapFunc)
        {
            var search = table.Scan(new ScanOperationConfig());
            var allItems = new List<T>();
            do
            {
                var set = await search.GetNextSetAsync();
                allItems.AddRange(set.Select(mapFunc));
            } while (!search.IsDone);

            var total = allItems.Count;
            var pagedItems = allItems
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            return new RecordsResponse<T>
            {
                Items = pagedItems,
                Total = total,
                Page = page,
                Pages = (int)Math.Ceiling((double)total / take)
            };
        }
    }
}
