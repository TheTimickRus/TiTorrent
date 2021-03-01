using System;
using System.Collections.Concurrent;

namespace TiTorrent.UWP.Helpers
{
    public static class Singleton<T> where T : new()
    {
        private static readonly ConcurrentDictionary<Type, T> Instances = new();

        public static T Instance
        {
            get
            {
                return Instances.GetOrAdd(typeof(T), t => new T());
            }
        }
    }
}
