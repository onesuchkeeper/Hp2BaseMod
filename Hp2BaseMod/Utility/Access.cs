// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.Utility
{
    /// <summary>
    /// Utility for accessing properties of objects
    /// </summary>
    public static class Access
    {
        /// <summary>
        /// Sets a target to the nullable's value only if the nullable isn't null
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="target">target</param>
        /// <param name="nullable">nullable</param>
        public static void NullableSet<T>(ref T target, Nullable<T> nullable)
            where T : struct
        {
            if (nullable.HasValue) { target = nullable.Value; }
        }

        /// <summary>
        /// Sets target to value only if value isn't null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void NullSet<T>(ref T target, T value)
            where T : class
        {
            if (value != null) { target = value; }
        }
    }
}
