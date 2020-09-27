using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using redlocktst.DistributedLockers;
using redlocktst.DistributedLockers.Redis;
using StackExchange.Redis;

namespace redlocktst.Ext
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("Redis");
            var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(s => RedLockFactory.Create(new List<RedLockMultiplexer>
            {
                redisConnectionMultiplexer
            }));
            services.Configure<RedLockOptions>(configuration.GetSection("RedLockOptions"));
            services.AddSingleton<IDistributedLocker, RedisDistributedLocker>();

            return services;
        }
    }
}