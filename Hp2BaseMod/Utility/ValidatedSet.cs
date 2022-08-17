// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Save;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Utility
{
    /// <summary>
    /// Dictates the way a data mod is loaded into an exsisting data instance.
    /// replace: assigns the value to that of the mod if the mod's value isn't null. For collections assigns the elements, not the collection itself.
    /// append: appends the mod's value to the collection. If the collection is null assigns it instead.
    /// prepend: prepends the mod's value to the collection. If the collection is null assigns it instead.
    /// assignNull: if the value is nullable, assigns the value to that of the mod even if the mod's value is null. Will not assign null to value types.
    /// </summary>
    public enum InsertStyle
    {
        assignNull,
        append,
        prepend,
        replace
    }

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

        public static void SetValue<T>(ref T target, IGameDefinitionInfo<T> info, InsertStyle style, GameDefinitionProvider gameData, AssetProvider assetProvider)
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

        public static void SetListValue<T>(ref List<T> target, IEnumerable<IGameDefinitionInfo<T>> value, InsertStyle style, GameDefinitionProvider gameData, AssetProvider assetProvider)
        {
            var converted = value?.Select(x =>
            {
                var newEntry = default(T);
                x.SetData(ref newEntry, gameData, assetProvider, style);
                return newEntry;
            });

            SetListValue(ref target, converted, style);
        }

        public static void SetModIds(List<int> saveFileIds, List<SavedSourceId> savedSourceIds, Func<RelativeId, int> getRuntimeId)
        {
            if (savedSourceIds != null)
            {
                foreach (var savedSourceId in savedSourceIds)
                {
                    var id = savedSourceId.ToRelativeId();

                    if (id.HasValue)
                    {
                        saveFileIds.Add(getRuntimeId.Invoke(id.Value));
                    }
                }
            }
        }

        public static void StripRuntimeIds(ref List<int> saveFileIds, List<SavedSourceId> moddedIds, string errorDescriptor, Func<int, RelativeId> getId)
        {
            if (saveFileIds == null)
            {
                saveFileIds = new List<int>();
            }

            var defaultIds = new List<int>();
            foreach (var runtimeId in saveFileIds)
            {
                var id = getId.Invoke(runtimeId);

                if (id.SourceId == -1)
                {
                    defaultIds.Add(id.LocalId);
                }
                else
                {
                    var mod = ModInterface.FindMod(id.SourceId);

                    if (mod == null)
                    {
                        ModInterface.Log.LogMissingIdError(errorDescriptor, id);
                    }
                    else
                    {
                        moddedIds.Add(new SavedSourceId(mod.SourceId, id.LocalId));
                    }
                }
            }
            saveFileIds = defaultIds;
        }
    }
}
