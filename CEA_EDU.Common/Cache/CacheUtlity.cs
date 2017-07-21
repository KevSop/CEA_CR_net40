using System;
using System.Collections;
using System.Web;


namespace CEA_EDU.Common
{
    public class CacheUtlity
    {
        protected static Object _LockObj = new object();

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            lock (_LockObj)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(CacheKey, objObject);
            }
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            lock (_LockObj)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
            }
        }

        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveOneCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            if (_cache.Count > 0)
            {
                ArrayList al = new ArrayList();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
                foreach (string key in al)
                {
                    _cache.Remove(key);
                }
            }
        }

        /// <summary>
        /// 以列表形式返回已存在的所有缓存
        /// </summary>
        /// <returns></returns>
        public static ArrayList ShowAllCache()
        {
            ArrayList al = new ArrayList();
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            if (_cache.Count > 0)
            {
                IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
            }
            return al;
        }
    }
}
