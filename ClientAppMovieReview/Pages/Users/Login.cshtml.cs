using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientAppMovieReview.Pages.Users
{
    public class LoginModel : PageModel
    {
        public User user { get; set; }
        public void OnGet()
        {

        }
    }
}