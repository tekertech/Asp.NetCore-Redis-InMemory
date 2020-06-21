using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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

            return View();
        }
    }
}
