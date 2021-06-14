using System;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Core.Caching
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        T Get<T>(string key, Func<T> acquire, int? cacheTime = null);

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        void Remove(string key);

        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void Clear();
        /// <summary>
        /// Get Async By Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);
        /// <summary>
        /// PerformActionWithLock
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="expirationTime"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
    }
}
