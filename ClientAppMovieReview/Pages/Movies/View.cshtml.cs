using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MovieReviewAPI.Models;

namespace ClientAppMovieReview.Pages.Movies
{
    public class ViewModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        [BindProperty]
        public Movie movie { get; set; }
        public ViewModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
        }
        //https://localhost:44387/Movies/View?id=1
        public async Task OnGetAsync(int? id)
        {
            Console.WriteLine(id);
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string json;
                HttpResponseMessage response;
                // get the specified item
                //int id = 4;
                //TodoItem item;
                response = await _client.GetAsync("api/Movies/" + id);

                if (response.IsSuccessStatusCode)
                {
                    movie = await response.Content.ReadAsAsync<Movie>();
                    //json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    //item = JsonConvert.DeserializeObject<TodoItem>(json);

                    Console.WriteLine("The return TodoItem details:\n " + movie);
                }

                else Console.WriteLine("Internal Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}