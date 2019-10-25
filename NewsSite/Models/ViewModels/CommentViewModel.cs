using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace newsSite.Models.ViewModels
{
    public class CommentViewModel
    {
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
