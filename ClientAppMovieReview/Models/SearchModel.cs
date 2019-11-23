using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientAppMovieReview.Models
{
    public class SearchModel
    {
        [Required]
        public string Keyword;
    }
}
