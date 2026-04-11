
namespace LibraryManagement.Domain.Entities
{
public class Fine {
    public int FineID {get;set;}
    public int BorrowingID {get;set;}
    public int MemberID {get;set;}
    public decimal Amount { get; set; }
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime? PaidAt {get;set;} = null;
    public string Status {get;set;} = "Pending" ;
    public string? WaiveReason{get;set;} = null;
        
        public Member Member { get; set; } = null!;
        public Borrowing Borrowing { get; set; } = null!;
}

}