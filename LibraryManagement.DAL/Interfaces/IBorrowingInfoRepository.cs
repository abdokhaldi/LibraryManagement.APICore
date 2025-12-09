using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IBorrowingInfoRepository
    {
        Task<IQueryable<BorrowingInfoView>> GetQueryableFullBorrowingsInfoAsync();
    }
}