using HarmonyLib;
using Hp2BaseMod.GameDataInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseMod.EnumExpansion
{
	/// <summary>
	/// Locations have their styles selected by their associated enum value, which doesn't work if you want
	/// to use an unpaired hairstyle/outfit combo or a custom hairstlye / outfit without a style enum value.
	/// This replaces the arrive method call from the OnDepartureComplete handler to use my lookup for outfits and hairstyles
	/// instead which records the outside info.
	/// </summary>
    [HarmonyPatch(typeof(LocationManager))]
    public static class LocationManager_ResetDollsPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnDepartureComplete")]
        public static bool OnDepartureComplete(LocationManager __instance)
        {
            try
            {
				// this just unloads all the dolls, but keeping this call incase the code changes at some point
				__instance.ResetDolls(true);
				Arrive(__instance);
			}
			catch (Exception e)
            {
				ModInterface.Log.LogLine(e.ToString());
            }

			return false;
		}

        private static void Arrive(LocationManager locationManager)
        {
			// get current location's info
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

			// time passing
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

			// save
			if (currentLocation.locationType != LocationType.DATE)
			{
				Game.Persistence.Apply(true);
				Game.Persistence.SaveGame();
				gameSaved = true;
			}

			// set up location
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
					if (Game.Session.Puzzle.puzzleStatus.isEmpty && currentGirlPair != null)
					{
						Game.Session.Puzzle.puzzleStatus.Reset(locationManager.currentGirlLeft, locationManager.currentGirlRight);
					}
					// null (non-stop date?) or boss pair
					if (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition || currentGirlPair == null)
					{
						List<GirlDefinition> allBySpecial = Game.Data.Girls.GetAllBySpecial(false);
						ListUtils.ShuffleList(allBySpecial);

						if (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition)
						{
							// there needs to be n pairs of girls, then the nymphojinn. If there are no girls,
							//just the nymphojinn are added and it'll go right to the bonus round. 
							//having an odd ammount of girls will screw with the pairs so an even count must be enforced.
							// 4 pairs is normal, but having more or less for whatever reason wont break it
							allBySpecial = allBySpecial.Take(8).ToList();

							if (allBySpecial.Count % 2 == 1)
                            {
								allBySpecial.RemoveAt(0);
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

			if (currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition)
			{
				Game.Session.gameCanvas.dollLeft.UnloadGirl();
				Game.Session.gameCanvas.dollRight.UnloadGirl();
				Game.Session.gameCanvas.dollMiddle.UnloadGirl();
			}
			else
			{
				ResetDolls(locationManager);
			}

			Game.Session.Hub.PrepHub();
			Game.Session.gameCanvas.header.rectTransform.anchoredPosition = new Vector2((!Game.Session.Location.AtLocationType(new LocationType[] { LocationType.HUB }))
				? Game.Session.gameCanvas.header.xValues.x
				: Game.Session.gameCanvas.header.xValues.y, Game.Session.gameCanvas.header.rectTransform.anchoredPosition.y);
			Game.Session.gameCanvas.cellphone.rectTransform.anchoredPosition = new Vector2((!Game.Session.Location.AtLocationType(new LocationType[] { LocationType.HUB }))
				? Game.Session.gameCanvas.cellphone.xValues.x
				: Game.Session.gameCanvas.cellphone.xValues.y, Game.Session.gameCanvas.cellphone.rectTransform.anchoredPosition.y);
			Game.Session.gameCanvas.header.Refresh(true);
			Game.Session.gameCanvas.cellphone.Refresh(true);

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
			transitions[currentTransitionType].Arrive(false, (currentGirlPair != null || !Game.Session.Puzzle.puzzleStatus.isEmpty) && accessArrivalCutscene.GetValue(locationManager) == null, gameSaved);
		}

		private static void RandomizeStyle(GirlDefinition girl, bool unpaired, out GirlStyleInfo style, bool anyOutfit = false)
        {
			var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, girl.id);
			var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girl);

			if (playerFileGirl == null || anyOutfit)
			{
				var outfitIndex = UnityEngine.Random.Range(0, girl.outfits.Count);
				var outfitId = ModInterface.Data.GetOutfitId(girlId, outfitIndex);
				var hairCount = girl.hairstyles.Count;

				style = new GirlStyleInfo()
				{
					OutfitId = outfitId,
					HairstyleId = outfitIndex > hairCount - 1
								  || unpaired
						? ModInterface.Data.GetHairstyleId(girlId, UnityEngine.Random.Range(0, hairCount))
						: outfitId
				};
			}
			else
			{
				var outfitIndex = playerFileGirl.unlockedOutfits.Any()
					? playerFileGirl.unlockedOutfits[UnityEngine.Random.Range(0, playerFileGirl.unlockedOutfits.Count)]
					: -1;
				var outfitId = outfitIndex == -1 ? RelativeId.Default : ModInterface.Data.GetOutfitId(girlId, outfitIndex);

				style = new GirlStyleInfo()
				{
					OutfitId = outfitId,
					HairstyleId = outfitIndex > girl.hairstyles.Count - 1
								  || unpaired
						? playerFileGirl.unlockedHairstyles.Any() ? ModInterface.Data.GetHairstyleId(girlId, playerFileGirl.unlockedHairstyles[UnityEngine.Random.Range(0, playerFileGirl.unlockedHairstyles.Count)]) : RelativeId.Default
						: outfitId
				};
			}
		}

		private static void ResetDolls(LocationManager locationManager)
        {
			var currentLocation = AccessTools.Field(typeof(LocationManager), "_currentLocation").GetValue(locationManager) as LocationDefinition;
			var unpairedStyles = Codes.UnpairStylesCodeId.HasValue
				? ModInterface.IsCodeUnlocked(Codes.UnpairStylesCodeId.Value)
				: false;

			// hub
			if (currentLocation.locationType == LocationType.HUB)
            {
				GirlStyleInfo hubGirlStyle = new GirlStyleInfo() { OutfitId = RelativeId.Default, HairstyleId = RelativeId.Default };

				//randomize
				if (Game.Persistence.playerFile.storyProgress >= 7
						&& Game.Persistence.playerFile.GetFlagValue(Game.Session.Hub.firstLocationFlag) >= 0)
				{
					var hubChangeRateUp = Codes.HubStyleChangeRateUpCodeId.HasValue
						? ModInterface.IsCodeUnlocked(Codes.HubStyleChangeRateUpCodeId.Value)
						: false;

					if (hubChangeRateUp || UnityEngine.Random.Range(0f, 1f) <= 0.1f)
					{
						RandomizeStyle(Game.Session.Hub.hubGirlDefinition,
									   unpairedStyles,
									   out hubGirlStyle,
									   true);
					}
				}

				//apply
				var hubGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, Game.Session.Hub.hubGirlDefinition.id);

				Game.Session.gameCanvas.dollLeft.UnloadGirl();
				Game.Session.gameCanvas.dollRight.LoadGirl(Game.Session.Hub.hubGirlDefinition,
					                                       -1,
														   ModInterface.Data.GetHairstyleIndex(hubGirlId, hubGirlStyle.HairstyleId) ?? -1,
														   ModInterface.Data.GetOutfitIndex(hubGirlId, hubGirlStyle.OutfitId) ?? -1,
														   null);
			}
			// pair based locations
            else
            {
				var currentGirlPair = AccessTools.Field(typeof(LocationManager), "_currentGirlPair").GetValue(locationManager) as GirlPairDefinition;
				
				if (currentGirlPair == null)
                {
					Game.Session.gameCanvas.dollLeft.UnloadGirl();
					Game.Session.gameCanvas.dollRight.UnloadGirl();
				}
				else
                {
					var playerFileGirlPair = Game.Persistence.playerFile.GetPlayerFileGirlPair(currentGirlPair);

					// soul defs are needed for the boss rounds
					GirlDefinition soulGirlDefOne = null;
					GirlDefinition soulGirlDefTwo = null;

					if (!Game.Session.Puzzle.puzzleStatus.isEmpty
						&& currentGirlPair == Game.Session.Puzzle.bossGirlPairDefinition)
					{
						soulGirlDefOne = Game.Session.Puzzle.bossGirlPairDefinition.girlDefinitionOne;
						soulGirlDefTwo = Game.Session.Puzzle.bossGirlPairDefinition.girlDefinitionTwo;
					}

					GirlStyleInfo leftStyle = GirlStyleInfo.Default();
					GirlStyleInfo rightStyle = GirlStyleInfo.Default();
					RelativeId leftGirlId, rightGirlId;

					if ((bool)AccessTools.Field(typeof(LocationManager), "_currentSidesFlipped").GetValue(locationManager))
					{
						DetermineStyles(currentGirlPair.girlDefinitionTwo, currentGirlPair.girlDefinitionOne, currentLocation, playerFileGirlPair, unpairedStyles, out leftStyle, out rightStyle);

						leftGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, currentGirlPair.girlDefinitionTwo.id);
						rightGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, currentGirlPair.girlDefinitionOne.id);
					}
					else
					{
						DetermineStyles(currentGirlPair.girlDefinitionOne, currentGirlPair.girlDefinitionTwo, currentLocation, playerFileGirlPair, unpairedStyles, out leftStyle, out rightStyle);

						leftGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, currentGirlPair.girlDefinitionOne.id);
						rightGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, currentGirlPair.girlDefinitionTwo.id);
					}

					Game.Session.gameCanvas.dollLeft.LoadGirl(locationManager.currentGirlLeft,
															  -1,
															  ModInterface.Data.GetHairstyleIndex(leftGirlId, leftStyle.HairstyleId) ?? -1,
															  ModInterface.Data.GetOutfitIndex(leftGirlId, leftStyle.OutfitId) ?? -1,
															  soulGirlDefOne);

					Game.Session.gameCanvas.dollRight.LoadGirl(locationManager.currentGirlRight,
															   -1,
															   ModInterface.Data.GetHairstyleIndex(rightGirlId, rightStyle.HairstyleId) ?? -1,
															   ModInterface.Data.GetOutfitIndex(rightGirlId, rightStyle.OutfitId) ?? -1,
															   soulGirlDefTwo);
				}
			}

			Game.Session.gameCanvas.dollMiddle.UnloadGirl();
		}

		private static void DetermineStyles(GirlDefinition leftGirl,
			                                GirlDefinition rightGirl,
											LocationDefinition currentLocation,
											PlayerFileGirlPair playerFileGirlPair,
											bool unpairedStyles,
											out GirlStyleInfo leftStyle,
											out GirlStyleInfo rightStyle)
        {
			leftStyle = GirlStyleInfo.Default();
			rightStyle = GirlStyleInfo.Default();

			// random styles
			if (Codes.RandomStylesCodeId.HasValue
				&& ModInterface.IsCodeUnlocked(Codes.RandomStylesCodeId.Value))
            {
				if (leftGirl != null)
                {
					RandomizeStyle(leftGirl,
								   unpairedStyles,
								   out leftStyle);
				}

				if (rightGirl != null)
				{
					RandomizeStyle(rightGirl,
								   unpairedStyles,
								   out rightStyle);
				}
			}
			// style by location
			else
			{
				switch (currentLocation.locationType)
				{
					case LocationType.SIM:
						// meeting styles
						if (playerFileGirlPair != null
							&& playerFileGirlPair.relationshipType == GirlPairRelationshipType.UNKNOWN)
						{
							var pairStyleInfo = ModInterface.Data.GetPairStyleInfo(ModInterface.Data.GetDataId(GameDataType.GirlPair, playerFileGirlPair.girlPairDefinition.id));
							
							if (playerFileGirlPair.girlPairDefinition.hasMeetingStyleOne)
							{
								leftStyle = pairStyleInfo.MeetingGirlOne;
							}

							if (playerFileGirlPair.girlPairDefinition.hasMeetingStyleTwo)
							{
								rightStyle = pairStyleInfo.MeetingGirlTwo;
							}
						}
						break;
					case LocationType.DATE:
						// sex styles
						if (playerFileGirlPair != null
							&& playerFileGirlPair.relationshipType == GirlPairRelationshipType.ATTRACTED
							&& Game.Persistence.playerFile.daytimeElapsed % 4 == (int)playerFileGirlPair.girlPairDefinition.sexDaytime)
						{
							var pairStyleInfo = ModInterface.Data.GetPairStyleInfo(ModInterface.Data.GetDataId(GameDataType.GirlPair, playerFileGirlPair.girlPairDefinition.id));

							leftStyle = pairStyleInfo.SexGirlOne;
							rightStyle = pairStyleInfo.SexGirlTwo;
						}
						// location or player defined styles
						else if (!Game.Session.Puzzle.puzzleStatus.isEmpty
								 && currentLocation != Game.Session.Puzzle.bossLocationDefinition)
						{
							var locationId = ModInterface.Data.GetDataId(GameDataType.Location, currentLocation.id);

							if (!Game.Session.Puzzle.puzzleStatus.girlStatusLeft.playerFileGirl.stylesOnDates)
							{
								leftStyle = ModInterface.Data.TryGetLocationStyleInfo(locationId, ModInterface.Data.GetDataId(GameDataType.Girl, leftGirl.id)) ?? GirlStyleInfo.Default();
							}

							if (!Game.Session.Puzzle.puzzleStatus.girlStatusRight.playerFileGirl.stylesOnDates)
							{
								rightStyle = ModInterface.Data.TryGetLocationStyleInfo(locationId, ModInterface.Data.GetDataId(GameDataType.Girl, rightGirl.id)) ?? GirlStyleInfo.Default();
							}
						}
						break;
				}
			}
		}
	}
}
