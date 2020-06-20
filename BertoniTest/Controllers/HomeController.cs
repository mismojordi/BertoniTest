using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BertoniTest.Models;
using Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using BertoniTest.Extensions;
using Microsoft.Extensions.Configuration;

namespace BertoniTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger,IHttpClient httpClient,IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _httpClient.GetAsync<IEnumerable<Album>>(new Uri(_configuration["Services:UrlAlbums"]));
            var model = new AlbumModel()
            {
                Albums = GetSelectAlbumItem(albums)
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<SelectListItem> GetSelectAlbumItem(IEnumerable<Album> albums)
        {
            var albumsModel = new List<SelectListItem>();
           foreach(var album in albums)
            {
                albumsModel.Add(new SelectListItem()
                {
                    Value = album.id.ToString(),
                    Text = album.title
                });
            }
            return albumsModel;
        }

        public async Task<IActionResult> GetAlbum(int id)
        {
            var vista = string.Empty;
            var sucess = false;
            var message = string.Empty;
            try
            {
                var photosTemp = await _httpClient.GetAsync<IEnumerable<Photo>>(new Uri(string.Format(_configuration["Services:UrlPhotos"],id)));
                vista = await this.RenderViewAsync("_Photo", photosTemp, true);
                sucess = true;
                photosTemp = null;
            } catch(Exception e)
            {
                message = e.Message;
            }

            return Json(new { sucess, vista, message });
        }
    }
}
