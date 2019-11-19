using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.DTOModels
{
    public class MovieRatingDto
    {
        public int MovieRatingId { get; set; }
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }
    }
}