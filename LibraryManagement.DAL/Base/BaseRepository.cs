
using System.Linq.Dynamic.Core;
using System.Xml.Linq;

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


    }
}
