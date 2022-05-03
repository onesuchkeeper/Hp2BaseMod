// Hp2BaseMod 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
    public static class CellphoneController
    {
        private static GameObject _leftButton;
        private static GameObject _rightButton;

        private static RectTransform _leftButtonTransform;
        private static RectTransform _rightButtonTransform;

        private static GameObject _leftStyleButton;
        private static GameObject _rightStyleButton;

        private static RectTransform _leftStyleButtonTransform;
        private static RectTransform _rightStyleButtonTransform;

        private static int _appPageIndex = 0;
        private static int _stylePageIndex = 0;
        private static int _currentAppIndex = -1;

        private static AudioKlip _pressedSfx;

        public static void OnCellphoneOpened()
        {
            var cellphone = Game.Session.gameCanvas.cellphone;
            _currentAppIndex = cellphone.openAppIndex;

            // grab sound clips
            var cellphoneButton = cellphone.cellphoneButtons.FirstOrDefault();
            if (cellphoneButton != null)
            {
                _pressedSfx = cellphoneButton.buttonBehavior.pressedSfx;
            }

            // create buttons if nessisary
            if (_leftButton == null)
            {
                _leftButton = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], cellphone.appContainerLow);
                _leftButtonTransform = _leftButton.GetComponent<RectTransform>();
                var button = _leftButton.GetComponent<Button>();
                button.onClick.AddListener(OnLeftButtonClicked);
            }

            if (_rightButton == null)
            {
                _rightButton = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], cellphone.appContainerLow);
                _rightButtonTransform = _rightButton.GetComponent<RectTransform>();

                _rightButton.GetComponent<Button>().onClick.AddListener(OnRightButtonClicked);
            }

            // wardrobe needs extra setup
            if (cellphone.openAppIndex == 8)
            {
                if (_leftStyleButton == null)
                {
                    _leftStyleButton = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[2], cellphone.appContainerLow);
                    _leftStyleButtonTransform = _leftStyleButton.GetComponent<RectTransform>();
                    _leftStyleButtonTransform.anchoredPosition = new Vector2(420f, -30f);

                    _leftStyleButton.GetComponent<Button>().onClick.AddListener(OnLeftStyleButtonClicked);
                }

                if (_rightStyleButton == null)
                {
                    _rightStyleButton = (GameObject)UnityEngine.Object.Instantiate(AssetHolder.Instance.Assets[3], cellphone.appContainerLow);
                    _rightStyleButtonTransform = _rightStyleButton.GetComponent<RectTransform>();
                    _rightStyleButtonTransform.anchoredPosition = new Vector2(1026f, -30f);

                    _rightStyleButton.GetComponent<Button>().onClick.AddListener(OnRightStyleButtonClicked);
                }

                // set page to to ones with the selected items
                var wardrobeGirl = Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id"));

                _currentAppIndex = Game.Data.Girls.GetAll().IndexOf(wardrobeGirl) / Constants.GirlsPerPage;

                _stylePageIndex = Game.Persistence.playerFile.GetPlayerFileGirl(wardrobeGirl).outfitIndex / Constants.WarbrobeStylesPerPage;

                var appWardrobe = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppWardrobe;
                var selectedGirlDef = (AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_selectedFileIconSlot").GetValue(appWardrobe) as UiAppFileIconSlot).girlDefinition;
                PlayerFileGirl playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(selectedGirlDef);
                appWardrobe.wearOnDatesCheckBox.Populate(playerFileGirl.stylesOnDates);
            }
            else
            {
                _currentAppIndex = 0;
                _stylePageIndex = 0;
            }

            if (AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) is UiCellphoneAppNew)
            {
                ModInterface.Instance.LogLine("8======================D I found it look here!");
            }

            OnCellphoneAppLoaded(cellphone);
        }

        public static void OnCellphoneClose()
        {
            // the game does some weird stuff with freezing buttons and ui,
            // safer for future to just to get rid of my extra Ui
            UnityEngine.Object.Destroy(_leftButton);
            _leftButton = null;
            UnityEngine.Object.Destroy(_rightButton);
            _rightButton = null;
            _appPageIndex = 0;

            UnityEngine.Object.Destroy(_leftStyleButton);
            _leftStyleButton = null;
            UnityEngine.Object.Destroy(_rightStyleButton);
            _rightStyleButton = null;
            _stylePageIndex = 0;
        }

        public static void OnCellphoneAppLoaded(UiCellphone cellphone)
        {
            if (_currentAppIndex != cellphone.openAppIndex)
            {
                _currentAppIndex = cellphone.openAppIndex;
                _appPageIndex = 0;
            }

            SetupCurrentApp();
        }

        public static void OnListItemSelected(UiAppStyleSelectList list, UiAppSelectListItem listItem)
        {
            var appWardrobe = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppWardrobe;
            var selectedListItemAccess = AccessTools.Field(typeof(UiAppStyleSelectList), "_selectedListItem");
            var selectedListItem = selectedListItemAccess.GetValue(list) as UiAppSelectListItem;
            var playerFileGirl = AccessTools.Field(typeof(UiAppStyleSelectList), "_playerFileGirl").GetValue(list) as PlayerFileGirl;

            var listIndexOffset = _stylePageIndex * Constants.WarbrobeStylesPerPage;

            if (!list.alternative)
            {
                playerFileGirl.hairstyleIndex = list.listItems.IndexOf(selectedListItem) + listIndexOffset;
            }
            else
            {
                playerFileGirl.outfitIndex = list.listItems.IndexOf(selectedListItem) + listIndexOffset;
            }

            AccessTools.Method(typeof(UiCellphoneAppWardrobe), "OnListItemSelected").Invoke(appWardrobe, new object[] { list, false });
        }

        public static void OnFileIconSlotSelected()
        {
            _stylePageIndex = Game.Persistence
                                  .playerFile
                                  .GetPlayerFileGirl(Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id")))
                                  .outfitIndex 
                              / Constants.WarbrobeStylesPerPage;

            var appWardrobe = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppWardrobe;
            var selectedGirlDef = (AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_selectedFileIconSlot").GetValue(appWardrobe) as UiAppFileIconSlot).girlDefinition;
            PlayerFileGirl playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(selectedGirlDef);
            appWardrobe.wearOnDatesCheckBox.Populate(playerFileGirl.stylesOnDates);

            SetupWardrobeStyles();
        }

        private static void CapPageIndex(int pageMax)
        {
            if (_appPageIndex < 0)
            {
                _appPageIndex = pageMax;
            }
            else if (_appPageIndex > pageMax)
            {
                _appPageIndex = 0;
            }
        }

        #region buttons control

        private static void OnLeftButtonClicked()
        {
            _appPageIndex--;
            SetupCurrentApp();
            Game.Manager.Audio.Play(AudioCategory.SOUND, _pressedSfx);
        }

        private static void OnRightButtonClicked()
        {
            _appPageIndex++;
            SetupCurrentApp();
            Game.Manager.Audio.Play(AudioCategory.SOUND, _pressedSfx);
        }

        private static void OnLeftStyleButtonClicked()
        {
            _stylePageIndex--;
            SetupWardrobeStyles();
            Game.Manager.Audio.Play(AudioCategory.SOUND, _pressedSfx);
        }

        private static void OnRightStyleButtonClicked()
        {
            _stylePageIndex++;
            SetupWardrobeStyles();
            Game.Manager.Audio.Play(AudioCategory.SOUND, _pressedSfx);
        }

        private static void ShowButtons()
        {
            _leftButton?.SetActive(true);
            _rightButton?.SetActive(true);
            _leftButtonTransform?.SetAsLastSibling();
            _rightButtonTransform?.SetAsLastSibling();
        }

        private static void ShowStyleButtons()
        {
            _leftStyleButton?.SetActive(true);
            _rightStyleButton?.SetActive(true);
            _leftStyleButtonTransform?.SetAsLastSibling();
            _rightStyleButtonTransform?.SetAsLastSibling();
        }

        private static void HideButtons()
        {
            _leftButton?.SetActive(false);
            _rightButton?.SetActive(false);
        }

        private static void HideStyleButtons()
        {
            _leftStyleButton?.SetActive(false);
            _rightStyleButton?.SetActive(false);
        }

        #endregion buttons control

        #region setup app

        private static void SetupCurrentApp()
        {
            switch (_currentAppIndex)
            {
                case 1: //finder
                    SetupFinderApp();
                    break;
                case 2: //girls
                    SetupGirlsApp();
                    break;
                case 6: //pairs
                    SetupPairsApp();
                    break;
                case 7: //profiles
                    SetupProfileApp();
                    break;
                case 8: //wardrobe
                    SetupWardrobeApp();
                    break;
                case 3: //stats
                case 4: //gifts
                case 5: //store
                default:
                    HideButtons();
                    break;
            }

            switch (_currentAppIndex)
            {
                case 1: //finder
                case 2: //girls
                case 6: //pairs
                case 7: //profiles
                    _leftButtonTransform.anchoredPosition = new Vector2(30f, -30f);
                    _rightButtonTransform.anchoredPosition = new Vector2(1026f, -30f);
                    break;
                case 8: //wardrobe
                    _leftButtonTransform.anchoredPosition = new Vector2(30f, -30f);
                    _rightButtonTransform.anchoredPosition = new Vector2(356f, -30f);
                    break;
                case 3: //stats
                case 4: //gifts
                case 5: //store
                default:
                    HideButtons();
                    break;
            }
        }

        private static void SetupFinderApp()
        {
            ModInterface.Instance.LogLine();

            var appFinder = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppFinder;
            var simLocationsArray = Game.Data.Locations.GetAllByLocationType(LocationType.SIM).ToArray();
            var pageMax = simLocationsArray.Length > 1 ? (simLocationsArray.Length - 1) / Constants.FinderLocationsPerPage : 0;

            if (pageMax > 0)
            {
                CapPageIndex(pageMax);

                if (_appPageIndex != 0)
                {
                    // set backend data
                    SetupEnumerable(_appPageIndex, simLocationsArray.Length, Constants.FinderLocationsPerPage, appFinder.finderSlots,
                        (slot, index) =>
                        {
                            slot.locationDefinition = simLocationsArray[index];
                            slot.Populate(false);
                            slot.canvasGroup.alpha = 1f;
                            slot.canvasGroup.blocksRaycasts = true;
                        },
                        (slot) =>
                        {
                            slot.canvasGroup.alpha = 0f;
                            slot.canvasGroup.blocksRaycasts = false;
                        });
                }

                ShowButtons();
            }
            else
            {
                HideButtons();
            }
        }

        private static void SetupPairsApp()
        {
            ModInterface.Instance.LogLine();

            var appPairs = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppPairs;
            var pairsArray = Game.Persistence.playerFile.metGirlPairs.ToArray();
            var pageMax = pairsArray.Length > 1 ? (pairsArray.Length - 1) / Constants.PairsPerPage : 0;

            if (pageMax > 0)
            {
                CapPageIndex(pageMax);

                if (_appPageIndex != 0)
                {
                    // set backend data
                    var renderCount = 0;

                    SetupEnumerable(_appPageIndex, pairsArray.Length, Constants.PairsPerPage, appPairs.pairSlots,
                        (slot, index) =>
                        {
                            slot.Populate(pairsArray[index]);
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
                }

                ShowButtons();
            }
            else
            {
                HideButtons();
            }
        }

        private static void SetupWardrobeApp()
        {
            ModInterface.Instance.LogLine();

            var appWardrobe = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppWardrobe;
            var girlsArray = Game.Persistence.playerFile.girls.Where(x => x.playerMet).ToArray();
            var pageMax = girlsArray.Length > 1 ? (girlsArray.Length - 1) / Constants.GirlsPerPage : 0;

            if (pageMax > 0)
            {
                CapPageIndex(pageMax);

                if (_appPageIndex > 0)
                {
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
                }

                ShowButtons();
            }
            else
            {
                HideButtons();
            }

            SetupWardrobeStyles();
        }

        private static void SetupWardrobeStyles()
        {
            ModInterface.Instance.LogLine();

            var appWardrobe = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppWardrobe;
            var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(Game.Data.Girls.Get(Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id")));

            var outfitsCount = playerFileGirl.girlDefinition.outfits.Count;
            var outfitsMax = outfitsCount > 1 ? (outfitsCount - 1) / Constants.WarbrobeStylesPerPage : 0;

            var hairstylesCount = playerFileGirl.girlDefinition.hairstyles.Count;
            var hairstylesMax = hairstylesCount > 1 ? (hairstylesCount - 1) / Constants.WarbrobeStylesPerPage : 0;

            var pageMax = Math.Max(outfitsMax, hairstylesMax);

            if (pageMax > 0)
            {
                if (_stylePageIndex < 0)
                {
                    _stylePageIndex = pageMax;
                }
                else if (_stylePageIndex > pageMax)
                {
                    _stylePageIndex = 0;
                }

                SetupSelectList(appWardrobe.selectListOutfit, playerFileGirl, outfitsCount);
                SetupSelectList(appWardrobe.selectListHairstyle, playerFileGirl, hairstylesCount);

                ShowStyleButtons();
            }
            else
            {
                HideStyleButtons();
            }
        }

        private static void SetupSelectList(UiAppStyleSelectList list, PlayerFileGirl playerFileGirl, int total)
        {
            ModInterface.Instance.LogLine();

            UiAppSelectListItem purchaseListItem = null;
            UiAppSelectListItem selectedListItem = null;

            var styleIndex = _stylePageIndex * Constants.WarbrobeStylesPerPage;

            foreach (var item in list.listItems.Take(Constants.WarbrobeStylesPerPage))
            {
                if (styleIndex < total)
                {
                    string valueName = (list.alternative)
                        ? playerFileGirl.girlDefinition.outfits[styleIndex].outfitName
                        :playerFileGirl.girlDefinition.hairstyles[styleIndex].hairstyleName;

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
        }

        private static void SetupGirlsApp()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var appGirls = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(Game.Session.gameCanvas.cellphone) as UiCellphoneAppGirls;
            var playerfileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).ToArray();
            var pageMax = playerfileGirls.Length > 1 ? (playerfileGirls.Length - 1) / Constants.GirlsPerPage : 0;

            if (pageMax > 0)
            {
                CapPageIndex(pageMax);

                if (_appPageIndex > 0)
                {
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

                    ModInterface.Instance.LogLine($"RenderedCount: {renderedCount}");

                    appGirls.girlSlotsContainer.anchoredPosition = new Vector2(528, -284);

                    appGirls.girlSlotsContainer.anchoredPosition += new Vector2((float)Mathf.Min(renderedCount - 1, 5) * -86f,
                                                                                (float)Mathf.Max(Mathf.CeilToInt((float)renderedCount / 6f) - 1, 0) * 136f);
                }

                ShowButtons();
            }
            else
            {
                HideButtons();
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupProfileApp()
        {
            ModInterface.Instance.LogLine();

            var cellphone = Game.Session.gameCanvas.cellphone;
            var profileGirl = Game.Data.Girls.Get(cellphone.GetCellFlag("profile_girl_id"));
            var appProfile = AccessTools.Field(typeof(UiCellphone), "_currentApp").GetValue(cellphone) as UiCellphoneAppProfile;

            var pairs = new List<GirlPairDefinition>();
            foreach (var pair in Game.Persistence.playerFile.metGirlPairs)
            {
                if (pair.HasGirlDef(profileGirl))
                {
                    pairs.Add(pair);
                }
            }
            var pairsArray = pairs.ToArray();
            var pairsCount = pairsArray.Length;

            var questionsArray = Game.Data.Questions.GetAll().ToArray();
            var questionsCount = questionsArray.Length;

            var pairsMax = pairsCount > 1 ? (pairsCount - 1) / Constants.ProfilePairsPerPage : 0;
            var questionsMax = questionsCount > 1 ? (questionsCount - 1) / Constants.QuestionsPerPage : 0;

            var pageMax = Mathf.Max(pairsMax, questionsMax);

            if (pageMax > 0)
            {
                CapPageIndex(pageMax);

                if (_appPageIndex > 0)
                {
                    // set backend data
                    SetupEnumerable(_appPageIndex, pairsCount, Constants.ProfilePairsPerPage, appProfile.pairSlots,
                        (slot, index) =>
                        {
                            slot.Populate(pairsArray[index]);
                            slot.canvasGroup.alpha = 1f;
                            slot.canvasGroup.blocksRaycasts = true;
                            slot.button.Enable();
                        },
                        (slot) => slot.Populate(null, null));

                    var _favQuestionDefField = AccessTools.Field(typeof(UiAppFavAnswer), "_favQuestionDefinition");
                    var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(profileGirl);
                    SetupEnumerable(_appPageIndex, questionsCount, Constants.QuestionsPerPage, appProfile.favAnswers,
                        (slot, index) => { _favQuestionDefField.SetValue(slot, questionsArray[index]); slot.Populate(playerFileGirl); },
                        (slot) => slot.textLabel.text = string.Empty);
                }

                ShowButtons();
            }
            else
            {
                HideButtons();
            }
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
