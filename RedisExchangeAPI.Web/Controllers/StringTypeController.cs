using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService redisService;
        public IDatabase db { get; set; }
        public StringTypeController(RedisService redisService)
        {
            db = redisService.GetDb(0);
            this.redisService = redisService;
        }

        public IActionResult Index()
        {
            /*
             Redis-server da db0 al ve onun üzerinde işlem yapalım.
             */
            
            db.StringSet("name", "Mehmet Teker");
            db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            ViewBag.name = db.StringGet("name");
            db.StringIncrement("ziyaretci", 1);
            ViewBag.ziyaretci = db.StringGet("ziyaretci");
            return View();
        }
    }
}
