using newsSite.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace newsSite.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumbnail { get; set; }
        public DateTime DateTime { get; set; }
        public int TotalViews { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
