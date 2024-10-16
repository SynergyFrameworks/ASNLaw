using System;
using System.Collections.Generic;

namespace Domain.Parse.Extensions
{
    public static class List
    {

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T t in list)
            {
                action(t);
                yield return t;
            }
        }
    }
}