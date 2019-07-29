/*============================================
*类名称：CacheHelper
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
    using Spring.Context;
    using Spring.Context.Support;

    /// <summary>
    /// CacheHelper
    /// </summary>
    public class CacheHelper
    {
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            //通过容器创建对象，实现属性注入（因为属性是静态的，可以不创建实例就使用，但不创建实例属性就是空的）
            IApplicationContext context = ContextRegistry.GetContext();
            //context.GetObject("CacheHelper");//属性注入
            CacheHelper.CacheWriter=context.GetObject("CacheWriter") as ICacheWriter; //直接通过容器
        }

        public static void AddCache(string key, object value)
        {
            //往缓存写：单机，分布式--观察者模式可以；修改配置就可以切换，spring.net注入
            //ICacheWriter cacheWriter=new HttpRuntimeCacheWriter();做成属性，然后注入
            CacheWriter.AddCache(key,value);
        }

        public static void AddCache(string key, object value,DateTime expDate)
        {
            CacheWriter.AddCache(key,value,expDate);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key,value);
        }

        public static void SetCache(string key, object value,DateTime expDate)
        {
            CacheWriter.SetCache(key,value,expDate);
        }
    }
}
