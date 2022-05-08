// Hp2BaseMod 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    internal static class CellphoneController
    {
        private static readonly Vector2 _cellphoneTopLeft = new Vector2(-494, 76);
        private static readonly Vector2 _cellphoneTopRight = new Vector2(494, 76);
        private static readonly Vector2 _hubCellphoneTopLeft = new Vector2(-854, 76);
        private static readonly Vector2 _hubCellphoneTopRight = new Vector2(136, 76);
        private static readonly Vector2 _hubCellphoneMidLeft = new Vector2(-536, 76);
        private static readonly Vector2 _hubCellphoneMidRight = new Vector2(-464, 76);

        private static CellphoneButton _leftAppButton;
        private static CellphoneButton _rightAppButton;

        private static CellphoneButton _leftStyleButton;
        private static CellphoneButton _rightStyleButton;

        private static int _appPageIndex;
        private static int _stylePageIndex;
        private static int _stylePageMax;

        private static bool _hasInit;
        private static bool _isFirstCellphoneOpen;
        private static bool _isSecondCellphoneOpen;

        public static void OnCellphoneOpen()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            if (_isFirstCellphoneOpen && _hasInit)
            {
                _isSecondCellphoneOpen = true;

                _leftAppButton.Hide();
                _rightAppButton.Hide();
                _leftStyleButton.Hide();
                _rightStyleButton.Hide();
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void OnCellphoneOpened()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            if (!_isSecondCellphoneOpen)
            {
                Init();
                
                _isFirstCellphoneOpen = true;

                if (Game.Session?.gameCanvas?.cellphone != null)
                {
                    var currentApp = AccessTools.Field(typeof(UiCellphone), "_currentApp")
                                                .GetValue(Game.Session.gameCanvas.cellphone)
                                                as UiCellphoneApp;

                    // wardrobe has special handling for initial pages since the displayed girl is saved
                    if (currentApp is UiCellphoneAppWardrobe)
                    {
                        var girlDef = Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id"));
                        var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDef);

                        var girlsList = Game.Persistence.playerFile.girls.Where(x => x.playerMet).ToList();
                        var girlIndex = girlsList.IndexOf(playerFileGirl);

                        _appPageIndex = girlIndex / Constants.GirlsPerPage;

                        _stylePageIndex = playerFileGirl.outfitIndex / Constants.WarbrobeStylesPerPage;

                        SetupApp(currentApp, false);
                    }
                    else
                    {
                        SetupApp(currentApp, true);
                    }
                }
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void OnCellphoneClose()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            if (_isSecondCellphoneOpen)
            {
                _isSecondCellphoneOpen = false;
                SetupCurrentApp(true);
            }
            else if (_isFirstCellphoneOpen && _hasInit)
            {
                _isFirstCellphoneOpen = false;

                _leftAppButton.Hide();
                _rightAppButton.Hide();
                _leftStyleButton.Hide();
                _rightStyleButton.Hide();
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        // when the game restarts it doesn't call the cellphone close event, 
        public static void PostUiCellphoneConstructor()
        {
            _appPageIndex = 0;
            _stylePageIndex = 0;
            _stylePageMax = 0;

            _hasInit = false;
            _isFirstCellphoneOpen = false;
            _isSecondCellphoneOpen = false;
        }

        public static void PostCellphoneButtonPressed()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            _appPageIndex = 0;
            SetupCurrentApp(true);

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void UiCellphoneAppGirls_PostGirlSlotPressed()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            SetupCurrentApp();

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void UiCellphoneAppWardrobe_PreListItemSelected(UiAppStyleSelectList list)
        {
            ModInterface.Instance.LogLine();

            var playerFileGirl = AccessTools.Field(typeof(UiAppStyleSelectList), "_playerFileGirl")
                                            .GetValue(list)
                                            as PlayerFileGirl;

            var listIndexOffset = _stylePageIndex * Constants.WarbrobeStylesPerPage;

            if (!list.alternative)
            {
                playerFileGirl.hairstyleIndex += listIndexOffset;
            }
            else
            {
                playerFileGirl.outfitIndex += listIndexOffset;
            }
        }

        public static void UiCellphoneAppWardrobe_PreFileIconSlotSelected(UiCellphoneAppWardrobe appWardrobe,
                                                                          UiAppFileIconSlot fileIconSlot)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            Game.Persistence.playerFile.SetFlagValue("wardrobe_girl_id", fileIconSlot.girlDefinition.id);

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void UiCellphoneAppWardrobe_PostFileIconSlotSelected(UiCellphoneAppWardrobe appWardrobe,
                                                                           UiAppFileIconSlot fileIconSlot)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var girlDef = Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id"));
            var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDef);

            _stylePageIndex = playerFileGirl.outfitIndex / Constants.WarbrobeStylesPerPage;
            SetupWardrobeStyles(appWardrobe, playerFileGirl);

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void UiCellphoneAppWardrobe_PostWardrobeRefresh(UiCellphoneAppWardrobe appWardrobe)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var girlDef = Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id"));

            ModInterface.Instance.LogLine($"refreshing {girlDef.girlName}");

            var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDef);
            var wardrobeDoll = AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_wardrobeDoll").GetValue(appWardrobe) as UiDoll;

            var girlsList = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(x => x.girlDefinition.id).ToList();
            var girlIndex = girlsList.IndexOf(playerFileGirl);

            var girlPageIndex = girlIndex / Constants.GirlsPerPage;

            // the source refresh screws up if the selected icon isn't on the page so redo it here
            if (_appPageIndex != girlPageIndex)
            {
                wardrobeDoll.LoadGirl(girlDef, -1, playerFileGirl.hairstyleIndex, playerFileGirl.outfitIndex, null);
                appWardrobe.wearOnDatesCheckBox.Populate(playerFileGirl.stylesOnDates);
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        #region setup app

        private static void Init()
        {
            Transform parent = null;
            
            // unity throws if you try to get the parent of a destroied object
            // unity doesn't handle the null accessor the same as it handles '== null'
            // don't be like unity, all my homies hate unity
            if (Game.Session?.gameCanvas?.cellphoneContainer != null)
            {
                parent = Game.Session?.gameCanvas?.cellphoneContainer.parent;
            }
            
            if (!_hasInit && parent != null)
            {
                ModInterface.Instance.LogLine();
                _hasInit = true;
                _appPageIndex = 0;
                _stylePageIndex = 0;

                var cellphoneTextureLeft = AssetHolder.Instance.Sprites["ui_app_setting_arrow_left"];
                var cellphoneTextureLeftOver = AssetHolder.Instance.Sprites["ui_app_setting_arrow_left_over"];
                var cellphoneTextureRight = AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"];
                var cellphoneTextureRightOver = AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"];
                var cellphoneButtonPressedSfx = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"];

                _leftAppButton = new CellphoneButton("Hp2BaseModTweaks.CellphoneController.leftAppButton",
                                                     cellphoneTextureLeft,
                                                     cellphoneTextureLeftOver,
                                                     cellphoneButtonPressedSfx);

                _leftAppButton.AddClickListener(() =>
                {
                    _appPageIndex--;
                    SetupCurrentApp();
                });

                _rightAppButton = new CellphoneButton("Hp2BaseModTweaks.CellphoneController.rightAppButton",
                                                     cellphoneTextureRight,
                                                     cellphoneTextureRightOver,
                                                     cellphoneButtonPressedSfx);

                _rightAppButton.AddClickListener(() =>
                {
                    _appPageIndex++;
                    SetupCurrentApp();
                });

                _leftStyleButton = new CellphoneButton("Hp2BaseModTweaks.CellphoneController.leftStyleButton",
                                                       cellphoneTextureLeft,
                                                       cellphoneTextureLeftOver,
                                                       cellphoneButtonPressedSfx);

                _leftStyleButton.AddClickListener(() =>
                {
                    _stylePageIndex = Utility.WrapIndex(_stylePageIndex-1, _stylePageMax);
                    SetupWardrobeStyles();
                });

                _rightStyleButton = new CellphoneButton("Hp2BaseModTweaks.CellphoneController.rightStyleButton",
                                                       cellphoneTextureRight,
                                                       cellphoneTextureRightOver,
                                                       cellphoneButtonPressedSfx);

                _rightStyleButton.AddClickListener(() =>
                {
                    _stylePageIndex = Utility.WrapIndex(_stylePageIndex + 1, _stylePageMax);
                    SetupWardrobeStyles();
                });

                _leftAppButton.SetParent(parent);
                _rightAppButton.SetParent(parent);
                _leftStyleButton.SetParent(parent);
                _rightStyleButton.SetParent(parent);

                _leftAppButton.Hide();
                _rightAppButton.Hide();
                _leftStyleButton.Hide();
                _rightStyleButton.Hide();
            }
        }

        private static void SetupCurrentApp(bool buttonsOnly = false)
        {
            if (Game.Session?.gameCanvas?.cellphone != null)
            {
                SetupApp(AccessTools.Field(typeof(UiCellphone), "_currentApp")
                                    .GetValue(Game.Session.gameCanvas.cellphone)
                                    as UiCellphoneApp,
                         buttonsOnly);
            }
        }

        private static void SetupApp(UiCellphoneApp app, bool buttonsOnly)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            bool isPastTutorial = Game.Persistence.playerFile.storyProgress >= 7;

            if (_isFirstCellphoneOpen && !_isSecondCellphoneOpen && isPastTutorial)
            {
                // wardrobe has special button placement
                if (app is UiCellphoneAppWardrobe appWardrobe)
                {
                    // position buttons
                    _leftAppButton.SetLocalPostion(_hubCellphoneTopLeft);
                    _rightAppButton.SetLocalPostion(_hubCellphoneMidLeft);
                    _leftStyleButton.SetLocalPostion(_hubCellphoneMidRight);
                    _rightStyleButton.SetLocalPostion(_hubCellphoneTopRight);

                    // discover page maxes
                    var girlsArray = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(x => x.girlDefinition.id).ToArray();
                    var girlsPageMax = girlsArray.Length > 1 ? (girlsArray.Length - 1) / Constants.GirlsPerPage : 0;

                    _appPageIndex = Utility.WrapIndex(_appPageIndex, girlsPageMax);

                    var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id")));

                    var outfitsCount = playerFileGirl.girlDefinition.outfits.Count;
                    var outfitsMax = outfitsCount > 1 ? (outfitsCount - 1) / Constants.WarbrobeStylesPerPage : 0;

                    var hairstylesCount = playerFileGirl.girlDefinition.hairstyles.Count;
                    var hairstylesMax = hairstylesCount > 1 ? (hairstylesCount - 1) / Constants.WarbrobeStylesPerPage : 0;

                    _stylePageMax = Math.Max(outfitsMax, hairstylesMax);

                    _stylePageIndex = Utility.WrapIndex(_stylePageIndex, _stylePageMax);

                    // setup app
                    if (!buttonsOnly)
                    {
                        if (girlsPageMax > 0)
                        {
                            SetupWardrobeGirls(appWardrobe, girlsArray);
                        }

                        if (_stylePageMax > 0)
                        {
                            SetupWardrobeStyles(appWardrobe, playerFileGirl);
                        }
                    }

                    // setup buttons
                    if (girlsPageMax > 0)
                    {
                        _leftAppButton.Show();
                        _rightAppButton.Show();
                    }
                    else
                    {
                        _leftAppButton.Hide();
                        _rightAppButton.Hide();
                    }

                    if (_stylePageMax > 0)
                    {
                        _leftStyleButton.Show();
                        _rightStyleButton.Show();
                    }
                    else
                    {
                        _leftStyleButton.Hide();
                        _rightStyleButton.Hide();
                    }
                }
                // default button placement
                else
                {
                    // position buttons
                    if (Game.Session.Location.currentLocation.locationType == LocationType.HUB)
                    {
                        _leftAppButton.SetLocalPostion(_hubCellphoneTopLeft);
                        _rightAppButton.SetLocalPostion(_hubCellphoneTopRight);
                    }
                    else
                    {
                        _leftAppButton.SetLocalPostion(_cellphoneTopLeft);
                        _rightAppButton.SetLocalPostion(_cellphoneTopRight);
                    }

                    var appPageMax = 0;

                    // handlers for each app type
                    if (app is UiCellphoneAppFinder appFinder)
                    {
                        var simLocations = Game.Data.Locations.GetAll().Where(x => x.locationType == LocationType.SIM).ToArray();

                        appPageMax = simLocations.Length > 1 ? (simLocations.Length - 1) / Constants.FinderLocationsPerPage : 0;

                        _appPageIndex = Utility.WrapIndex(_appPageIndex, appPageMax);

                        var available = true;
                        bool settled = false;

                        if (Game.Session.Location.AtLocationType(new LocationType[1]))
                        {
                            settled = Game.Manager.Windows.IsWindowActive(Game.Session.Location.actionBubblesWindow, true, false);
                        }
                        else if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.DATE }))
                        {
                            settled = Game.Session.Puzzle.puzzleStatus.gameOver;
                        }
                        else if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.HUB }))
                        {
                            settled = ((Game.Session.Hub.hubStepType == HubStepType.ROOT
                                        && Game.Manager.Windows.IsWindowActive(Game.Session.Dialog.dialogOptionsWindow, true, false))
                                      || Game.Persistence.playerFile.GetFlagValue(Game.Session.Hub.firstLocationFlag) < 0);
                        }
                        else
                        {
                            available = false;
                            appPageMax = 0;
                            settled = false;
                        }

                        if (!buttonsOnly && available)
                        {
                            try
                            {
                                SetupFinderApp(appFinder, simLocations, settled);
                            }
                            catch (Exception ex)
                            {
                                ModInterface.Instance.LogLine(ex.Message);
                            }
                        }
                    }
                    else if (app is UiCellphoneAppPairs appPairs)
                    {
                        var pairs = Game.Persistence.playerFile.metGirlPairs.ToArray();

                        appPageMax = pairs.Length > 1 ? (pairs.Length - 1) / Constants.PairsPerPage : 0;

                        _appPageIndex = Utility.WrapIndex(_appPageIndex, appPageMax);

                        if (!buttonsOnly)
                        {
                            SetupPairsApp(appPairs, pairs);
                        }
                    }
                    else if (app is UiCellphoneAppGirls appGirls)
                    {
                        var playerfileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(x => x.girlDefinition.id).ToArray();

                        appPageMax = playerfileGirls.Length > 1 ? (playerfileGirls.Length - 1) / Constants.GirlsPerPage : 0;

                        _appPageIndex = Utility.WrapIndex(_appPageIndex, appPageMax);

                        if (!buttonsOnly)
                        {
                            SetupGirlsApp(appGirls, playerfileGirls);
                        }
                    }
                    else if (app is UiCellphoneAppProfile appProfile)
                    {
                        var profileGirl = Game.Data.Girls.Get(Game.Session.gameCanvas.cellphone.GetCellFlag("profile_girl_id"));

                        var pairsList = new List<GirlPairDefinition>();
                        foreach (var pair in Game.Persistence.playerFile.metGirlPairs)
                        {
                            if (pair.HasGirlDef(profileGirl))
                            {
                                pairsList.Add(pair);
                            }
                        }

                        var questions = Game.Data.Questions.GetAll().ToArray();

                        var pairsMax = pairsList.Count > 1 ? (pairsList.Count - 1) / Constants.ProfilePairsPerPage : 0;
                        var questionsMax = questions.Length > 1 ? (questions.Length - 1) / Constants.QuestionsPerPage : 0;

                        appPageMax = Mathf.Max(pairsMax, questionsMax);

                        _appPageIndex = Utility.WrapIndex(_appPageIndex, appPageMax);

                        if (!buttonsOnly)
                        {
                            SetupProfileApp(appProfile, pairsList, questions, profileGirl);
                        }
                    }

                    // setup buttons
                    if (appPageMax > 0)
                    {
                        _leftAppButton.Show();
                        _rightAppButton.Show();
                    }
                    else
                    {
                        _leftAppButton.Hide();
                        _rightAppButton.Hide();
                    }
                }
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupFinderApp(UiCellphoneAppFinder appFinder, LocationDefinition[] simLocations, bool settled)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var locationIndex = _appPageIndex * Constants.FinderLocationsPerPage;
            var playerFileFinderSlotAccess = AccessTools.Field(typeof(UiAppFinderSlot), "_playerFileFinderSlot");

            foreach (var slot in appFinder.finderSlots.Take(Constants.FinderLocationsPerPage))
            {
                if (locationIndex < simLocations.Length)
                {
                    slot.canvasGroup.alpha = 1f;
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.locationDefinition = simLocations[locationIndex];
                    var playerFileFinderSlot = Game.Persistence.playerFile.GetPlayerFileFinderSlot(slot.locationDefinition);

                    bool blocksRaycasts = true;
                    bool isUnavailable;

                    if (slot.locationDefinition.locationType == LocationType.SIM && slot.locationDefinition == Game.Session.Location.currentLocation)
                    {
                        if (Game.Session.Location.currentGirlPair)
                        {
                            slot.headSlotLeft.Populate(Game.Session.Location.currentGirlLeft);
                            slot.headSlotRight.Populate(Game.Session.Location.currentGirlRight);
                            slot.relationshipSlot.Populate(Game.Session.Location.currentGirlPair, 0, null, false);
                        }
                        isUnavailable = true;
                    }
                    else if (slot.locationDefinition.locationType == LocationType.SIM)
                    {
                        if (playerFileFinderSlot?.girlPairDefinition == null)
                        {
                            slot.headSlotLeft.itemIcon.color = Color.clear;
                            slot.headSlotRight.itemIcon.color = Color.clear;
                            slot.relationshipSlot.itemIcon.color = Color.clear;

                            isUnavailable = true;
                            blocksRaycasts = false;
                        }
                        else
                        {
                            slot.headSlotLeft.itemIcon.color = Color.white;
                            slot.headSlotRight.itemIcon.color = Color.white;
                            slot.relationshipSlot.itemIcon.color = Color.white;

                            if (!playerFileFinderSlot.sidesFlipped)
                            {
                                slot.headSlotLeft.Populate(playerFileFinderSlot.girlPairDefinition.girlDefinitionOne);
                                slot.headSlotRight.Populate(playerFileFinderSlot.girlPairDefinition.girlDefinitionTwo);
                            }
                            else
                            {
                                slot.headSlotLeft.Populate(playerFileFinderSlot.girlPairDefinition.girlDefinitionTwo);
                                slot.headSlotRight.Populate(playerFileFinderSlot.girlPairDefinition.girlDefinitionOne);
                            }
                            slot.relationshipSlot.Populate(playerFileFinderSlot.girlPairDefinition, 1, null, false);
                            isUnavailable = !settled;
                        }
                    }
                    else
                    {
                        isUnavailable = true;
                        blocksRaycasts = false;
                    }

                    if (isUnavailable)
                    {
                        if (slot.headSlotLeft.canvasGroup.alpha > 0f)
                        {
                            slot.headSlotLeft.canvasGroup.alpha = 0.25f;
                        }
                        if (slot.headSlotRight.canvasGroup.alpha > 0f)
                        {
                            slot.headSlotRight.canvasGroup.alpha = 0.25f;
                        }
                        if (slot.relationshipSlot.canvasGroup.alpha > 0f)
                        {
                            slot.relationshipSlot.canvasGroup.alpha = 0.25f;
                        }
                        if (slot.headSlotLeft.canvasGroup.blocksRaycasts)
                        {
                            slot.headSlotLeft.canvasGroup.blocksRaycasts = blocksRaycasts;
                        }
                        if (slot.headSlotRight.canvasGroup.blocksRaycasts)
                        {
                            slot.headSlotRight.canvasGroup.blocksRaycasts = blocksRaycasts;
                        }
                        if (slot.relationshipSlot.canvasGroup.blocksRaycasts)
                        {
                            slot.relationshipSlot.canvasGroup.blocksRaycasts = blocksRaycasts;
                        }
                        slot.locationIcon.color = ColorUtils.ColorAlpha(slot.locationIcon.color, 0.5f);
                        slot.locationLabel.color = ColorUtils.ColorAlpha(slot.locationLabel.color, 0.4f);
                        slot.locationButton.Disable();
                    }
                    else
                    {
                        slot.locationIcon.color = ColorUtils.ColorAlpha(slot.locationIcon.color, 1f);
                        slot.locationLabel.color = ColorUtils.ColorAlpha(slot.locationLabel.color, 1f);
                        slot.locationButton.Enable();
                    }

                    slot.locationIcon.sprite = slot.locationDefinition.finderLocationIcon;
                    slot.locationLabel.text = slot.locationDefinition.locationName.ToUpper();

                    int num = Mathf.RoundToInt(slot.locationLabel.preferredWidth);
                    if (num % 2 != 0)
                    {
                        num++;
                    }
                    slot.locationRectTransform.sizeDelta = new Vector2((float)(90 + num), slot.locationRectTransform.sizeDelta.y);

                    playerFileFinderSlotAccess.SetValue(slot, playerFileFinderSlot);

                    locationIndex++;
                }
                else
                {
                    slot.canvasGroup.alpha = 0f;
                    slot.canvasGroup.blocksRaycasts = false;
                }
            }

            foreach (var slot in appFinder.finderSlots.Skip(Constants.FinderLocationsPerPage))
            {
                slot.canvasGroup.alpha = 0f;
                slot.canvasGroup.blocksRaycasts = false;
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupPairsApp(UiCellphoneAppPairs appPairs, GirlPairDefinition[] pairs)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var renderCount = 0;

            SetupEnumerable(_appPageIndex, pairs.Length, Constants.PairsPerPage, appPairs.pairSlots,
                (slot, index) =>
                {
                    slot.Populate(pairs[index]);
                    slot.canvasGroup.alpha = 1f;
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.button.Enable();
                    slot.rectTransform.anchoredPosition = new Vector2((float)(renderCount % 4) * 256f,
                                                                      (float)Mathf.FloorToInt((float)renderCount / 4f) * -90f);
                    renderCount++;
                },
                (slot) => slot.Populate(null, null));

            appPairs.pairSlotsContainer.anchoredPosition = new Vector2(528, -284);

            appPairs.pairSlotsContainer.anchoredPosition += new Vector2(Mathf.Min(renderCount - 1, 3) * -128f,
                                                                    Mathf.Max(Mathf.CeilToInt((float)renderCount / 4f) - 1, 0) * 45f);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupWardrobeGirls(UiCellphoneAppWardrobe appWardrobe, PlayerFileGirl[] girlsArray)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            UiAppFileIconSlot selectedFileIconSlot = null;
            var iconIndex = _appPageIndex * Constants.GirlsPerPage;
            int renderedCount = 0;

            foreach (var slot in appWardrobe.fileIconSlots.Take(Constants.GirlsPerPage))
            {
                if (iconIndex < girlsArray.Length)
                {
                    slot.button.Disable();
                    slot.girlDefinition = girlsArray[iconIndex].girlDefinition;
                    slot.Populate(false);
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.canvasGroup.alpha = 1f;
                    slot.rectTransform.anchoredPosition = new Vector2((float)(renderedCount % 3) * 120f,
                                                                      (float)Mathf.FloorToInt((float)renderedCount / 3f) * -120f);

                    if (slot.girlDefinition.id == Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id"))
                    {
                        selectedFileIconSlot = slot;
                    }
                    else
                    {
                        slot.button.Enable();
                    }

                    renderedCount++;
                    iconIndex++;
                }
                else
                {
                    slot.Populate(true);
                    slot.canvasGroup.blocksRaycasts = false;
                    slot.canvasGroup.alpha = 0f;
                }
            }

            foreach (var slot in appWardrobe.fileIconSlots.Skip(Constants.GirlsPerPage))
            {
                slot.Populate(true);
                slot.canvasGroup.blocksRaycasts = false;
                slot.canvasGroup.alpha = 0f;
            }

            appWardrobe.fileIconSlotsContainer.anchoredPosition = new Vector2(193, -283);

            appWardrobe.fileIconSlotsContainer.anchoredPosition += new Vector2(Mathf.Min(renderedCount - 1, 2) * -60f,
                                                                               Mathf.Max(Mathf.CeilToInt(renderedCount / 3f) - 1, 0) * 60f);

            if (Game.Persistence.playerFile.storyProgress >= 14)
            {
                appWardrobe.fileIconSlotsContainer.anchoredPosition += Vector2.up * 28f;
            }

            AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_selectedFileIconSlot").SetValue(appWardrobe, selectedFileIconSlot);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupWardrobeStyles() => SetupWardrobeStyles(AccessTools.Field(typeof(UiCellphone), "_currentApp")
                                                                                    .GetValue(Game.Session.gameCanvas.cellphone)
                                                                                    as UiCellphoneAppWardrobe,
                                                                         Game.Persistence
                                                                             .playerFile
                                                                             .GetPlayerFileGirl(Game.Data
                                                                                                    .Girls
                                                                                                    .Get(Game.Persistence
                                                                                                    .playerFile
                                                                                                    .GetFlagValue("wardrobe_girl_id"))));

        private static void SetupWardrobeStyles(UiCellphoneAppWardrobe appWardrobe, PlayerFileGirl playerFileGirl)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            SetupSelectList(appWardrobe.selectListOutfit, playerFileGirl, playerFileGirl.girlDefinition.outfits.Count);
            SetupSelectList(appWardrobe.selectListHairstyle, playerFileGirl, playerFileGirl.girlDefinition.hairstyles.Count);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupGirlsApp(UiCellphoneAppGirls appGirls, PlayerFileGirl[] playerfileGirls)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var girlIndex = _appPageIndex * Constants.GirlsPerPage;
            int renderedCount = 0;

            foreach (var slot in appGirls.girlSlots.Take(Constants.GirlsPerPage))
            {
                if (girlIndex < playerfileGirls.Length)
                {
                    slot.girlDefinition = playerfileGirls[girlIndex].girlDefinition;
                    slot.rectTransform.anchoredPosition = new Vector2(renderedCount % 6 * 172f,
                                                                      Mathf.FloorToInt(renderedCount / 6f) * -272f);
                    slot.Populate();

                    renderedCount++;
                    girlIndex++;
                }
                else
                {
                    slot.Clear();
                }
            }

            foreach (var slot in appGirls.girlSlots.Skip(Constants.GirlsPerPage))
            {
                slot.Clear();
            }

            appGirls.girlSlotsContainer.anchoredPosition = new Vector2(528, -284);

            appGirls.girlSlotsContainer.anchoredPosition += new Vector2((float)Mathf.Min(renderedCount - 1, 5) * -86f,
                                                                        (float)Mathf.Max(Mathf.CeilToInt((float)renderedCount / 6f) - 1, 0) * 136f);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupProfileApp(UiCellphoneAppProfile appProfile,
                                            List<GirlPairDefinition> pairs,
                                            QuestionDefinition[] questions,
                                            GirlDefinition girlDefinition)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            SetupEnumerable(_appPageIndex, pairs.Count, Constants.ProfilePairsPerPage, appProfile.pairSlots,
                (slot, index) =>
                {
                    slot.Populate(pairs[index]);
                    slot.canvasGroup.alpha = 1f;
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.button.Enable();
                },
                (slot) => slot.Populate(null, null));

            var _favQuestionDefField = AccessTools.Field(typeof(UiAppFavAnswer), "_favQuestionDefinition");
            var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDefinition);
            SetupEnumerable(_appPageIndex, questions.Length, Constants.QuestionsPerPage, appProfile.favAnswers,
                (slot, index) => { _favQuestionDefField.SetValue(slot, questions[index]); slot.Populate(playerFileGirl); },
                (slot) => slot.textLabel.text = string.Empty);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupSelectList(UiAppStyleSelectList list, PlayerFileGirl playerFileGirl, int total)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            AccessTools.Field(typeof(UiAppStyleSelectList), "_playerFileGirl").SetValue(list, playerFileGirl);
            UiAppSelectListItem purchaseListItem = null;
            UiAppSelectListItem selectedListItem = null;

            var styleIndex = _stylePageIndex * Constants.WarbrobeStylesPerPage;

            foreach (var item in list.listItems.Take(Constants.WarbrobeStylesPerPage))
            {
                if (styleIndex < total)
                {
                    string valueName = (list.alternative)
                        ? playerFileGirl.girlDefinition.outfits[styleIndex].outfitName
                        : playerFileGirl.girlDefinition.hairstyles[styleIndex].hairstyleName;

                    bool isUnlocked = (list.alternative)
                        ? playerFileGirl.IsOutfitUnlocked(styleIndex)
                        : playerFileGirl.IsHairstyleUnlocked(styleIndex);

                    bool hideIfLocked = false;

                    // locked items
                    if (!isUnlocked)
                    {
                        valueName = "???";
                        if (Game.Persistence.playerFile.storyProgress >= 14)
                        {
                            if (Game.Session.Hub.unlockStylesCode.Contains(styleIndex))
                            {
                                valueName = "Unlock With Code";
                            }
                            else if (Game.Session.Hub.unlockStylesBuy.Contains(styleIndex) && purchaseListItem == null)
                            {
                                valueName = "Purchase:";
                                purchaseListItem = item;
                            }
                        }
                        else if (Game.Session.Hub.unlockStylesCode.Contains(styleIndex) || Game.Session.Hub.unlockStylesBuy.Contains(styleIndex))
                        {
                            hideIfLocked = true;
                        }
                    }

                    item.Populate(isUnlocked, valueName, hideIfLocked);

                    // selected
                    if (styleIndex == (list.alternative ? playerFileGirl.outfitIndex : playerFileGirl.hairstyleIndex))
                    {
                        selectedListItem = item;
                        selectedListItem.Select(true);
                    }

                    // unselected
                    else
                    {
                        item.Select(false);
                    }

                    styleIndex++;
                }
                // out of range items
                else
                {
                    item.Populate(false, null, true);
                    item.Select(false);
                }
            }

            foreach (var item in list.listItems.Skip(Constants.WarbrobeStylesPerPage))
            {
                item.Populate(false, null, true);
                item.Select(false);
            }

            if (purchaseListItem != null)
            {
                var fruitCategoryInfo = Game.Session.Gift.GetFruitCategoryInfo((!list.alternative)
                    ? playerFileGirl.girlDefinition.leastFavoriteAffectionType
                    : playerFileGirl.girlDefinition.favoriteAffectionType);

                int price = (!list.alternative)
                    ? Game.Session.Hub.buyCostHairstyles[Game.Session.Hub.unlockStylesBuy.IndexOf(list.listItems.IndexOf(purchaseListItem))]
                    : Game.Session.Hub.buyCostOutfits[Game.Session.Hub.unlockStylesBuy.IndexOf(list.listItems.IndexOf(purchaseListItem))];

                if (Game.Persistence.playerFile.settingDifficulty == SettingDifficulty.EASY)
                {
                    price = Mathf.FloorToInt((float)price * 1.5f);
                }
                else if (Game.Persistence.playerFile.settingDifficulty == SettingDifficulty.HARD)
                {
                    price = Mathf.CeilToInt((float)price * 0.5f);
                }

                purchaseListItem.ShowCost(fruitCategoryInfo, price);

                if (Game.Persistence.playerFile.GetFruitCount(fruitCategoryInfo.affectionType) >= price)
                {
                    list.buyButton.Enable();
                }
                else
                {
                    list.buyButton.Disable();
                }
            }
            else
            {
                list.buyButton.Disable();
            }

            AccessTools.Field(typeof(UiAppStyleSelectList), "_purchaseListItem").SetValue(list, purchaseListItem);
            AccessTools.Field(typeof(UiAppStyleSelectList), "_selectedListItem").SetValue(list, selectedListItem);

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupEnumerable<T>(int startIndex,
                                       int total,
                                       int perPage,
                                       IEnumerable<T> enumerable,
                                       Action<T, int> setValue,
                                       Action<T> setNull)
        {
            var current = startIndex * perPage;

            foreach (var entry in enumerable.Take(perPage))
            {
                if (current < total)
                {
                    setValue.Invoke(entry, current);
                    current++;
                }
                else
                {
                    setNull.Invoke(entry);
                }
            }

            foreach (var entry in enumerable.Skip(perPage))
            {
                setNull.Invoke(entry);
            }
        }

        #endregion setup app
    }
}
