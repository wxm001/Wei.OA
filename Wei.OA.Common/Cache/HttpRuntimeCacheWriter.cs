/*============================================
*类名称：HttpRuntimeCacheWriter
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
    using System.Web;

    /// <summary>
    /// HttpRuntimeCacheWriter
    /// </summary>
    public class HttpRuntimeCacheWriter : ICacheWriter
    {
        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key,value);
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Insert(key,value,null,expDate,TimeSpan.Zero);
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key,value,expDate);
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value);
        }
    }
}
