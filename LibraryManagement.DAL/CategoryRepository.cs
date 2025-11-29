using LibraryManagement.DTO;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class CategoryRepository
    {
        public static List<CategoryDTO> GetAllCategories()
        {
            string query = @"SELECT * FROM Categories;";
            List<CategoryDTO> categories = new();
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,null);
            while (reader.Read())
            {
                categories.Add(
                    new CategoryDTO
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                    }
                    );
            }
            return categories;
        }

        public static CategoryDTO GetCategoryByID(int categoryID)
        {
            string query = @"SELECT * FROM Categories WHERE CategoryID=@categoryID;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["CategoryID"] = (SqlDbType.Int, categoryID, null),
            };
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
             if (reader!=null && reader.Read())
            {
                return new CategoryDTO
                {
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"] == DBNull.Value ? "" : reader["Description"].ToString(),
                };
            }
             return null;
        }

    }
}
