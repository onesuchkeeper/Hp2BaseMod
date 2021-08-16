// Hp2BaseModTweaks 2021, By OneSuchKeeper

using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
	//Add handel to hub manager

	/// <summary>
	/// Loads button prefabs into asset manager
	/// </summary>
	[HarmonyPatch(typeof(GameData), MethodType.Constructor)]
	public static class ExtendGameData
	{
		public static void Prefix(GameData __instance)
        {
			try
			{
				new AssetHolder().Awake();
			}
			catch (Exception e)
			{
				Harmony.DEBUG = true;
				FileLog.Log(e.Message);
			}
		}
	}

	/// <summary>
	/// Adds PostApp handlers to UiCellphone
	/// </summary>
	[HarmonyPatch(typeof(UiCellphone))]
	public static class ExtendUiCellPhone
	{
		[HarmonyPostfix]
		[HarmonyPatch("LoadClosedApp")]
		private static void PostfixLoadClosedApp(UiCellphone __instance) { Functionality.PostAppChange(); }

		[HarmonyPatch("LoadOpenApp")]
		private static void PostfixLoadOpenApp(UiCellphone __instance) { Functionality.PostAppChange(); }

		[HarmonyPostfix]
		[HarmonyPatch("OnCellphoneButtonPressed")]
		private static void PostfixOnCellphoneButtonPressed(UiCellphone __instance, UiCellphoneButton cellphoneButton) { Functionality.PostAppChange(); }

		[HarmonyPostfix]
		[HarmonyPatch("Refresh")]
		private static void PostfixRefresh(UiCellphone __instance, bool hard) { Functionality.PostAppChange(); }

		// Open and LoadApp Cause issues :(
		// and extending OnAppButtonPressed or wa
	}

	/// <summary>
	/// Addds PostWindow handlers to WindowManager 
	/// </summary>
	[HarmonyPatch(typeof(LocationManager), MethodType.Constructor)]
	public static class ExtendLocationManager
	{
		// This is pretty hackey, I would have prefered to do this anywhere else but the cell phone is
		// part of the title screen as well so this keeps crashing. SOLID my man... I beg you
		private static void Postfix(LocationManager __instance)
		{
			__instance.ArriveEvent += Functionality.SubscribePostLocation;
		}
	}

	/// <summary>
	/// Addds PostWindow handlers to WindowManager 
	/// </summary>
	[HarmonyPatch(typeof(WindowManager), MethodType.Constructor)]
	public static class ExtendWindowManager
	{
		private static void Postfix(WindowManager __instance)
        {
            __instance.WindowShowEvent += Functionality.PostWindowChange;
        }
    }

	/// <summary>
	/// Addds PostWindow handlers to WindowManager 
	/// </summary>
	[HarmonyPatch(typeof(GameSession), MethodType.Constructor)]
	public static class ExtendGameSession
	{
		private static void Postfix(GameSession __instance)
		{
			AssetHolder.Instance.SubscribedOpenEvent = false;
		}
	}

	/// <summary>
	/// Contains handlers
	/// </summary>
	public static class Functionality
    {
		public static void PostAppChange()
		{
			Harmony.DEBUG = true;

			var currentApp = AccessTools.Field(typeof(UiCellphone), "_currentApp")
										.GetValue(Game.Session.gameCanvas.cellphone)
										as UiCellphoneApp;

			if (currentApp is UiCellphoneAppGirls appGirls)
            {
				var playerFileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).ToArray();

				if (playerFileGirls.Length > AssetHolder.GirlsPerPage)
                {
					var pagesCap = (playerFileGirls.Length / AssetHolder.GirlsPerPage);

					AssetHolder.Instance.GirlPageIndex = (pagesCap == 0)
						? 0
						: NfMod(AssetHolder.Instance.GirlPageIndex, pagesCap);

					if (AssetHolder.Instance.GirlPageIndex != 0)
					{
						var index = AssetHolder.Instance.GirlPageIndex * AssetHolder.GirlsPerPage;

						foreach (var slot in appGirls.girlSlots)
						{
							GirlDefinition newDef = null;

							if (index < playerFileGirls.Length)
							{
								newDef = playerFileGirls[index].girlDefinition;
								index++;
							}
							else
							{
								newDef = Game.Data.Girls.Get(13);
							}

							slot.girlDefinition = newDef;
						}
					}

					// Create buttons
					var buttonLeftGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], appGirls.girlSlotsContainer.parent);
                    var buttonLeftButton = buttonLeftGO.GetComponent<Button>();
                    var buttonLeftTransform = buttonLeftGO.GetComponent<RectTransform>();

                    var buttonRightGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], appGirls.girlSlotsContainer.parent);
                    var buttonRightButton = buttonRightGO.GetComponent<Button>();
                    var buttonRightTransform = buttonRightGO.GetComponent<RectTransform>();

                    buttonLeftTransform.anchoredPosition = new Vector2(30f, -30f);
                    buttonRightTransform.anchoredPosition = new Vector2(1026f, -30f);

                    buttonLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.GirlPageIndex -= 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));

                    buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.GirlPageIndex += 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));
                }
			}
            else
            {
				AssetHolder.Instance.GirlPageIndex = 0;
			}

			if (currentApp is UiCellphoneAppPairs appPairs)
			{
				var playerFilePairs = Game.Persistence.playerFile.metGirlPairs.ToArray();

				if (playerFilePairs.Length > AssetHolder.PairsPerPage)
				{
					// setup page
					var pagesCap = (playerFilePairs.Length / AssetHolder.PairsPerPage);

					AssetHolder.Instance.PairPageIndex = (pagesCap == 0)
						? 0
						: NfMod(AssetHolder.Instance.PairPageIndex, pagesCap);

					if (AssetHolder.Instance.PairPageIndex != 0)
					{
						var index = AssetHolder.Instance.PairPageIndex * AssetHolder.PairsPerPage;

						foreach (var slot in appPairs.pairSlots)
						{
							GirlPairDefinition newDef = null;

							if (index < playerFilePairs.Length)
							{
								newDef = playerFilePairs[index];
								index++;
							}

							slot.Populate(newDef);
						}
					}

					// Create buttons
					var buttonLeftGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], appPairs.pairSlotsContainer.parent);
					var buttonLeftButton = buttonLeftGO.GetComponent<Button>();
					var buttonLeftTransform = buttonLeftGO.GetComponent<RectTransform>();

					var buttonRightGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], appPairs.pairSlotsContainer.parent);
					var buttonRightButton = buttonRightGO.GetComponent<Button>();
					var buttonRightTransform = buttonRightGO.GetComponent<RectTransform>();

					buttonLeftTransform.anchoredPosition = new Vector2(30f, -30f);
					buttonRightTransform.anchoredPosition = new Vector2(1026f, -30f);

					buttonLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
					{
						AssetHolder.Instance.PairPageIndex -= 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));

					buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
					{
						AssetHolder.Instance.PairPageIndex += 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));
				}
			}
			else
            {
				AssetHolder.Instance.PairPageIndex = 0;
			}

			if (currentApp is UiCellphoneAppWardrobe appWardrobe)
            {
				var playerFileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).ToArray();

				// Setup icons
				if (playerFileGirls.Length > AssetHolder.WarbrobeGirlsPerPage)
                {
					var pagesCap = (playerFileGirls.Length / AssetHolder.WarbrobeGirlsPerPage);

					AssetHolder.Instance.WarbrobeGirlPageIndex = (pagesCap == 0)
						? 0
						: NfMod(AssetHolder.Instance.WarbrobeGirlPageIndex, pagesCap);

					if (AssetHolder.Instance.WarbrobeGirlPageIndex != 0)
					{
						var index = AssetHolder.Instance.WarbrobeGirlPageIndex * AssetHolder.WarbrobeGirlsPerPage;

						foreach (var slot in appWardrobe.fileIconSlots)
						{
							GirlDefinition newDef = null;

							if (index < playerFileGirls.Length)
							{
								newDef = playerFileGirls[index].girlDefinition;
								index++;
							}
                            else
                            {
								newDef = Game.Data.Girls.Get(13);
							}

							slot.girlDefinition = newDef;
						}
                    }

					// Create buttons for girl icons
					var buttonLeftGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], appWardrobe.fileIconSlotsContainer.parent);
					var buttonLeftButton = buttonLeftGO.GetComponent<Button>();
					var buttonLeftTransform = buttonLeftGO.GetComponent<RectTransform>();

					var buttonRightGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], appWardrobe.fileIconSlotsContainer.parent);
					var buttonRightButton = buttonRightGO.GetComponent<Button>();
					var buttonRightTransform = buttonRightGO.GetComponent<RectTransform>();

					buttonLeftTransform.anchoredPosition = new Vector2(30f, -30f);
					buttonRightTransform.anchoredPosition = new Vector2(356f, -30f);

					buttonLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
					{
						AssetHolder.Instance.WarbrobeGirlPageIndex -= 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));

					buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
					{
						AssetHolder.Instance.WarbrobeGirlPageIndex += 1;
						Game.Session.gameCanvas.cellphone.Refresh(true);
					}));
				}
			}
			else
            {
				AssetHolder.Instance.WarbrobeGirlPageIndex = 0;
            }

			if (currentApp is UiCellphoneAppNew appNew)
            {
				//Add buttons for head sprites
            }

			Harmony.DEBUG = false;
		}

		public static void PostWindowChange()
        {
			Harmony.DEBUG = true;

			var currentWindow = AccessTools.Field(typeof(WindowManager), "_currentWindow").GetValue(Game.Manager.Windows) as UiWindow;

			if (currentWindow == null) { return; }

			if (currentWindow is UiWindowPhotos windowPhotos)
            {
				if (Game.Data.Photos.GetAll().Count > AssetHolder.PhotossPerPage)
                {
					var slotsContainer = windowPhotos.transform.GetChild(1).GetChild(0);

                    // Create buttons
                    var buttonLeftGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[0], slotsContainer);
                    var buttonLeftButton = buttonLeftGO.GetComponent<Button>();
                    var buttonLeftTransform = buttonLeftGO.GetComponent<RectTransform>();

                    var buttonRightGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[1], slotsContainer);
                    var buttonRightButton = buttonRightGO.GetComponent<Button>();
                    var buttonRightTransform = buttonRightGO.GetComponent<RectTransform>();

                    buttonLeftTransform.anchoredPosition = new Vector2(-110f, 125f);
                    buttonRightTransform.anchoredPosition = new Vector2(1688f, 125f);

                    buttonLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.GirlPageIndex -= 1;
						FileLog.Log("Left BOOP");
					}));

                    buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.GirlPageIndex += 1;
						FileLog.Log("Right BOOP");
					}));
                }
            }

			Harmony.DEBUG = false;
		}

		public static void SubscribePostLocation()
        {
			if (!AssetHolder.Instance.SubscribedOpenEvent)
            {
				Game.Session.gameCanvas.cellphone.OpenedEvent += PostAppChange;
				AssetHolder.Instance.SubscribedOpenEvent = true;
			}
        }

        public static int NfMod(int x, int m) => (x % m + m) % m;
	}
}
