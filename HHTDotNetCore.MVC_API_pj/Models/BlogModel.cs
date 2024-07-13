using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTDotNetCore.MVC_API_pj.Models
{
    [Table("Table_1")]
    public class BlogModel
    {
        [Key]
        public int BlodId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
    //public record BlogEntity(int BlodId, string BlogTitle, string BlogContent, string BlogAuthor);
}
