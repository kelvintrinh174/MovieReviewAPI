using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MovieReviewAPI.Models;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        [BindProperty]
        public Movie movie { get; set; }

        public CreateModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAsync() {
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try {
                string json;
                HttpResponseMessage response;
               // movie.DateCreated = DateTime.Now.ToString("yyyy/MM/dd");
                Console.WriteLine(movie.DateReleased.Date);
                DateTime released = (DateTime)movie.DateReleased;
                ////add a new item
                //TodoItem item = new TodoItem { Id = 6, Name = "Lab#5", IsComplete = false };
                movie.DateCreated = DateTime.Now.Date;
                movie.DateReleased = movie.DateReleased.Date;
                json = JsonConvert.SerializeObject(movie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("api/Movies", content);

                //   response = await client.PostAsJsonAsync("/api/TodoItems", item);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("todo item has been inserted " + json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}