// Hp2Sample 2022, By OneSuchKeeper

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
        public static void Postfix()
        {
            ModInterface.Log.LogTitle("Awaking Hp2BaseModTweaks Asset Holder");
            using (ModInterface.Log.MakeIndent())
            {
                new AssetHolder().Awake();
            }
        }
    }
}
