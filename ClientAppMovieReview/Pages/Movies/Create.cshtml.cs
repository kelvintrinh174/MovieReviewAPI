using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private string _apiKey;
        [BindProperty]
        public Movie movie { get; set; }

        public CreateModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAsync() {
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try {
                string json;
                HttpResponseMessage response;
               // movie.DateCreated = DateTime.Now.ToString("yyyy/MM/dd");
                Console.WriteLine(movie.DateReleased.Date);
                DateTime released = (DateTime)movie.DateReleased;
                Console.WriteLine(movie);
                ////add a new item
                movie.DateCreated = DateTime.Now.Date;
                movie.DateReleased = movie.DateReleased.Date;
                json = JsonConvert.SerializeObject(movie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("moviereview/api/Movies", content);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "Movie has been added Successfully";

                Console.WriteLine("todo item has been inserted " + json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["errormsg"] = e.Message;
            }
        }
        public async Task OnPostUploadAsync(IFormFile formFile)
        {
            long size = formFile.Length;

                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                Console.WriteLine(filePath);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                Console.WriteLine(formFile);
            }
            

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

        }
    }
}