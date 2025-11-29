using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace LibraryManagement.DAL
{
    public class BookRepository
    {
        private static BookDTO FillBookDTO(SqlDataReader reader)
        {
          return new BookDTO
            {
                BookID = Convert.ToInt32(reader["BookID"]),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                Publisher = reader["Publisher"].ToString(),
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                Image = reader["Image"].ToString(),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
            };
        }
        static public List<BookDTO> GetAllBooks()
        {
            var list = new List<BookDTO>();
            BookDTO bookDTO = null;
            string query = $"SELECT * FROM Books"; // SQL query
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text);
            while (reader.Read())
            {
                bookDTO = FillBookDTO(reader);
                list.Add(bookDTO);
            }
            return list;
        }


        public static BookDTO FindBookByID(int id)
        {
            string query = @"SELECT * FROM Books WHERE BookID=@bookID;";

            var parameters = new Dictionary<string, (SqlDbType, object,int?)>
            {
                ["@bookID"] = (SqlDbType.Int,id,null)
            };
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
            if (!reader.Read()) return null;

            return FillBookDTO(reader);
        }
           
        


        public static int AddNewBook(BookDTO data)
        {
            string query = @"INSERT INTO Books (Title,Author,Publisher,YearPublished,Quantity,Image,CategoryID,IsActive)
                           VALUES(@title,@author,@publisher,@yearPublished,@quantity,@image,@categoryID,@isActive)
                           SELECT CAST(SCOPE_IDENTITY() AS int);";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@title"] = (SqlDbType.NVarChar, data.Title, 200),
                ["@author"] = (SqlDbType.NVarChar, data.Author, 100),
                ["@publisher"] = (SqlDbType.NVarChar, data.Publisher, 100),
                ["@yearPublished"] = (SqlDbType.Int, data.YearPublished, 50),
                ["@quantity"] = (SqlDbType.Int, data.Quantity, null),
                ["@categoryID"] = (SqlDbType.Int, data.CategoryID, null),
                ["@image"] = (SqlDbType.NVarChar, data.Image, 150),
                ["IsActive"] = (SqlDbType.Bit,data.IsActive,null),
            };

            int bookID = (int)SqlHelper.ExecuteCommand(query, CommandType.Text, SqlHelper.ExecuteType.ExecuteScalar, parameters);
           
            return bookID;
           }

        public static int UpdateBookByID(BookDTO data)
        {
            string query = @"UPDATE Books 
                              SET Title=@title,
                                  Author=@author,
                                  Publisher=@publisher,
                                  YearPublished=@yearPublished,
                                  Quantity=@quantity,
                                  Image = @image,
                                  CategoryID=@categoryID ,
                                  IsActive = @isActive
                            WHERE BookID=@bookID;";
            
           
                        var parameters = new Dictionary<string, (SqlDbType, object, int?)>
                        {
                            ["@bookID"] = (SqlDbType.Int, data.BookID, null),
                            ["@title"] = (SqlDbType.NVarChar, data.Title, 200),
                            ["@author"] = (SqlDbType.NVarChar, data.Author, 100),
                            ["@publisher"] = (SqlDbType.NVarChar, data.Publisher, 100),
                            ["@yearPublished"] = (SqlDbType.NVarChar, data.YearPublished, 50),
                            ["@quantity"] = (SqlDbType.Int, data.Quantity, null),
                            ["@categoryID"] = (SqlDbType.Int,data.CategoryID,null),
                            ["@image"] = (SqlDbType.NVarChar, data.Image, 150),
                            ["IsActive"] = (SqlDbType.Bit, data.IsActive, null),

                        };
            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;      
        }

        public static bool DeleteBookByID(int bookID)
        {
            string query = @"DELETE FROM Books WHERE BookID=@bookID";
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand(query,conn))
                    {
                        comm.Parameters.Add("@bookID", SqlDbType.Int).Value = bookID;

                        rowsAffected = comm.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
                throw new Exception($"Error : {ex.Message}");
            }

            return rowsAffected > 0;
        }

        public static object GetBookQuantity(int id)
        {
            string query = @"SELECT Quantity FROM Books WHERE BookID=@id;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@id"] = (SqlDbType.Int,id,null)
            };
            object quantity = SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteScalar,parameters);
            return quantity;
        }

        public static int SetBookActiveStatus(int bookID, bool isActive)
        {
            string query = @"UPDATE Books 
                             SET IsActive = @isActive WHERE BookID=@bookID;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@bookID"] = (SqlDbType.Int, bookID, null),
                ["@isActive"] = (SqlDbType.Bit, isActive, null),
            };

            int rowsAffected = (int)SqlHelper.ExecuteCommand(query, CommandType.Text, SqlHelper.ExecuteType.ExecuteNonQuery, parameters);
            return rowsAffected;
        }


    }
}
