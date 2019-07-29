namespace Wei.OA.Common.Cache
{
    using System;

    public interface ICacheWriter
    {
        void AddCache(string key, object value);

        void AddCache(string key, object value, DateTime expDate);

        object GetCache(string key);

        T GetCache<T>(string key);

        void SetCache(string key, object value, DateTime expDate);

        void SetCache(string key, object value);
    }
}