

namespace LibraryManagement.Shared.Helpers
{
    public class PagedList<T>
    {
      
        public Metadata Metadata { get; set; }
        public List<T> Items { get; private set; }

        public PagedList(List<T> items ,int currentPage, int totalCount, int pageSize)
        {
            Metadata = new Metadata {
                CurrentPage = currentPage,
                TotalCount = totalCount,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            Items = items;
        }
    }
}
