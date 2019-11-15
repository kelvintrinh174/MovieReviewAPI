using System;
using System.Collections.Generic;

namespace MovieReviewAPI.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieComment = new HashSet<MovieComment>();
        }

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public int? Rating { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<MovieComment> MovieComment { get; set; }
    }
}
