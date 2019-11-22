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
    public class ViewModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        private string _apiKey;
        public User _loggedInUser;
        [BindProperty]
        public Movie movie { get; set; }
        [BindProperty]
        public MovieRating rating { get; set; }
        [BindProperty]
        public MovieComment movieComment { get; set; }

        public ViewModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        //https://localhost:44387/Movies/View?id=1
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
                response = await _client.GetAsync("moviereview/api/Movies/" + id);

                if (response.IsSuccessStatusCode)
                {
                    movie = await response.Content.ReadAsAsync<Movie>();
                    //rating.MovieId = movie.MovieId;
                }

                else Console.WriteLine("Internal Server error");
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message.ToString();
            }
        }

        public async Task<IActionResult> OnPostAsync() {
            Console.WriteLine(rating);
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                rating.DateCreated = DateTime.Now.Date;
                json = JsonConvert.SerializeObject(rating);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("moviereview/api/MovieRatings", content);

                //   response = await client.PostAsJsonAsync("/api/TodoItems", item);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "Rated Successfully";
                return RedirectToPage(new { id = rating.MovieId });
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message;
                return RedirectToPage(new { id = rating.MovieId });
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
                response = await _client.PostAsync("moviereview/api/moviecomments", content);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "Comment has been posted Successfully";
                return RedirectToPage(new { id = movieComment.MovieId});
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message;
                return RedirectToPage();
            }
        }
        public async Task<IActionResult> OnGetDeleteAsync(int? id)
        {
            if (id == null)
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
                response = await _client.DeleteAsync("moviereview/api/moviecomments/"+id);
                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                MovieComment _m = JsonConvert.DeserializeObject<MovieComment>(json);
                TempData["successmsg"] = "Comment has been deleted Successfully";
                return RedirectToPage(new { id = _m.MovieId });
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message;
                return RedirectToPage("/Index");
            }
        }
    }
}