using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.DTOModels
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public int? Rating { get; set; }
        public string Genre { get; set; }

        //public virtual ICollection<MovieCommentDto> MovieComment { get; set; }
    }
}
