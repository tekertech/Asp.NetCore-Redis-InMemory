using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
        private readonly string redisHost;
        private readonly string redisPort;
        /*
         Redis db lerinde birden fazla metotu kullanmamıza olanak sağlar.
         */
        public IDatabase db { get; set; }

        private ConnectionMultiplexer redis;

        /*
         Redis-server a ait host ve port bilgilerini almak.
         */
        public RedisService(IConfiguration configuration)
        {
            redisHost = configuration["Redis:Host"];
            redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var configString = $"{redisHost}:{redisPort}";
            redis = ConnectionMultiplexer.Connect(configString);
        }

        /*
         Redis-server tarafında hangi db e bağlanmak istersek parametre 
         olarak onu veririz. Örneğin Redis-server da 0-14 e kadar 15 adet
         db gelir. Bunu RedisDesktopManager programında görebilirsiniz.
         */
        public IDatabase GetDb(int db)
        {
            return redis.GetDatabase(db);
        }
    }
}
