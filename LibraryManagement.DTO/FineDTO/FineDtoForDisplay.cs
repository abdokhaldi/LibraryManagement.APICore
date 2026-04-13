

namespace LibraryManagement.DTO.FineDTO
{
    public class FineDtoForDisplay
    {
        public int FineID { get; set; }
        public int BorrowingID { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? PaidAt { get; set; } = null;
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
         public string WaiveReason { get; set; } = null!;
    }
}
