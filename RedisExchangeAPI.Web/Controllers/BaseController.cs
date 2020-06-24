using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly RedisService redisService;
        protected IDatabase db;

        public BaseController(RedisService redisService,int dbCount)
        {
            this.redisService = redisService;
            db = this.redisService.GetDb(dbCount);
        }
         
    }
}
