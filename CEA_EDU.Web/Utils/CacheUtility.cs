using System;
using System.Web;

namespace CEA_EDU.Web.Utils
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public class CacheUtility
    {
        private int expireMin = 0;//缓存过期时间,单位秒
        private Object objCacheObject = new object();//缓存对象
        private string cacheKey = "";//缓存键名称
        //加了一个静态对象
        protected static Object _LockObj = new object();
        public CacheUtility()
        {
        }

        public CacheUtility(string cacheKey)
        {
            this.cacheKey = cacheKey;
        }

        /// <summary>
        /// 设置/获得缓存项目。
        /// </summary>
        public string CacheKey
        {
            get { return cacheKey; }
            set { cacheKey = value; }
        }

        /// <summary>
        /// 设置缓存过期时间间隔。
        /// </summary>
        public int ExpireMin
        {
            get { return expireMin; }
            set { expireMin = value; }
        }

        /// <summary>
        /// 保存对象到缓存中。
        /// </summary>
        public void SetCache(object objContent)
        {
            if (CheckParameter() == false) return;
            //lock (objCacheObject)
            //{
            DateTime Dt = DateTime.Now;
            System.Web.HttpContext.Current.Cache.Insert(cacheKey, objContent, null, Dt.AddMinutes(expireMin), System.TimeSpan.Zero);
            //System.Web.HttpContext.Current.Cache.Insert(cacheKey + "_UpdateTime", Dt.ToString(), null, Dt.AddMinutes(expireMin), System.TimeSpan.Zero);
            //System.Web.HttpContext.Current.Cache.Insert(cacheKey + "_LostDateTime", Dt.AddSeconds(expireMin), null, Dt.AddMinutes(expireMin), System.TimeSpan.Zero);
            //}
        }

        public void SetCache(object objContent,int sExpireMin)
        {
            if (CheckParameter() == false) return;
            //lock (objCacheObject)
            //{
            DateTime Dt = DateTime.Now;
            System.Web.HttpContext.Current.Cache.Insert(cacheKey, objContent, null, Dt.AddMinutes(sExpireMin), System.TimeSpan.Zero);
            //System.Web.HttpContext.Current.Cache.Insert(cacheKey + "_UpdateTime", Dt.ToString(), null, Dt.AddMinutes(expireMin), System.TimeSpan.Zero);
            //System.Web.HttpContext.Current.Cache.Insert(cacheKey + "_LostDateTime", Dt.AddSeconds(expireMin), null, Dt.AddMinutes(expireMin), System.TimeSpan.Zero);
            //}
        }

        /// <summary>
        /// 从缓存中取出对象。
        /// </summary>
        public object GetCache()
        {
            if (CheckParameter() == false) return null;
            if (System.Web.HttpContext.Current.Cache[cacheKey] != null)
            {
                return System.Web.HttpContext.Current.Cache[cacheKey];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从缓存中清空对象。
        /// </summary>
        public void Clear()
        {
            if (CheckParameter() == false) return;
            lock (objCacheObject)
            {
                if (System.Web.HttpContext.Current.Cache[cacheKey] != null)
                {
                    System.Web.HttpContext.Current.Cache.Remove(cacheKey);
                }
                //if (System.Web.HttpContext.Current.Cache[cacheKey + "_UpdateTime"] != null)
                //{
                //    System.Web.HttpContext.Current.Cache.Remove(cacheKey + "_UpdateTime");
                //}
                //if (System.Web.HttpContext.Current.Cache[cacheKey + "_LostDateTime"] != null)
                //{
                //    System.Web.HttpContext.Current.Cache.Remove(cacheKey + "_LostDateTime");
                //}
            }
        }

        /// <summary>
        /// 缓存对象是否有效。
        /// </summary>
        public bool ValidCache()
        {
            if (CheckParameter() == false) return false;

            if (System.Web.HttpContext.Current.Cache[cacheKey] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查缓存参数
        /// </summary>
        public bool CheckParameter()
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                return false;
            }
            return true;
        }

        ///// <summary>
        ///// 获得缓存最后更新时间。
        ///// </summary>
        //public string LastUpdatetime
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Cache[cacheKey + "_UpdateTime"] != null)
        //        {
        //            return System.Web.HttpContext.Current.Cache[cacheKey + "_UpdateTime"].ToString();
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}

        ///// <summary>
        ///// 读取缓存过期时间。
        ///// </summary>
        //public string LostDateTime
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Cache[cacheKey + "_LostDateTime"] != null)
        //        {
        //            return System.Web.HttpContext.Current.Cache[cacheKey + "_LostDateTime"].ToString();
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}
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
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            lock (_LockObj)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
            }
        }
    }
}