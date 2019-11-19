using System;

namespace MovieReviewAPI.Models
{
    public class MovieComment
    {
        public int MovieCommentId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        public string Comment { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}