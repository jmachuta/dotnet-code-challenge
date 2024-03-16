using System.Collections.Generic;

namespace CodeChallenge.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// A null-safe way to iterate through a list
        /// </summary>
        /// <typeparam name="T">Type of object in the list</typeparam>
        /// <param name="list">The list</param>
        /// <returns>An empty list if the list is null, otherwise will return the same list passed in</returns>
        public static List<T> AsNullSafe<T>(this List<T> list)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            return list;
        }
    }
}
