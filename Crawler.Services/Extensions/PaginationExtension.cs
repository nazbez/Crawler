using System.Collections.Generic;
using System.Linq;


namespace Crawler.Services.Extensions
{
    public static class PaginationExtension
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, int page = 1, int pageSize = 10)
        {
            return collection.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
