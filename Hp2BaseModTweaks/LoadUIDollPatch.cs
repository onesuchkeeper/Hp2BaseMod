// Hp2Sample 2021, By OneSuchKeeper

using HarmonyLib;
using System;

namespace Hp2BaseModTweaks
{
	/// <summary>
	/// It didn't like me adding a prefix to load girl for whatever reason, but change outfit works just as well I guess...
	/// </summary>
    public static class LoadUIDollPatch
    {
		public static void Patch(Harmony harmony)
		{
			try
			{
				var mOrigional = AccessTools.Method(typeof(UiDoll), "ChangeOutfit");
				var mChangeOutfitPost = SymbolExtensions.GetMethodInfo(() => ChangeOutfitPost(null));

                harmony.Patch(mOrigional, null, new HarmonyMethod(mChangeOutfitPost));
			}
			catch (Exception e)
			{
				Harmony.DEBUG = true;
				FileLog.Log("EXCEPTION Hp2BaseModTweaks.LoadUIDollPatch: " + e.Message);
			}
		}

		private static void ChangeOutfitPost(UiDoll __instance)
		{
			if (__instance.soulGirlDefinition?.specialCharacter ?? true) { return; }

			var _specialEffectAccess = AccessTools.Field(typeof(UiDoll), "_specialEffect");

            if (_specialEffectAccess.GetValue(__instance) as UiDollSpecialEffect == null && __instance.soulGirlDefinition?.specialEffectPrefab != null)
            {
				var specialEffectInstance = UnityEngine.Object.Instantiate(__instance.soulGirlDefinition.specialEffectPrefab);

				_specialEffectAccess.SetValue(__instance, specialEffectInstance);
				specialEffectInstance.rectTransform.SetParent(Game.Session.gameCanvas.dollSpecialEffectContainer, false);
				specialEffectInstance.Init(__instance);
            }
        }
	}
}
