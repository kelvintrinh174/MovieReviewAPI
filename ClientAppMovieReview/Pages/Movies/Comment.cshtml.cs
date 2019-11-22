using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MovieReviewAPI.Models;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages.Movies
{
    public class CommentModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        private string _apiKey;
        public User _loggedInUser;
        [BindProperty]
        public MovieComment movieComment { get; set; }
        public CommentModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        public async Task OnGetAsync(int? id)
        {
            if (HttpContext.Session.IsAvailable)
            {
                var userString = HttpContext.Session.GetString("userString");
                if (userString == null)
                {
                    Console.WriteLine("User is not loggedIn");
                }
                else
                {
                    _loggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userString"));
                }
            }
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                // get the specified item
                response = await _client.GetAsync("moviereview/api/MovieComments/" + id);

                if (response.IsSuccessStatusCode)
                {
                    movieComment = await response.Content.ReadAsAsync<MovieComment>();
                }

                else Console.WriteLine("Internal Server error");
               // return RedirectToPage("/Movies/View", new { id = movieComment.MovieId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               
            }

        }
        public async Task<IActionResult> OnPostCommentAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                movieComment.DateCreated = DateTime.Now.Date;
                json = JsonConvert.SerializeObject(movieComment);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PutAsync("moviereview/api/MovieComments/"+movieComment.MovieCommentId, content);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "Comment has been updates Successfully";
                return RedirectToPage("/Movies/View", new { id = movieComment.MovieId });
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message;
                return RedirectToPage("/Movies/View", new { id = movieComment.MovieId });
            }
        }
    }
}