using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages.Users
{
    public class LoginModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        [BindProperty]
        public User user { get; set; }
        public LoginModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAsync()
        {
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string json;
                HttpResponseMessage response;
                Console.WriteLine(user);
                //user.DateCreated = DateTime.Now.Date;
                json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("api/Users/Login", content);

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