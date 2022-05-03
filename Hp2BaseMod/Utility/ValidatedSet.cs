// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Utility
{
    /// <summary>
    /// Sets value to target given propper conditions
    /// </summary>
    public static class ValidatedSet
    {
        public static void SetValue<T>(ref T target, T value, InsertStyle style)
        {
            if (value != null || style == InsertStyle.assignNull)
            {
                target = value;
            }
        }

        public static void SetValue<T>(ref T target, Nullable<T> value)
            where T : struct
        {
            if (value.HasValue)
            {
                target = value.Value;
            }
        }

        public static void SetValue<T>(ref T target, IGameDataInfo<T> info, InsertStyle style, GameDataProvider gameData, AssetProvider assetProvider)
        {
            if (info == null)
            {
                // don't do it if it's not nullable
                var type = typeof(T);
                if (style == InsertStyle.assignNull && (!type.IsValueType || Nullable.GetUnderlyingType(type) != null))
                {
                    target = default(T);
                }
            }
            else
            {
                info.SetData(ref target, gameData, assetProvider, style);
            }
        }

        public static void SetListValue<T>(ref List<T> target, IEnumerable<Nullable<T>> value, InsertStyle style)
            where T : struct
        {
            if (target == null || style == InsertStyle.assignNull)
            {
                target = value?.Select(x => x.HasValue ? x.Value : default(T)).ToList();
            }
            else if (value != null)
            {
                switch (style)
                {
                    case InsertStyle.append:
                        target = target.Concat(value?.Select(x => x.HasValue ? x.Value : default(T))).ToList();
                        break;
                    case InsertStyle.prepend:
                        target = value?.Select(x => x.HasValue ? x.Value : default(T)).Concat(target).ToList();
                        break;
                    case InsertStyle.replace:
                        var valueIt = value.GetEnumerator();
                        var targetIt = target.GetEnumerator();
                        var result = new List<T>();

                        while (targetIt.MoveNext())
                        {
                            if (valueIt.MoveNext() && valueIt.Current.HasValue)
                            {
                                result.Add(valueIt.Current.Value);
                            }
                            else
                            {
                                result.Add(targetIt.Current);
                            }
                        }

                        while (valueIt.MoveNext())
                        {
                            result.Add(valueIt.Current.HasValue ? valueIt.Current.Value : default(T));
                        }

                        target = result;
                        break;
                }
            }
        }

        public static void SetListValue<T>(ref List<T> target, IEnumerable<T> value, InsertStyle style)
        {
            if (target == null || style == InsertStyle.assignNull)
            {
                target = value?.ToList();
            }
            else if (value != null)
            {
                switch (style)
                {
                    case InsertStyle.append:
                        target = target.Concat(value).ToList();
                        break;
                    case InsertStyle.prepend:
                        target = value.Concat(target).ToList();
                        break;
                    case InsertStyle.replace:
                        var valueIt = value.GetEnumerator();
                        var targetIt = target.GetEnumerator();
                        var result = new List<T>();

                        while (targetIt.MoveNext())
                        {
                            if (valueIt.MoveNext() && valueIt.Current != null)
                            {
                                result.Add(valueIt.Current);
                            }
                            else
                            {
                                result.Add(targetIt.Current);
                            }
                        }

                        while (valueIt.MoveNext())
                        {
                            result.Add(valueIt.Current);
                        }

                        target = result;
                        break;
                }
            }
        }

        public static void SetListValue<T>(ref List<T> target, IEnumerable<IGameDataInfo<T>> value, InsertStyle style, GameDataProvider gameData, AssetProvider assetProvider)
        {
            var converted = value?.Select(x =>
            {
                var newEntry = default(T);
                x.SetData(ref newEntry, gameData, assetProvider, style);
                return newEntry;
            });

            SetListValue(ref target, converted, style);
        }
    }
}
