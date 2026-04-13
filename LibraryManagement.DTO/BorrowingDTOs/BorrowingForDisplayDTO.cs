using LibraryManagement.DTO.FineDTO;


namespace LibraryManagement.DTO.BorrowingDTOs
{
    public class BorrowingForDisplayDTO
    {
       
            public required int BorrowingID { get; set; }
            public required string Title { get; set; }
            public required string Barcode { get; set; }
            public required string FullName { get; set; }
            public required DateTime BorrowingDate { get; set; }
            public required DateTime DueDate { get; set; }
            public required decimal InitialFees {get;set;}
            public FineDtoForDisplay? Fine { get; set; }
            public  decimal CurrentFine { get
            {
                if (Fine?.PaidAt != null || DateTime.UtcNow <= DueDate)
                    return 0;

                int overdueDays = (DateTime.UtcNow - DueDate).Days;
                return overdueDays * 15m;
            }
            }
            public int current { get; set; } = 0;
            public DateTime? ReturnDate { get; set; }
            public required string Status { get; set; }
            
    }
}
