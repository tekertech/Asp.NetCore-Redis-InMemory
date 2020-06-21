using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //if(memoryCache.TryGetValue("zaman", out object result))
            //{
                MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions();
            // memoryCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

            memoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(10);

                memoryCache.Set<string>("zaman", DateTime.Now.ToString(),memoryCacheEntryOptions);
            //}
            return View();
        }

        public IActionResult Show()
        {
            memoryCache.TryGetValue("zaman", out string zamanCache);
            ViewBag.zaman = zamanCache;
            return View();
        }
    }
}
