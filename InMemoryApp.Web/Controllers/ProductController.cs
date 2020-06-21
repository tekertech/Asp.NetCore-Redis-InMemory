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
            //1. yol
            if (String.IsNullOrEmpty(memoryCache.Get<string>("zaman")))
            {
                memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            //2. yol

            if(memoryCache.TryGetValue("zaman", out object result))
            {
                memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }
            
            return View();
        }

        public IActionResult Show()
        {
            memoryCache.GetOrCreate<string>("zaman",  cacheEntry => {
                return DateTime.Now.ToString();
            }
            );
             
            ViewBag.zaman = memoryCache.Get("zaman");
            return View();
        }
    }
}
