using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemcacheDemo
{
    using Memcached.ClientLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            //分布Memcached服务IP 端口
            string[] servers = { "192.168.43.26:11211"};
            //初始化池
            SockIOPool pool =SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc=new MemcachedClient();
            mc.EnableCompression = false;
            mc.Add("key3","123");
        }
    }
}
