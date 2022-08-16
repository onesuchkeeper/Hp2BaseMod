using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.Ui;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Hp2BaseModTweaks.CellphoneApps
{
    internal class ExpandedUiCellphoneFinderApp : IUiController
    {
        private static readonly int _finderLocationsPerPage = 8;
        private static readonly FieldInfo _playerFileFinderSlotAccess = AccessTools.Field(typeof(UiAppFinderSlot), "_playerFileFinderSlot");
        private Hp2ButtonWrapper _previousPage;
        private Hp2ButtonWrapper _nextPage;

        private int _currentPage = 0;
        private readonly int _pageMax;
        private readonly LocationDefinition[] _simLocations;

        private readonly UiCellphoneAppFinder _finderApp;

        public ExpandedUiCellphoneFinderApp(UiCellphoneAppFinder finderApp)
        {
            _finderApp = finderApp ?? throw new ArgumentNullException(nameof(finderApp));

            _simLocations = Game.Data.Locations.GetAll().Where(x => x.locationType == LocationType.SIM).ToArray();
            _pageMax = _simLocations.Length > 1 ? (_simLocations.Length - 1) / _finderLocationsPerPage : 0;

            // no need for extra ui
            if (_pageMax != 0)
            {
                var cellphoneButtonPressedKlip = new AudioKlip()
                {
                    clip = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"],
                    volume = 1f
                };

                _previousPage = Hp2ButtonWrapper.MakeCellphoneButton("PreviousPage",
                                                                     AssetHolder.Instance.Sprites["ui_app_setting_arrow_left"],
                                                                     AssetHolder.Instance.Sprites["ui_app_setting_arrow_left_over"],
                                                                     cellphoneButtonPressedKlip);

                _previousPage.GameObject.transform.SetParent(finderApp.transform, false);
                _previousPage.RectTransform.anchoredPosition = new Vector2(30, -30);
                _previousPage.ButtonBehavior.ButtonPressedEvent += (e) => {
                    _currentPage--;
                    PostRefresh();
                };

                _nextPage = Hp2ButtonWrapper.MakeCellphoneButton("NextPage",
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"],
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"],
                                                                 cellphoneButtonPressedKlip);

                _nextPage.GameObject.transform.SetParent(finderApp.transform, false);
                _nextPage.RectTransform.anchoredPosition = new Vector2(1024, -30);
                _nextPage.ButtonBehavior.ButtonPressedEvent += (e) => {
                    _currentPage++;
                    PostRefresh();
                };
            }

            PostRefresh();
        }

        public void PreRefresh()
        {

        }

        public void PostRefresh()
        {
            //location slots
            var locationIndex = _currentPage * _finderLocationsPerPage;
            
            foreach (var slot in _finderApp.finderSlots.Take(_finderLocationsPerPage))
            {
                if (locationIndex < _simLocations.Length)
                {
                    slot.canvasGroup.alpha = 1f;
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.locationDefinition = _simLocations[locationIndex];
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

                            if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.SIM }))
                            {
                                isUnavailable = !Game.Manager.Windows.IsWindowActive(Game.Session.Location.actionBubblesWindow, true, false);
                            }
                            else if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.DATE }))
                            {
                                isUnavailable = !Game.Session.Puzzle.puzzleStatus.gameOver;
                            }
                            else if (Game.Session.Location.AtLocationType(new LocationType[] { LocationType.HUB }))
                            {
                                isUnavailable = !(((Game.Session.Hub.hubStepType == HubStepType.ROOT
                                            && Game.Manager.Windows.IsWindowActive(Game.Session.Dialog.dialogOptionsWindow, true, false))
                                          || Game.Persistence.playerFile.GetFlagValue(Game.Session.Hub.firstLocationFlag) < 0));
                            }
                            else
                            {
                                isUnavailable = true;
                            }
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

                    _playerFileFinderSlotAccess.SetValue(slot, playerFileFinderSlot);

                    locationIndex++;
                }
                else
                {
                    slot.canvasGroup.alpha = 0f;
                    slot.canvasGroup.blocksRaycasts = false;
                }
            }

            foreach (var slot in _finderApp.finderSlots.Skip(_finderLocationsPerPage))
            {
                slot.canvasGroup.alpha = 0f;
                slot.canvasGroup.blocksRaycasts = false;
            }

            if (_pageMax == 0)
            {
                return;
            }

            //buttons
            if (_currentPage <= 0)
            {
                _currentPage = 0;
                _previousPage.ButtonBehavior.Disable();
            }
            else
            {
                _previousPage.ButtonBehavior.Enable();
            }

            if (_currentPage >= _pageMax)
            {
                _currentPage = _pageMax;
                _nextPage.ButtonBehavior.Disable();
            }
            else
            {
                _nextPage.ButtonBehavior.Enable();
            }
        }
    }
}
