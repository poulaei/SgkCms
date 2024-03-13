using System.Linq;
using CmsKitDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmsKitDemo.Data;

public static class BoxEfCoreQueryableExtensions
{
    public static IQueryable<Box> IncludeDetails(this IQueryable<Box> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.BoxItems);
    }
}
