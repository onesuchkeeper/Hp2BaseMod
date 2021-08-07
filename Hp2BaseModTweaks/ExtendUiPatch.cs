// Hp2Sample 2021, By OneSuchKeeper

using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    public static class ExtendUiPatch
    {
		public static void Patch(Harmony harmony)
		{
			try
			{
				//var t = MethodType.Setter;

				//var test = AccessTools.FieldRefAccess<,>();

				Harmony.DEBUG = true;
				var mOrigional = AccessTools.Method(typeof(UiCellphone), "LoadApp");
				var mPostfix = SymbolExtensions.GetMethodInfo(() => ExtendUi(null, 0));

				harmony.Patch(mOrigional, null, new HarmonyMethod(mPostfix));

				

				Harmony.DEBUG = false;
			}
			catch (Exception e)
			{
				Harmony.DEBUG = true;
				FileLog.Log("EXCEPTION Hp2BaseModTweaks.ExttendUiPatch: " + e.Message);
			}
		}

		private static void ExtendUi(UiCellphone __instance, int appIndex)
		{
			Harmony.DEBUG = true;

			//var currentApp = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(__instance) as UiCellphoneApp;

			//FileLog.Log($"App {appIndex}: {currentApp.name ?? "NULL"}");

			Harmony.DEBUG = false;
		}
	}
}
