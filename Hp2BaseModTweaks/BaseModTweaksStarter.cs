// Hp2BaseModTweaks 2021, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;

namespace Hp2BaseModTweaks
{
    public class BaseModTweaksStarter : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataModder)
        {
            new Harmony("Hp2BaseMod.Hp2BaseModTweaks").PatchAll();
        }
    }
}
