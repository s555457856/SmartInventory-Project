using Microsoft.Data.Sqlite;
using SmartInventory.Models;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SmartInventory.Data
{
    public static class DbHelper
    {
        private static string connStr = "Data Source=inventory.db";

        //建立資料表
        public static void InitDb()
        {
            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = """
                        CREATE TABLE if not exists  Products (
                    	Id	INTEGER PRIMARY KEY AUTOINCREMENT,
                    	Name TEXT,                    	
                    	Category TEXT,
                    	Quantity INTEGER,
                    	Price REAL
                    	);
                    """;

                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //新增資料
        public static void InsertProduct(Product p)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = """
                    insert into Products(Name,Category,Quantity,Price) values
                    (@Name,@Category,@Quantity,@Price);
                    """;
                    





                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", p.Name);
                    cmd.Parameters.AddWithValue("@Category", p.Category);
                    cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
                    cmd.Parameters.AddWithValue("@Price", (double)p.Price);
                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static List<Product> GetAllProducts()
        {
            var result = new List<Product>();

            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = "select * from Products";

                using (var cmd = new SqliteCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var p = new Product();

                            // CTRL+K+C CTRL+K+U
                            p.Id = Convert.ToInt32(reader["Id"]);
                            p.Name = reader["Name"].ToString()!;
                            p.Category = reader["Category"].ToString()!;
                            p.Quantity = Convert.ToInt32(reader["Quantity"]);
                            p.Price = Convert.ToDecimal(reader["Price"]);

                            result.Add(p);
                        }
                    }
                }
            }


            return result;
        }

      


        // 刪除
        public static void DeleteProduct(Product p)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = "delete from Products where id=@id";
                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // 刪除全部
        public static void DeleteAllItem()
        {
            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = "delete from item";
                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // 修改      
        public static void UpdateProducts(Product p)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                string sql = """
                    update Products set
                    Name=@Name,
                    Category=@Category,
                    Quantity=@Quantity,
                    Price=@Price                    
                    where Id=@Id
                    """;

                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", p.Name);
                    cmd.Parameters.AddWithValue("@Category", p.Category);
                    cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
                    cmd.Parameters.AddWithValue("@Price", (double)p.Price);
                    cmd.Parameters.AddWithValue("@Id", p.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
