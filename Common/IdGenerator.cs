using System.Collections.Generic;

namespace Common
{
    public static class IdGenerator
    {
        private static readonly Dictionary<string, long> Ids = new Dictionary<string, long>();
        private static readonly object GeneratorLock = new object();

        public static long Next(string key) {
            lock (GeneratorLock)
            {
                if (!Ids.ContainsKey(key))
                {
                    Ids.Add(key, 1L);
                    return 1L;
                }
                var id = Ids[key]++;
                Ids.Add(key, id);
                return id;
            }
        }
    }
}