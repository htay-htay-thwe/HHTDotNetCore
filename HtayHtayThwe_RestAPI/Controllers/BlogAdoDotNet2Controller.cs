using Dapper;
using HHTDotNetCore.shared;
using HtayHtayThwe_RestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace HtayHtayThwe_RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        //private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        private readonly AdoDotNetService _doDotNetService;

        public BlogAdoDotNet2Controller(AdoDotNetService doDotNetService)
        {
            _doDotNetService = doDotNetService;
        }

        [HttpGet]
            public IActionResult GetBlogs()
            {
            string query = "select * from Table_1";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            //List<BlogModel> lst = new List<BlogModel>();
            //foreach(DataRow dr in dt.Rows)
            //{
            //    BlogModel blog =new BlogModel();
            //    blog.BlodId = Convert.ToInt32(dr["BlodId"]);
            //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //    lst.Add(blog);

            //}
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
            BlodId = Convert.ToInt32(dr["BlodId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
           
        }).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Table_1 where BlodId=@BlodId";
            //AdoDotNetParameter[]  parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlodId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, new AdoDotNetParameter("@BlodId", id));

            var item = _adoDotNetService.QueryFitstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id));
            //SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@BlodId", id);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sqlDataAdapter.Fill(dt);

            //connection.Close();
            if(item is null)
            {
                return NotFound("no data!");
            }

            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Table_1]
       ([BlogTitle]
           , [BlogAuthor]
           , [BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
             SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Create Successful." : "Create Failed.";
            //return statuscode(500,message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Table_1]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlodId = @BlodId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlodId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                return NotFound("No data found for the given ID.");
            }
            return Ok("update success!");

        }

      
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle,";
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogAuthor] = @BlogTitle,";
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogContent] = @BlogTitle,";
            }
            if (conditions.Length < 0)
            {
                return NotFound("No data to update.");
            }


            string query = $@"UPDATE [dbo].[Table_1]
   SET {conditions}
 WHERE BlodId = @BlodId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            var ID = cmd.Parameters.AddWithValue("@BlodId", id);
            connection.Close();
            if (ID is null)
            {
                return NotFound("No data found.");

            }
            return Ok("patch success!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"delete from [dbo].[Table_1] where BlodId = @BlodId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            var ID = cmd.Parameters.AddWithValue("@BlodId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();


            if (ID is null)
            {
                return NotFound("No data found.");
            }
            return Ok(result);
        }

    }
}
