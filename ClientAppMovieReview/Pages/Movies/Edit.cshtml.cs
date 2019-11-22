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
    public class EditModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        private string _apiKey;
        [BindProperty]
        public Movie movie { get; set; }
        public EditModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        public async Task OnGetAsync(int? id)
        {
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
                TempData["errormsg"] = e.Message;
            }
        }
        public async Task OnPostAsync()
        {
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                json = JsonConvert.SerializeObject(movie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PutAsync("moviereview/api/Movies/"+movie.MovieId, content);

                //   response = await client.PostAsJsonAsync("/api/TodoItems", item);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "Movie has been updated Successfully";
                Console.WriteLine("todo item has been inserted " + json);
            }
            catch (Exception e)
            {
                TempData["errormsg"] = e.Message;
            }
        }
    }
}