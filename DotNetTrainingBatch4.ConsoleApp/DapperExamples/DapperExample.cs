using Dapper;
using HHTDotNetCore.ConsoleApp.Dtos;
using HHTDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        private SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperExample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Run()
        {
            //Read();
            //Edit(11);
            //Create("title", "author", "content");
            //Update(2002, "title2", "author2", "content2");
            Delete(2002);
        }
        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from dbo.Table_1").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlodId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query("select * from dbo.Table_1 where blodId = @BlodId", new BlogDto { BlodId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data found!");
                return;
            }
            Console.WriteLine(item.BlodId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            string query = @"INSERT INTO [dbo].[Table_1]
       ([BlogTitle]
           , [BlogAuthor]
           , [BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.Write(message);

        }
        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlodId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            string query = @"UPDATE [dbo].[Table_1]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlodId = @BlodId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.Write(message);
        }

        private void Delete(int id)
        {
            var item = new BlogDto { BlodId = id };
            string query = @"delete from [dbo].[Table_1] where BlodId = @BlodId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.Write(message);

        }
    }


}
