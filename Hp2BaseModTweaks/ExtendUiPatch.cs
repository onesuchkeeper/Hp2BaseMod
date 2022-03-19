// Hp2BaseModTweaks 2021, By OneSuchKeeper

using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
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
    }

    /// <summary>
    /// Addds PostWindow handlers to WindowManager 
    /// </summary>
    [HarmonyPatch(typeof(LocationManager), MethodType.Constructor)]
    public static class ExtendLocationManager
    {
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
            __instance.WindowShownEvent += Functionality.PostWindowChange;
        }
    }

    /// <summary>
    /// 
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
    /// Addds PostWindow handlers to WindowManager 
    /// </summary>
    [HarmonyPatch(typeof(UiWindowPhotos), MethodType.Constructor)]
    public static class PostOnPhotoComplete
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
        public static void foo()
        {

        }

        public static void PostAppChange()
        {
            Harmony.DEBUG = true;

            var currentApp = AccessTools.Field(typeof(UiCellphone), "_currentApp")
                                        .GetValue(Game.Session.gameCanvas.cellphone)
                                        as UiCellphoneApp;

            if (currentApp is UiCellphoneAppGirls appGirls)
            {
                var playerFileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(o => o.girlDefinition.id).ToArray();

                if (playerFileGirls.Length > AssetHolder.GirlsPerPage)
                {
                    // wrap index
                    var pagesCap = (playerFileGirls.Length / AssetHolder.GirlsPerPage);

                    if (AssetHolder.Instance.GirlPageIndex < 0)
                    {
                        AssetHolder.Instance.GirlPageIndex = pagesCap;
                    }
                    else if (AssetHolder.Instance.GirlPageIndex > pagesCap)
                    {
                        AssetHolder.Instance.GirlPageIndex = 0;
                    }

                    // set backend data
                    var index = AssetHolder.Instance.GirlPageIndex * AssetHolder.GirlsPerPage;
                    var kyuDef = Game.Data.Girls.Get(13);

                    foreach (var slot in appGirls.girlSlots.Take(AssetHolder.GirlsPerPage))
                    {
                        GirlDefinition newDef = null;

                        if (index < playerFileGirls.Length)
                        {
                            newDef = playerFileGirls[index].girlDefinition;
                            index++;
                        }
                        else
                        {
                            newDef = kyuDef;
                        }

                        slot.girlDefinition = newDef;
                    }

                    foreach (var slot in appGirls.girlSlots.Skip(AssetHolder.GirlsPerPage))
                    {
                        slot.girlDefinition = kyuDef;
                    }

                    // Refresh Ui

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

                    // Refresh
                    if (AssetHolder.Instance.GirlAppNeedsRefresh)
                    {
                        AssetHolder.Instance.GirlAppNeedsRefresh = false;
                        Game.Session.gameCanvas.cellphone.Refresh(true);
                    }
                }
            }
            else
            {
                AssetHolder.Instance.GirlPageIndex = 0;
                AssetHolder.Instance.GirlAppNeedsRefresh = true;
            }

            if (currentApp is UiCellphoneAppPairs appPairs)
            {
                var playerFilePairs = Game.Persistence.playerFile.metGirlPairs.ToArray();

                void updateSlots()
                {
                    // wrap index
                    var pagesCap = (playerFilePairs.Length / AssetHolder.PairsPerPage);

                    if (AssetHolder.Instance.PairPageIndex < 0)
                    {
                        AssetHolder.Instance.PairPageIndex = pagesCap;
                    }
                    else if (AssetHolder.Instance.PairPageIndex > pagesCap)
                    {
                        AssetHolder.Instance.PairPageIndex = 0;
                    }

                    // Set backend data
                    var index = AssetHolder.Instance.PairPageIndex * AssetHolder.PairsPerPage;
                    int num = -1;

                    foreach (var slot in appPairs.pairSlots.Take(AssetHolder.PairsPerPage))
                    {
                        GirlPairDefinition newDef = null;

                        if (index < playerFilePairs.Length)
                        {
                            newDef = playerFilePairs[index];
                            slot.canvasGroup.alpha = 1f;
                            slot.canvasGroup.blocksRaycasts = true;
                            slot.button.Enable();
                            index++;
                            num++;
                        }

                        slot.rectTransform.anchoredPosition = new Vector2((float)(num % 4) * 256f, (float)Mathf.FloorToInt((float)num / 4f) * -90f);
                        slot.Populate(newDef);
                    }

                    foreach (var slot in appPairs.pairSlots.Skip(AssetHolder.PairsPerPage))
                    {
                        slot.Populate(null);
                    }

                    num++;

                    appPairs.pairSlotsContainer.anchoredPosition =
                        //Game.Session.gameCanvas.cellphoneContainer.anchoredPosition +
                        //Game.Session.gameCanvas.cellphone.rectTransform.anchoredPosition
                        new Vector2(528, -282)
                        + new Vector2((float)Mathf.Min(num - 1, 3) * -128f, (float)Mathf.Max(Mathf.CeilToInt((float)num / 4f) - 1, 0) * 45f);
                }

                if (playerFilePairs.Length > AssetHolder.PairsPerPage)
                {
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
                        updateSlots();
                    }));

                    buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.PairPageIndex += 1;
                        buttonLeftButton.interactable = true;
                        updateSlots();
                    }));
                }

                updateSlots();
            }
            else
            {
                AssetHolder.Instance.PairPageIndex = 0;
            }

            if (currentApp is UiCellphoneAppWardrobe appWardrobe)
            {
                var playerFileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(o => o.girlDefinition.id).ToArray();

                // Setup icons
                if (playerFileGirls.Length > AssetHolder.WarbrobeGirlsPerPage)
                {
                    // wrap index
                    var pagesCap = (playerFileGirls.Length / AssetHolder.WarbrobeGirlsPerPage);

                    if (AssetHolder.Instance.WarbrobeGirlPageIndex < 0)
                    {
                        AssetHolder.Instance.WarbrobeGirlPageIndex = pagesCap;
                    }
                    else if (AssetHolder.Instance.WarbrobeGirlPageIndex > pagesCap)
                    {
                        AssetHolder.Instance.WarbrobeGirlPageIndex = 0;
                    }

                    // Set backend data
                    var index = AssetHolder.Instance.WarbrobeGirlPageIndex * AssetHolder.WarbrobeGirlsPerPage;
                    var kyuDef = Game.Data.Girls.Get(13);

                    foreach (var slot in appWardrobe.fileIconSlots.Take(AssetHolder.WarbrobeGirlsPerPage))
                    {
                        GirlDefinition newDef = null;

                        if (index < playerFileGirls.Length)
                        {
                            newDef = playerFileGirls[index].girlDefinition;
                            index++;
                        }
                        else
                        {
                            newDef = kyuDef;
                        }

                        slot.girlDefinition = newDef;
                    }

                    foreach (var slot in appWardrobe.fileIconSlots.Skip(AssetHolder.WarbrobeGirlsPerPage))
                    {
                        slot.girlDefinition = kyuDef;
                    }

                    var firstIconSlot = appWardrobe.fileIconSlots.First();

                    var selectedFileIconSlot = AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_selectedFileIconSlot");
                    selectedFileIconSlot.SetValue(appWardrobe, appWardrobe.fileIconSlots[0]);

                    // Refresh Ui
                    if (AssetHolder.Instance.WardropeNeedsRefresh)
                    {
                        AssetHolder.Instance.WardropeNeedsRefresh = false;
                        Game.Session.gameCanvas.cellphone.Refresh(true);
                        return;
                    }

                    var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(firstIconSlot.girlDefinition);
                    var wardrobeDoll = Game.Session.gameCanvas.dollRight;

                    if (firstIconSlot.girlDefinition != wardrobeDoll.girlDefinition)
                    {
                        Game.Persistence.playerFile.SetFlagValue("wardrobe_girl_id", firstIconSlot.girlDefinition.id);
                        wardrobeDoll.LoadGirl(firstIconSlot.girlDefinition, -1, -1, -1, null);

                        appWardrobe.selectListHairstyle.Populate(playerFileGirl);
                        appWardrobe.selectListOutfit.Populate(playerFileGirl);
                        appWardrobe.wearOnDatesCheckBox.Populate(playerFileGirl.stylesOnDates);
                    }
                    else
                    {
                        wardrobeDoll.ChangeHairstyle(-1);
                        wardrobeDoll.ChangeOutfit(-1);
                    }

                    // Create buttons for girl icons
                    var buttonLeftGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], appWardrobe.fileIconSlotsContainer.parent);
                    var buttonLeftButton = buttonLeftGO.GetComponent<Button>();
                    var buttonLeftTransform = buttonLeftGO.GetComponent<RectTransform>();

                    buttonLeftTransform.anchoredPosition = new Vector2(30f, -30f);

                    buttonLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.WarbrobeGirlPageIndex -= 1;
                        Game.Session.gameCanvas.cellphone.Refresh(true);
                    }));

                    var buttonRightGO = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], appWardrobe.fileIconSlotsContainer.parent);
                    var buttonRightButton = buttonRightGO.GetComponent<Button>();
                    var buttonRightTransform = buttonRightGO.GetComponent<RectTransform>();

                    buttonRightTransform.anchoredPosition = new Vector2(356f, -30f);

                    buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.WarbrobeGirlPageIndex += 1;
                        Game.Session.gameCanvas.cellphone.Refresh(true);
                    }));

                    // Buttons for hair & outfits, Todo
                }
            }
            else
            {
                AssetHolder.Instance.WarbrobeGirlPageIndex = 0;
                AssetHolder.Instance.WardropeNeedsRefresh = true;
            }

            if (currentApp is UiCellphoneAppNew appNew)
            {
                //Add buttons for head sprites
            }
            else
            {
                //reset index to 0
            }

            if (currentApp is UiCellphoneAppProfile appProfile)
            {
                //Add buttons for pairs
            }
            else
            {
                //reset index to 0
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
                var photos = Game.Data.Photos.GetAll().OrderBy(o => o.id).ToArray();

                void updateSlots()
                {
                    // wrap index
                    var pagesCap = (photos.Length - 2) / AssetHolder.PhotossPerPage;
                    if (AssetHolder.Instance.PhotoPageIndex < 0)
                    {
                        AssetHolder.Instance.PhotoPageIndex = pagesCap;
                    }
                    else if (AssetHolder.Instance.PhotoPageIndex > pagesCap)
                    {
                        AssetHolder.Instance.PhotoPageIndex = 0;
                    }

                    // set backend data, +2 for kyu's extra photos
                    var index = AssetHolder.Instance.PhotoPageIndex == 0
                        ? AssetHolder.Instance.PhotoPageIndex * AssetHolder.PhotossPerPage
                        : (AssetHolder.Instance.PhotoPageIndex * AssetHolder.PhotossPerPage) + 2;


                    var photoDefinition = AccessTools.Field(typeof(UiPhotoSlot), "_photoDefinition");

                    foreach (var slot in windowPhotos.photoSlots)
                    {
                        if (index < photos.Length)
                        {
                            photoDefinition.SetValue(slot, photos[index]);
                            slot.buttonBehavior.Enable();
                            index++;
                        }
                        else
                        {
                            photoDefinition.SetValue(slot, null);
                            slot.thumbnailImage = null;
                            slot.buttonBehavior.Disable();
                        }

                        slot.Refresh(0);
                    }

                    // first page has special handling with kyu and the nymphojin stuff
                    if (AssetHolder.Instance.PhotoPageIndex == 0)
                    {
                        //set player's kyu choice
                    }
                }

                // -2 for Kyu's potos
                if (photos.Length - 2 > AssetHolder.PhotossPerPage)
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
                        AssetHolder.Instance.PhotoPageIndex -= 1;
                        updateSlots();
                    }));

                    buttonRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        AssetHolder.Instance.PhotoPageIndex += 1;
                        updateSlots();
                    }));
                }
            }

            Harmony.DEBUG = false;
        }

        // This has to be subcribed later in execution
        public static void SubscribePostLocation()
        {
            if (!AssetHolder.Instance.SubscribedOpenEvent)
            {
                Game.Session.gameCanvas.cellphone.OpenedEvent += PostAppChange;
                AssetHolder.Instance.SubscribedOpenEvent = true;
            }
        }
    }
}
