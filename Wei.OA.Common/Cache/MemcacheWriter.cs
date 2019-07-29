/*============================================
*类名称：MemcacheWriter
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Common.Cache
{
    using Memcached.ClientLibrary;

    /// <summary>
    /// MemcacheWriter
    /// </summary>
    public class MemcacheWriter:ICacheWriter
    {
        private MemcachedClient memcachedClient;

        public MemcacheWriter()
        {           
            //分布Memcached服务IP 端口
            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];
            string[] servers = strAppMemcachedServer.Split(',');

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
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
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;

            this.memcachedClient = mc;
        }

        public void AddCache(string key, object value)
        {
            this.memcachedClient.Add(key, value);
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            this.memcachedClient.Add(key, value, expDate);
        }

        public object GetCache(string key)
        {
            return this.memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)this.memcachedClient.Get(key);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            this.memcachedClient.Set(key, value, expDate);
        }

        public void SetCache(string key, object value)
        {
            this.memcachedClient.Set(key, value);
        }
    }
}
