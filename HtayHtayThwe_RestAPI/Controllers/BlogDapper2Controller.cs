using Azure;
using Dapper;
using HHTDotNetCore.shared;
using HtayHtayThwe_RestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection.Metadata;

namespace HtayHtayThwe_RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        //private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        private readonly DapperService _dapperService;

        public BlogDapper2Controller(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        [HttpGet]
        public IActionResult GetBlogs() {
            string query = "select * from dbo.Table_1";

            var lst2 = _dapperService.Query<BlogModel>(query);
            return Ok(lst2);
        }



        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");

            }
            return Ok();
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
           int result = _dapperService.Execute(query,blog);
            string message = result > 0 ? "create Successful." : "create Failed.";
            Console.Write(message);
            return Ok();
        }



        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");

            }
            string query = @"UPDATE [dbo].[Table_1]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlodId = @BlodId";
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.Write(message);
            return Ok("Update");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");

            }
            string conditions = string.Empty;
           if(!string.IsNullOrEmpty(blog.BlogTitle))
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
            if(conditions.Length > 0)
            {
                return NotFound("No data to update.");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            string query = $@"UPDATE [dbo].[Table_1]
   SET {conditions}
 WHERE BlodId = @BlodId";
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.Write(message);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");

            }
            string query = @"delete from [dbo].[Table_1] where BlodId = @BlodId";
            int result = _dapperService.Execute(query);
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.Write(message);
            return Ok();
        }

        private BlogModel? FindById(int id)
        {

            string query = "select * from dbo.Table_1 where blodId = @BlodId";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlodId = id });
            return item;
        }
    }
}
