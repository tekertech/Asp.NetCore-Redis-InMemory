using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DistributedCachedRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DistributedCachedRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDistributedCache distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            Product product = new Product { Id = 1, Name = "Mehmet", Price = 10 };
            string jsonProduct = JsonConvert.SerializeObject(product);
            distributedCache.SetStringAsync($"product:{product.Id.ToString()}", jsonProduct,options);

            return View();
        }

        public IActionResult Show()
        {
            string product = distributedCache.GetString("product:1");
            ViewBag.product = product;
            return View();
        }

        public IActionResult Remove()
        {
            distributedCache.Remove("product:1");
            return View();
        }

    }
}
