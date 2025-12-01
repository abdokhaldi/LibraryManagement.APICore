using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryManagement.DAL
{
    public class BorrowingInfoRepository
    {
        public static List<BorrowingInfoDTO> GetAllBorrowings()
        {
            List<BorrowingInfoDTO> borrowings = new();
            string query = @"SELECT * FROM FullBorrowingsInfo;";
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,null);
            if (reader == null) return null;
            while (reader.Read())
            {
                borrowings.Add(
                    new BorrowingInfoDTO
                    {
                        BorrowingID = (int)reader["BorrowingID"],
                        Title = Convert.ToString(reader["Title"]),
                        FullName = reader["FullName"].ToString(),
                        BorrowingDate = (DateTime)reader["BorrowingDate"],
                        DueDate = (DateTime)reader["DueDate"],
                        ReturnDate = reader["ReturnDate"]==DBNull.Value?null: Convert.ToDateTime(reader["ReturnDate"]),
                        Status = (string)reader["Status"],
                        BookID = (int)reader["BookID"],
                    }
                    );
            }
            return borrowings;
        }
    }
}
