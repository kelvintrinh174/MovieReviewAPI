using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.Models
{
    public partial class MovieComment
    {
        public int MovieCommentId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Comment { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
