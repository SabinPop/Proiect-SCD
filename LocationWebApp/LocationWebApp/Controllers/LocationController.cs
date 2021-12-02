using LocationWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using AutoMapper;
using LocationWebApp.Models.ViewModels;

namespace LocationWebApp.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public LocationController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = this.httpClientFactory.CreateClient("API");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var request = await httpClient.GetAsync("location/all");
            var locations = new List<Location>();
            if (request.IsSuccessStatusCode)
            {
                locations = JsonConvert.DeserializeObject<List<Location>>(await request.Content.ReadAsStringAsync());
            }
            return View(locations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationViewModel location)
        {
            var request = await httpClient.PostAsJsonAsync<Location>("location/create", mapper.Map<Location>(location));
            if (request.IsSuccessStatusCode)
            {
                return View("Details", location);
            }
            return View();
        }

        //[Route("Location/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var request = await httpClient.GetAsync("location/" + id);
            LocationViewModel location = null;
            if (request.IsSuccessStatusCode)
            {
                location = mapper.Map<LocationViewModel>(JsonConvert.DeserializeObject<Location>(await request.Content.ReadAsStringAsync()));
            }
            return View(location);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var request = await httpClient.GetAsync("location/" + id);
            Location location = null;
            if (request.IsSuccessStatusCode)
            {
                location = JsonConvert.DeserializeObject<Location>(await request.Content.ReadAsStringAsync());
            }
            return View(location);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Location location)
        {
            var request = await httpClient.PutAsJsonAsync<Location>("location/update", location);
            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { location.Id });
            }
            return View(location.Id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = await httpClient.GetAsync("location/" + id);
            Location location = null;
            if (request.IsSuccessStatusCode)
            {
                location = JsonConvert.DeserializeObject<Location>(await request.Content.ReadAsStringAsync());
            }
            return View(location);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Location location)
        {
            var request = await httpClient.DeleteAsync("location/" + location.Id);
            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(location);
        }
    }
}
