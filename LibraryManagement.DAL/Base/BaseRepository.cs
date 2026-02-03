
using LibraryManagement.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Base
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query , string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return query;

            try
            {
                    return query.OrderBy(orderBy);
            }
            catch (Exception)
            {
                return query;
            }
           
        }

        public static async Task<PagedList<T>>  ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber,int pageSize )
        {
            int totalCount = await  query.CountAsync();

           var items = await  query
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            return new PagedList<T>(items,pageNumber,totalCount,pageSize);
        }
    }
}
