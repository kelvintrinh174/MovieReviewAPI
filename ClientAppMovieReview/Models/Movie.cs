using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.Models
{
    public partial class Movie
    {
        public Movie()
        {
          //  MovieComment = new HashSet<MovieComment>();
            MovieRating = new HashSet<MovieRating>();
        }

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        //public string DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReleased { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime ReleasedOn { get; set; }
        public string Genre { get; set; }
        public string Actor { get; set; }
        public string MovieImage { get; set; }
        public string MovieVideo { get; set; }

        public virtual List<MovieComment> MovieComment { get; set; }
        //public virtual ICollection<MovieComment> MovieComment { get; set; }
        public virtual ICollection<MovieRating> MovieRating { get; set; }
    }
}
