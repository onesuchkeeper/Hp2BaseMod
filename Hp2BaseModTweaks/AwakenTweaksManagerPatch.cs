// Hp2Sample 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;

namespace Hp2BaseModTweaks
{
    /// <summary>
    /// Loads button prefabs into asset manager
    /// </summary>
    [HarmonyPatch(typeof(GameData), MethodType.Constructor)]
    public static class AwakenTweaksManagerPatch
    {
        public static void Prefix(GameData __instance)
        {
            ModInterface.Instance.LogLine("Awaking Hp2BaseModTweaks Asset holder");
            ModInterface.Instance.IncreaseLogIndent();
            new AssetHolder().Awake();
            ModInterface.Instance.LogLine("Done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
