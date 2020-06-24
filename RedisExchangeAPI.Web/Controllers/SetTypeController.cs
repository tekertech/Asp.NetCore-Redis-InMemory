using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService redisService;
        public IDatabase db;
        private string listKey = "hashname";
        public SetTypeController(RedisService redisService)
        {
            this.redisService = redisService;
            db = this.redisService.GetDb(2);
        }

        public IActionResult Index()
        {
            HashSet<string> nameList = new HashSet<string>();

            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }

            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (!db.KeyExists(listKey))
            {
                db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            }
            db.SetRandomMembers(listKey, 6);

            //db.SetAdd(listKey, name);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SetRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}
