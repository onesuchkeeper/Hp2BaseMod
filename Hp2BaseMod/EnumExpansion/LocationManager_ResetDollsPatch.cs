using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Hp2BaseMod.EnumExpansion
{
    [HarmonyPatch(typeof(LocationManager))]
    public static class LocationManager_ResetDollsPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnDepartureComplete")]
        public static bool OnDepartureComplete(LocationManager __instance)
        {
			__instance.ResetDolls(true);
			Arrive(__instance, Game.Persistence.playerFile.locationDefinition, Game.Persistence.playerFile.girlPairDefinition, Game.Persistence.playerFile.sidesFlipped, false);
			return false;
		}

        private static void Arrive(LocationManager locationManager, LocationDefinition locationDef, GirlPairDefinition girlPairDef, bool sidesFlipped, bool initialArrive = false)
        {
			Game.Persistence.playerFile.locationDefinition = locationDef;
			Game.Persistence.playerFile.girlPairDefinition = girlPairDef;
			Game.Persistence.playerFile.sidesFlipped = sidesFlipped;

			var currentLocation = Game.Persistence.playerFile.locationDefinition;
			AccessTools.Field(typeof(LocationManager), "_currentLocation").SetValue(locationManager, currentLocation);

			var currentGirlPair = Game.Persistence.playerFile.girlPairDefinition;
			AccessTools.Field(typeof(LocationManager), "_currentGirlPair").SetValue(locationManager, currentGirlPair);

			var currentSidesFlipped = Game.Persistence.playerFile.sidesFlipped;
			AccessTools.Field(typeof(LocationManager), "_currentSidesFlipped").SetValue(locationManager, currentSidesFlipped);

			var currentTransitionType = (LocationTransitionType)AccessTools.Field(typeof(LocationManager), "_currentTransitionType").GetValue(locationManager);
			var transitions = AccessTools.Field(typeof(LocationManager), "_transitions").GetValue(locationManager) as Dictionary<LocationTransitionType, LocationTransition>;
			transitions[currentTransitionType].Prep();

			bool gameSaved = false;
			if (!initialArrive || Game.Persistence.debugMode)
			{
				Game.Persistence.playerFile.ClearPerishableInventoryItems();
				int daytimeElapsed = Game.Persistence.playerFile.daytimeElapsed;
				int num = Mathf.FloorToInt((float)daytimeElapsed / 4f);
				if (num > 0)
				{
					if (daytimeElapsed != Game.Persistence.playerFile.finderRestockTime)
					{
						Game.Persistence.playerFile.finderRestockTime = daytimeElapsed;
						Game.Persistence.playerFile.PopulateFinderSlots();
					}
					if (num != Game.Persistence.playerFile.storeRestockDay)
					{
						Game.Persistence.playerFile.storeRestockDay = num;
						Game.Persistence.playerFile.PopulateStoreProducts();
					}
				}
				if (!initialArrive && currentLocation.locationType != LocationType.DATE)
				{
					Game.Persistence.Apply(true);
					Game.Persistence.SaveGame();
					gameSaved = true;
				}
			}
			switch (currentLocation.locationType)
			{
				case LocationType.SIM:
					if (currentGirlPair != null)
					{
						Game.Session.Puzzle.puzzleStatus.Reset(locationManager.currentGirlLeft, locationManager.currentGirlRight);
						Game.Session.Puzzle.puzzleStatus.girlStatusLeft.playerFileGirl.staminaFreeze = -1;
						Game.Session.Puzzle.puzzleStatus.girlStatusRight.playerFileGirl.staminaFreeze = -1;
					}
					else
					{
						Game.Session.Puzzle.puzzleStatus.Clear();
					}
					break;
				case LocationType.DATE:
					if ((initialArrive || Game.Session.Puzzle.puzzleStatus.isEmpty) && currentGirlPair != null)
					{
						Game.Session.Puzzle.puzzleStatus.Reset(locationManager.currentGirlLeft, locationManager.currentGirlRight);
					}
					if (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition || currentGirlPair == null)
					{
						List<GirlDefinition> allBySpecial = Game.Data.Girls.GetAllBySpecial(false);
						ListUtils.ShuffleList<GirlDefinition>(allBySpecial);
						if (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition)
						{
							while (allBySpecial.Count > 8)
							{
								allBySpecial.RemoveAt(allBySpecial.Count - 1);
							}
							allBySpecial.Add(currentGirlPair.girlDefinitionOne);
							allBySpecial.Add(currentGirlPair.girlDefinitionTwo);
							Game.Session.Puzzle.puzzleStatus.Reset(allBySpecial, false);
						}
						else
						{
							Game.Session.Puzzle.puzzleStatus.Reset(allBySpecial, true);
						}
					}
					Game.Session.Puzzle.puzzleStatus.PopulateAilments();
					break;
				case LocationType.SPECIAL:
					Game.Session.Puzzle.puzzleStatus.Clear();
					break;
				case LocationType.HUB:
					Game.Session.Puzzle.puzzleStatus.Clear();
					break;
			}
			//locationManager.ResetDolls(currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition);
			ResetDolls(locationManager, currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition);
			Game.Session.Hub.PrepHub();
			Game.Session.gameCanvas.header.rectTransform.anchoredPosition = new Vector2((!Game.Session.Location.AtLocationType(new LocationType[]
			{
			LocationType.HUB
			})) ? Game.Session.gameCanvas.header.xValues.x : Game.Session.gameCanvas.header.xValues.y, Game.Session.gameCanvas.header.rectTransform.anchoredPosition.y);
			Game.Session.gameCanvas.cellphone.rectTransform.anchoredPosition = new Vector2((!Game.Session.Location.AtLocationType(new LocationType[]
			{
			LocationType.HUB
			})) ? Game.Session.gameCanvas.cellphone.xValues.x : Game.Session.gameCanvas.cellphone.xValues.y, Game.Session.gameCanvas.cellphone.rectTransform.anchoredPosition.y);
			Game.Session.gameCanvas.header.Refresh(true);
			Game.Session.gameCanvas.cellphone.Refresh(true);
			//this event has no subscribers...
			//if (locationManager.ArriveEvent != null)
			//{
			//	locationManager.ArriveEvent();
			//}
			var accessArrivalCutscene = AccessTools.Field(typeof(LocationManager), "_arrivalCutscene");

			accessArrivalCutscene.SetValue(locationManager, null);
			if (currentLocation.locationType == LocationType.SIM)
			{
				if (currentGirlPair != null && !currentGirlPair.specialPair)
				{
					PlayerFileGirl playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(currentGirlPair.girlDefinitionOne);
					if (!playerFileGirl.playerMet)
					{
						playerFileGirl.playerMet = true;
					}
					PlayerFileGirl playerFileGirl2 = Game.Persistence.playerFile.GetPlayerFileGirl(currentGirlPair.girlDefinitionTwo);
					if (!playerFileGirl2.playerMet)
					{
						playerFileGirl2.playerMet = true;
					}
					PlayerFileGirlPair playerFileGirlPair = Game.Persistence.playerFile.GetPlayerFileGirlPair(currentGirlPair);
					if (playerFileGirlPair.relationshipType == GirlPairRelationshipType.UNKNOWN)
					{
						playerFileGirlPair.RelationshipLevelUp();
						if (currentGirlPair.introductionPair)
						{
							accessArrivalCutscene.SetValue(locationManager, locationManager.cutsceneMeetingIntro);
						}
						else
						{
							accessArrivalCutscene.SetValue(locationManager, locationManager.cutsceneMeeting);
						}
					}
				}
			}
			else
			{
				Game.Session.Logic.ProcessBundleList(currentLocation.arriveBundleList, false);
			}
			transitions[currentTransitionType].Arrive(initialArrive, (currentGirlPair != null || !Game.Session.Puzzle.puzzleStatus.isEmpty) && accessArrivalCutscene.GetValue(locationManager) == null, gameSaved);
		}

		private static void ResetDolls(LocationManager locationManager, bool unload)
        {
			if (unload)
			{
				Game.Session.gameCanvas.dollLeft.UnloadGirl();
				Game.Session.gameCanvas.dollRight.UnloadGirl();
				Game.Session.gameCanvas.dollMiddle.UnloadGirl();
				return;
			}
			int hairstyleIndexLeft = -1;
			int outfitIndexLeft = -1;
			int hairstyleIndexRight = -1;
			int outfitIndexRight = -1;

			var currentGirlPair = AccessTools.Field(typeof(LocationManager), "_currentGirlPair").GetValue(locationManager) as GirlPairDefinition;
			var currentLocation = AccessTools.Field(typeof(LocationManager), "_currentLocation").GetValue(locationManager) as LocationDefinition;
			var currentSidesFlipped = (bool)AccessTools.Field(typeof(LocationManager), "_currentSidesFlipped").GetValue(locationManager);

			var pairStyleInfo = EnumLookups.GetPairStyleInfo(currentGirlPair.id);

			PlayerFileGirlPair playerFileGirlPair = Game.Persistence.playerFile.GetPlayerFileGirlPair(currentGirlPair);
			switch (currentLocation.locationType)
			{
				case LocationType.SIM:
					if (playerFileGirlPair != null
						&& playerFileGirlPair.relationshipType == GirlPairRelationshipType.UNKNOWN)
					{
						if (!currentSidesFlipped)
						{
							if (currentGirlPair.hasMeetingStyleOne)
							{
								var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.MeetingGirlOne.HairstyleName);
                                hairstyleIndexLeft = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);
								var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.MeetingGirlOne.OutfitName);
								outfitIndexLeft = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);
                            }
							if (currentGirlPair.hasMeetingStyleTwo)
							{
								var hairstyle = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.MeetingGirlTwo.HairstyleName);
								hairstyleIndexRight = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle);
								var outfit = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.MeetingGirlTwo.OutfitName);
								outfitIndexRight = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit);
							}
						}
						else
						{
							if (currentGirlPair.hasMeetingStyleTwo)
							{
								var hairstyle = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.MeetingGirlTwo.HairstyleName);
								hairstyleIndexLeft = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle);
								var outfit = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.MeetingGirlTwo.OutfitName);
								outfitIndexLeft = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit);
							}
							if (currentGirlPair.hasMeetingStyleOne)
							{
								var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.MeetingGirlOne.HairstyleName);
								hairstyleIndexRight = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);
								var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.MeetingGirlOne.OutfitName);
								outfitIndexRight = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);
							}
						}
					}
					break;
				case LocationType.DATE:
					if (playerFileGirlPair != null
						&& playerFileGirlPair.relationshipType == GirlPairRelationshipType.ATTRACTED
						&& Game.Persistence.playerFile.daytimeElapsed % 4 == (int)playerFileGirlPair.girlPairDefinition.sexDaytime)
					{
						if (!currentSidesFlipped)
						{
							var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.SexGirlOne.HairstyleName);
							hairstyleIndexLeft = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);

							var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.SexGirlOne.OutfitName);
							outfitIndexLeft = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);

							var hairstyle2 = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.SexGirlTwo.HairstyleName);
							hairstyleIndexRight = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle2);

							var outfit2 = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.SexGirlTwo.OutfitName);
							outfitIndexRight = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit2);
						}
						else
						{
							var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.SexGirlOne.HairstyleName);
							hairstyleIndexRight = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);

							var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.SexGirlOne.OutfitName);
							outfitIndexRight = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);

							var hairstyle2 = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == pairStyleInfo.SexGirlTwo.HairstyleName);
							hairstyleIndexLeft = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle2);

							var outfit2 = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == pairStyleInfo.SexGirlTwo.OutfitName);
							outfitIndexLeft = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit2);
						}
					}
					else if (!Game.Session.Puzzle.puzzleStatus.isEmpty && currentLocation != Game.Session.Puzzle.bossLocationDefinition)
					{
						if (!Game.Session.Puzzle.puzzleStatus.girlStatusLeft.playerFileGirl.stylesOnDates)
						{
							// girl one on the left
							if (!currentSidesFlipped)
                            {
								var styleInfo = EnumLookups.GetLocationStyleInfo(currentLocation.id, currentGirlPair.girlDefinitionOne.id);

								var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == styleInfo.HairstyleName);
								hairstyleIndexLeft = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);

								var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == styleInfo.OutfitName);
								outfitIndexLeft = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);
							}
							// girl one on the right
							else
                            {
								var styleInfo = EnumLookups.GetLocationStyleInfo(currentLocation.id, currentGirlPair.girlDefinitionOne.id);

								var hairstyle = currentGirlPair.girlDefinitionOne.hairstyles.FirstOrDefault(x => x.hairstyleName == styleInfo.HairstyleName);
								hairstyleIndexRight = currentGirlPair.girlDefinitionOne.hairstyles.IndexOf(hairstyle);

								var outfit = currentGirlPair.girlDefinitionOne.outfits.FirstOrDefault(x => x.outfitName == styleInfo.OutfitName);
								outfitIndexRight = currentGirlPair.girlDefinitionOne.outfits.IndexOf(outfit);
							}
						}
						if (!Game.Session.Puzzle.puzzleStatus.girlStatusRight.playerFileGirl.stylesOnDates)
						{
							// girl two on the right
							if (!currentSidesFlipped)
							{
								var styleInfo = EnumLookups.GetLocationStyleInfo(currentLocation.id, currentGirlPair.girlDefinitionTwo.id);

								var hairstyle = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == styleInfo.HairstyleName);
								hairstyleIndexRight = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle);

								var outfit = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == styleInfo.OutfitName);
								outfitIndexRight = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit);
							}
							// girl two on the left
							else
							{
								var styleInfo = EnumLookups.GetLocationStyleInfo(currentLocation.id, currentGirlPair.girlDefinitionTwo.id);

								var hairstyle = currentGirlPair.girlDefinitionTwo.hairstyles.FirstOrDefault(x => x.hairstyleName == styleInfo.HairstyleName);
								hairstyleIndexLeft = currentGirlPair.girlDefinitionTwo.hairstyles.IndexOf(hairstyle);

								var outfit = currentGirlPair.girlDefinitionTwo.outfits.FirstOrDefault(x => x.outfitName == styleInfo.OutfitName);
								outfitIndexLeft = currentGirlPair.girlDefinitionTwo.outfits.IndexOf(outfit);
							}
						}
					}
					break;
				case LocationType.HUB:
					if (Game.Persistence.playerFile.storyProgress >= 7 && Game.Persistence.playerFile.GetFlagValue(Game.Session.Hub.firstLocationFlag) >= 0 && UnityEngine.Random.Range(0f, 1f) <= 0.1f)
					{
						int num = UnityEngine.Random.Range(0, Game.Session.Hub.hubGirlAltStyleIndexes.Length);
						hairstyleIndexRight = Game.Session.Hub.hubGirlAltStyleIndexes[num];
						outfitIndexRight = Game.Session.Hub.hubGirlAltStyleIndexes[num];
					}
					break;
			}
			if (!Game.Session.Puzzle.puzzleStatus.isEmpty)
			{
				GirlDefinition soulGirlDef = (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition) ? Game.Session.Puzzle.bossGirlPairDefinition.girlDefinitionOne : null;
				GirlDefinition soulGirlDef2 = (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition) ? Game.Session.Puzzle.bossGirlPairDefinition.girlDefinitionTwo : null;
				Game.Session.gameCanvas.dollLeft.LoadGirl(Game.Session.Puzzle.puzzleStatus.girlStatusLeft.girlDefinition, -1, hairstyleIndexLeft, outfitIndexLeft, soulGirlDef);
				Game.Session.gameCanvas.dollRight.LoadGirl(Game.Session.Puzzle.puzzleStatus.girlStatusRight.girlDefinition, -1, hairstyleIndexRight, outfitIndexRight, soulGirlDef2);
			}
			else if (currentGirlPair != null)
			{
				Game.Session.gameCanvas.dollLeft.LoadGirl(locationManager.currentGirlLeft, -1, hairstyleIndexLeft, outfitIndexLeft, null);
				Game.Session.gameCanvas.dollRight.LoadGirl(locationManager.currentGirlRight, -1, hairstyleIndexRight, outfitIndexRight, null);
			}
			else if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.HUB }))
			{
				Game.Session.gameCanvas.dollLeft.UnloadGirl();
				Game.Session.gameCanvas.dollRight.LoadGirl(Game.Session.Hub.hubGirlDefinition, -1, hairstyleIndexRight, outfitIndexRight, null);
			}
			else
			{
				Game.Session.gameCanvas.dollLeft.UnloadGirl();
				Game.Session.gameCanvas.dollRight.UnloadGirl();
			}
			Game.Session.gameCanvas.dollMiddle.UnloadGirl();
		}
	}
}
