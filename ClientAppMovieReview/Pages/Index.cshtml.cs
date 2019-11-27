using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MovieReviewAPI.Models;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        private string _apiKey;
        public IEnumerable<Movie> Movies;
        public IEnumerable<Movie> LatestMovies;


        public IndexModel(IConfiguration iConfiguration) {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        public async Task OnGetAsync()
        {
            _client.BaseAddress = new Uri(_apiUrl);
         
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                //get all items
                response = await _client.GetAsync("moviereview/api/movies");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    Movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                    LatestMovies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                }
                else
                    Console.WriteLine("Internal Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task OnPostSearchAsync() {
            string type = Request.Form["type"];
            string keyword = Request.Form["keyword"];

            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                string url = "";

                //get all items
                HttpResponseMessage responseMovies = await _client.GetAsync("moviereview/api/movies");

                if (responseMovies.IsSuccessStatusCode)
                {
                    json = await responseMovies.Content.ReadAsStringAsync();
                    LatestMovies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                }
                else
                    Console.WriteLine("Internal Server error");

                if (type == "actor")
                {
                    url ="moviereview/api/movies/searchactor/" + keyword;
                }
                else if (type == "title")
                {
                    url = "moviereview/api/movies/searchtitle/" + keyword;
                }
                //get all items

                response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    Movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                }
                else
                    Console.WriteLine("Internal Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
