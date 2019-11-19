using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.DTOModels
{
    public class MovieCommentDto
    {
        public int MovieCommentId { get; set; }
        public string MovieTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        public string Comment { get; set; }
        public int MovieId { get; set; }
    }
}