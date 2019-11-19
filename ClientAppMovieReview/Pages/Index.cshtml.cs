﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public IEnumerable<Movie> Movies;
        
        public IndexModel(IConfiguration iConfiguration) {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
        }
        public async Task OnGetAsync()
        {
            _client.BaseAddress = new Uri(_apiUrl);
            //_client.BaseAddress = new Uri("http://moviereviewapi-dev.us-east-1.elasticbeanstalk.com");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string json;
                HttpResponseMessage response;
                //get all items
                response = await _client.GetAsync("/api/movies");

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
