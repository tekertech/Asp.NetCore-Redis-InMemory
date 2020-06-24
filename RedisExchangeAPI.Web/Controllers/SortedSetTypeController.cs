using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService redisService;
        public IDatabase db;
        private string listKey = "sortedSetTypeName";
        public SortedSetTypeController(RedisService redisService)
        {
            this.redisService = redisService;
            db = this.redisService.GetDb(2);
        }

        public IActionResult Index()
        {
            HashSet<string> sortedList = new HashSet<string>();

            if (db.KeyExists(listKey))
            {
                //db.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                //    sortedList.Add(x.ToString());
                //});

                db.SortedSetRangeByRank(listKey, order: Order.Ascending).ToList().ForEach(x =>
                {
                    sortedList.Add(x.ToString());
                });

            }

            return View(sortedList);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            db.SortedSetAdd(listKey, name, score);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SetRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}
