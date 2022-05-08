﻿// Hp2Sample 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;

namespace Hp2BaseModTweaks
{
    /// <summary>
    /// Loads button prefabs into asset manager
    /// </summary>
    [HarmonyPatch(typeof(GameData), MethodType.Constructor)]
    public static class AwakenAssetHolderPatch
    {
        public static void Postfix(GameData __instance)
        {
            ModInterface.Instance.LogTitle("Awaking Asset Holder");
            ModInterface.Instance.IncreaseLogIndent();
            new AssetHolder().Awake();
            ModInterface.Instance.LogLine("Done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}