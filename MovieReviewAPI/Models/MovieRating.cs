using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.Models
{
    public partial class MovieRating
    {
        public int MovieRatingId { get; set; }
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
