using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HHTDotNetCore.ConsoleApp.AdoDotNetExamples
{
public class AdoDotNetExample
    {
        //private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = "DESKTOP-QNI7OO1",
        //    InitialCatalog = "DotNEtTrainingBatch4",
        //    UserID = "sa",
        //    Password = "sasa@123",
        //};

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public AdoDotNetExample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            this.sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Read()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();

            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from table_1";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id =>" + dr["BlodId"]);
                Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
                Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
                Console.WriteLine("--------------------------");

            }
        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Table_1]
       ([BlogTitle]
           , [BlogAuthor]
           , [BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.Write(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Table_1]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlodId = @BlodId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlodId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.Write(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"delete from [dbo].[Table_1] where BlodId = @BlodId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlodId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.Write(message);

        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = "select * from table_1 where BlodId = @BlodId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlodId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            DataRow dr = dt.Rows[0];
        }
    }
}
