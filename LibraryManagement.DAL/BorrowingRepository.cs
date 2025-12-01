using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Context;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LibraryManagement.DAL
{
    public class BorrowingRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowingRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBookCurrentlyUnavailableAsync(int bookID,int personID)
        {
            try
            {
                bool IsBookCurrentlyUnavailable = await _context.Borrowings.AnyAsync(
                                             b => b.BookID == bookID
                                             && b.PersonID == personID
                                             && b.ReturnDate == null
                                             && b.IsCanceled == false
                                            );
                return IsBookCurrentlyUnavailable;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> RecordNewBorrowingAsync(Borrowing borrowingEntity)
        {
            try
            {
                var bookToBorrow = await _context.Books.FindAsync(borrowingEntity.BookID);
                
                if (bookToBorrow == null || bookToBorrow.IsActive == false || bookToBorrow.Quantity<=0)
                {
                    throw new InvalidOperationException("Book is unavailable or not found.");
                }
                
                bookToBorrow.Quantity--;

               _context.Borrowings.Add(borrowingEntity);
                await _context.SaveChangesAsync();
                return borrowingEntity.BorrowingID;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task<int> UpdateBorrowingAsync(Borrowing borrowingEntity)
        {
            try{
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch(DbException ex) { throw; }
            catch (Exception ex) { throw; }
        }
        
        public static int EditDueDate(int borrowingID,DateTime dueDate)
        {
            string query = @"UPDATE Borrowings SET DueDate =@dueDate WHERE BorrowingID=@borrowingID AND ReturnDate IS NULL;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@borrowingID"] = (SqlDbType.Int, borrowingID,null),
                ["@dueDate"] = (SqlDbType.DateTime,dueDate,null)
            };
            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;
        }
        public static int DeleteBorrowing(int id)
        {
            string query = @"DELETE FROM Borrowings WHERE BorrowingID=@borrowingID;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@borrowingID"] = (SqlDbType.Int,id,null)
            };
            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;
        }

        public static BorrowingDTO FindBorrowingByID(int borrowingID)
        {
            string query = @"SELECT * FROM Borrowings WHERE BorrowingID=@borrowingID;";
            var parameters = new Dictionary<string, (SqlDbType,object,int?)>
            {
                ["@borrowingID"] = (SqlDbType.Int,borrowingID,null)
            };
          using var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters);

            if (reader != null && reader.Read())
            {

                return new BorrowingDTO
                {
                    BorrowingID = Convert.ToInt32(reader["BorrowingID"]),
                    BookID = Convert.ToInt32(reader["BookID"]),
                    PersonID = Convert.ToInt32(reader["MemberID"]),
                    BorrowingDate = Convert.ToDateTime(reader["BorrowingDate"]),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ReturnDate"]),
                    Status = reader["Status"].ToString(),
                    IsCanceled = Convert.ToBoolean(reader["IsCanceled"])
                };
            }
            return null;
            }
           
        public static List<BorrowingDTO> GetAllBorrowings()
        {
            var list = new List<BorrowingDTO>();
            string query = @"SELECT * FROM Borrowings;";
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text);
            BorrowingDTO dto = null;
            while (reader.Read())
            {
                dto = new BorrowingDTO
                {
                    BorrowingID = Convert.ToInt32(reader["BorrowingID"]),
                    BookID = Convert.ToInt32(reader["BookID"]),
                    PersonID = Convert.ToInt32(reader["MemberID"]),
                    BorrowingDate = Convert.ToDateTime(reader["BorrowingDate"]),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"],
                    Status = Convert.ToString(reader["Status"]),
                    IsCanceled = Convert.ToBoolean(reader["IsCanceled"])
                    
                };
                list.Add(dto);
            }
            return list;     
        }
         
        public static bool ReturnBookAndRestock(int borrowingID,int bookID,DateTime returnDate,int quantity)
        {
            string query1 = @"UPDATE Borrowings SET ReturnDate=@returnDate WHERE BorrowingID=@borrowingID;";
            string query2 = @"UPDATE Books SET Quantity=@quantity WHERE BookID=@bookID;";

            var parameters1 = new Dictionary<string, (SqlDbType, object, int?)> {
                ["@returnDate"] = (SqlDbType.DateTime,returnDate,null),
                ["@borrowingID"] = (SqlDbType.Int,borrowingID,null)
            };
            var parameters2 = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@quantity"] = (SqlDbType.Int, quantity, null),
                ["@bookID"] = (SqlDbType.Int, bookID, null)
            };

            var cmd1 = (query1,parameters1,false);
            var cmd2 = (query2, parameters2,false);

            var commands = new List<(string,Dictionary<string, (SqlDbType, object, int?)>,bool)>()
            {
                cmd1,cmd2
            };
            
           var result = SqlHelper.ExecuteTransaction(commands);

            return  result.Success;
               
       }
        public static bool CancelBorrowing(int borrowingID,int bookID)
        {
            string query1 = @"UPDATE Borrowings SET IsCanceled=1,ReturnDate=GetDate(),Status='Canceled'  WHERE BorrowingID=@borrowingID AND ReturnDate IS NULL AND IsCanceled=0;";
            string query2 = @"UPDATE Books SET Quantity=Quantity+1 WHERE BookID=@bookID;";

            var parameters1 = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@borrowingID"] = (SqlDbType.Int, borrowingID, null)
            };
            var parameters2 = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@bookID"] = (SqlDbType.Int, bookID, null)
            };

            var cmdBorrowing = (query1, parameters1, false);
            var cmdBook = (query2, parameters2, false);

            var commands = new List<(string, Dictionary<string, (SqlDbType, object, int?)>, bool)>()
            {
                cmdBorrowing,cmdBook
            };

            var result = SqlHelper.ExecuteTransaction(commands);

            return result.Success;

        }


    }
}
