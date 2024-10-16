namespace Domain.Parse
{
	using System;
	using System.Threading;

	public static class ThreadRandom
    {
        public static Random NewRandom()
        {
            lock (_g_Lock) //...not thread safe, so we must synchronize
            {
                return new Random(_g_Random.Next());
            }
        }

        public static Random Instance { get => _threadRandom.Value; }

        private static readonly Random _g_Random = new Random(); //...global Random used to guarantee thread-local instances are seeded differently
        private static readonly object _g_Lock = new object(); //...Random is not thread-safe on Windows so use this to synchronize access

        private static readonly ThreadLocal<Random> _threadRandom = new ThreadLocal<Random>(NewRandom); //...static per thread
    } 
}
