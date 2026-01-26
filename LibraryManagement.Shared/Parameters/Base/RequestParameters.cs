namespace LibraryManagement.Shared.Parameters.Base
{
    public abstract class RequestParameters
    {
       private const int _maxPageSize = 50;
       private int _pageSize = 10;
       public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
        public int PageNumber { get; set; } = 1;
        public string? SearchTerm { get; set; }
    }
}
