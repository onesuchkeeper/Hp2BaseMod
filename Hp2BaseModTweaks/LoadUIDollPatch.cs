// Hp2Sample 2021, By OneSuchKeeper

using HarmonyLib;
using System;
using System.Collections.Generic;

namespace Hp2BaseModTweaks
{
    class LoadUIDollPatch
    {
		public static bool LoadGirl(UiDoll __instance, GirlDefinition girlDef, int expressionIndex = -1, int hairstyleIndex = -1, int outfitIndex = -1, GirlDefinition soulGirlDef = null)
		{
			//var girlDefinitionAccess = AccessTools.Field(typeof(UiDoll), "_girlDefinition");

			//if ((girlDefinitionAccess.GetValue(__instance) as GirlDefinition) != null)
			//{
			//	__instance.UnloadGirl();
			//}
			//girlDefinitionAccess.SetValue(__instance, girlDef);
			////girlDefinition = girlDef;
			//AccessTools.Field(typeof(UiDoll), "_soulGirlDefinition").SetValue(__instance, soulGirlDef);
			////__instance._soulGirlDefinition = soulGirlDef;
			//AccessTools.Field(typeof(UiDoll), "_currentExpression").SetValue(__instance, girlDef.expressions[0]);
			////__instance._currentExpression = girlDef.expressions[0];

			//var phonemeLetters = AccessTools.Field(typeof(UiDoll), "PHONEME_LETTERS").GetValue(__instance) as string[];
			//var letterMouths = AccessTools.Field(typeof(UiDoll), "_letterMouths").GetValue(__instance) as Dictionary<string, int>;

			//for (int i = 0; i < phonemeLetters.Length; i++)
			//{
			//	string[] array = phonemeLetters[i].Split(new char[]
			//	{
			//	','
			//	});
			//	for (int j = 0; j < array.Length; j++)
			//	{
			//		letterMouths.Add(array[j], girlDef.partIndexesPhonemes[i]);
			//	}
			//}
			//__instance.SetBlinking(false);
			//__instance.LoadPart(__instance.partBody, girlDef.partIndexBody, -1f);
			//__instance.LoadPart(__instance.partNipples, girlDef.partIndexNipples, -1f);
			//__instance.LoadPart(__instance.partBlushLight, girlDef.partIndexBlushLight, -1f);
			//__instance.LoadPart(__instance.partBlushHeavy, girlDef.partIndexBlushHeavy, -1f);
			//__instance.LoadPart(__instance.partBlink, girlDef.partIndexBlink, -1f);
			//__instance.partBlushLight.Hide();
			//__instance.partBlushHeavy.Hide();
			//__instance.ChangeExpression(expressionIndex, false);
			//__instance.ChangeHairstyle(hairstyleIndex);
			//__instance.ChangeOutfit(outfitIndex);
			//__instance.breathEmitterRectTransform.anchoredPosition = girlDef.breathEmitterPos;
			//__instance.upsetEmitterRectTransform.anchoredPosition = girlDef.upsetEmitterPos;
			//Game.Manager.Time.Play(__instance._breathSequence, __instance.pauseDefinition, Random.Range(0f, __instance._breathSequence.Duration(false)));
			//if (__instance.soulGirlDefinition.specialCharacter && __instance.soulGirlDefinition.specialEffectPrefab != null)
			//{
			//	__instance._specialEffect = Object.Instantiate<UiDollSpecialEffect>(__instance.soulGirlDefinition.specialEffectPrefab);
			//	__instance._specialEffect.rectTransform.SetParent(Game.Session.gameCanvas.dollSpecialEffectContainer, false);
			//	__instance._specialEffect.Init(__instance);
			//}

			return false;
		}
	}
}
