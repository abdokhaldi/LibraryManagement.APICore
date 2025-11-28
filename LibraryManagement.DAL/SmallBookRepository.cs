using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Data;


namespace DAL_LibraryManagement
{
    public class SmallBookRepository
    {
        public static List<SmallBookDTO> GetSmallBookAutoSearch(string searchTerm)
        {
            List<SmallBookDTO> books = new();
            string query = @"SELECT * FROM SmallBooks WHERE Title LIKE @title;";
            var paramerers = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@title"] = (SqlDbType.NVarChar,searchTerm,null),
            };
            using var reader = SqlHelper.ExecuteReaderWildCard(query,CommandType.Text,paramerers);
            if (reader == null)
            {
                books = null;
                return books;
            }
            while (reader != null&& reader.Read())
            {
                books.Add
                (
                    new SmallBookDTO
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        Title = reader["Title"].ToString()
                    }
                );
            }
            return books;
        }
    }
}
