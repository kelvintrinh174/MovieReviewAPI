using System;
using System.Collections.Generic;

namespace MovieReviewAPI.Models
{
    public partial class MovieComment
    {
        public int MovieCommentId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
