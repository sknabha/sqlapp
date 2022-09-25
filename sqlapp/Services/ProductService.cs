using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "my-appsql.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Sandeep@123s";
        private static string db_database = "myappsql";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection con = GetConnection();
            List<Product> products = new List<Product>();
            string query = "select ProductId,ProductName,Quantity from Products";
            con.Open();

            SqlCommand cmd=new SqlCommand(query, con);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }
            }
            con.Close();
            return products;
        }

    }
}
