using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;
using ViewType = MvcMusicStore.ViewModels.ViewType;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index(ViewType? type)
        {
            // Get most popular albums
            var albums = type == ViewType.TopViewed ? GetTopViewedAlbums(5) : GetTopSellingAlbums(5);

            var mainImage = GetMainImage();

            ViewBag.MainImageUrl = mainImage;
            ViewBag.ViewType = type;
            return View(albums);
        }

        public string GetMainImage()
        {
            var imageCount = storeDB.MainImages.Count();
            var random = new Random();
            var imageId = random.Next(imageCount);

            var image = storeDB.MainImages.Find(imageId+1);
            return image != null ? image.ImageUrl : "/Content/Images/main01.jpg";
        }

        private List<Album> GetTopViewedAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.ViewCount)
                .Take(count)
                .ToList();
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}