using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.DTOModels
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReleased { get; set; }
        public string Genre { get; set; }
        public string Actor { get; set; }
        public string MovieImage { get; set; }
        public string MovieVideo { get; set; }

        public ICollection<MovieCommentDto> MovieComment { get; set; }
        = new List<MovieCommentDto>();

        public ICollection<MovieRatingDto> MovieRating { get; set; }
        = new List<MovieRatingDto>();
    }
}
