using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure.Common.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>Indicates whether the specified enumerable is null or has a length of zero.</summary>
        /// <param name="data">The data to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null || !data.Any();
        }

        public static IEnumerable<IEnumerable<T>> Paginate<T>(this IEnumerable<T> items, int pageSize)
        {
            var page = new List<T>();
            foreach (var item in items)
            {
                page.Add(item);
                if (page.Count >= pageSize)
                {
                    yield return page;
                    page = new List<T>();
                }
            }
            if (page.Count > 0)
            {
                yield return page;
            }
        }

        /// <summary>
        /// Performs the indicated action on each item.
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        /// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
        public static void Apply<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs the indicated action on each item. Boxing free for <c>List+Enumerator{T}</c>.
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        /// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
        public static void Apply<T>(this List<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs the indicated action on each key-value pair.
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        /// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
        public static void Apply(this System.Collections.IDictionary items, Action<object, object> action)
        {
            foreach (var key in items.Keys)
            {
                action(key, items[key]);
            }
        }

        public static int GetOrderIndependentHashCode<T>(this IEnumerable<T> source)
        {
            int hash = 0;
            //Need to force order to get  order independent hash code
            foreach (T element in source.OrderBy(x => x, Comparer<T>.Default))
            {
                hash = hash ^ EqualityComparer<T>.Default.GetHashCode(element);
            }
            return hash;
        }

        public static List<T> ConvertList<T>(this IEnumerable list, IList<string> propertiesToSkip = null)
        {
            var newList = Activator.CreateInstance<List<T>>();
            newList.AddRange(from object item in list select item.Convert<T>(propertiesToSkip));
            return newList;
        }


        public static IList<T> MergeList<T>(this IList<T> mergeeList, IEnumerable mergerCollection)
        {
            // Items must have Id values
            // If item is in merger list but not mergee list, add it
            // If item is in both lists, update it and add it.
            if (mergerCollection == null)
                return EmptyList(mergeeList);

            var mergerList = mergerCollection as IList<object> ?? mergerCollection.Cast<object>().ToList();

            if (!mergerList.Any())
                return EmptyList(mergeeList);

            var newList = Activator.CreateInstance<List<T>>();
            if (mergeeList == null)
            {
                mergeeList = Activator.CreateInstance<List<T>>();
            }

            var newMergeeList = new Dictionary<string, T>();
            var newMergerList = new List<string>();
            foreach (var destinationItem in mergeeList)
            {
                var idInfo = destinationItem.GetType().GetProperty("Id");
                var id = idInfo != null ? idInfo.GetValue(destinationItem, null) : null;
                var guidId = id as Guid?;
                if (id == null || guidId == Guid.Empty)
                    throw new InvalidDataException("Cannot merge properties without Ids");

                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    newMergeeList.Add(id.ToString(), destinationItem);
                }

            }
            foreach (var originItem in mergerList)
            {
                var idInfo = originItem.GetType().GetProperty("Id");
                var isNewItem = true;
                if (idInfo != null)
                {
                    var id = idInfo.GetValue(originItem, null).BlankIfNull().ToString();
                    if (!string.IsNullOrEmpty(id) && newMergeeList.ContainsKey(id))
                    {
                        newMergerList.Add(id);
                        var oldItem = newMergeeList[id];
                        oldItem.UpdateObject(originItem);
                        isNewItem = false;
                    }
                }
                if (isNewItem)
                {
                    var newItem = (T)Activator.CreateInstance(typeof(T));
                    newItem.UpdateObject(originItem);
                    newList.Add(newItem);
                }

            }
            foreach (var mergeeItem in newMergeeList.Where(mergeeItem => !newMergerList.Contains(mergeeItem.Key)))
            {
                mergeeList.Remove(mergeeItem.Value);
            }
            foreach (var newItem in newList)
            {
                mergeeList.Add(newItem);
            }
            return mergeeList;
        }

        private static IList<T> EmptyList<T>(IList<T> mergeeList)
        {
            if (mergeeList == null)
                return null;
            mergeeList.Clear();
            return mergeeList;
        }
    }
}
