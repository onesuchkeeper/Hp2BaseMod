using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.Ui;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks.CellphoneApps
{
    internal class ExpandedUiCellphoneWardrobeApp : IUiController
    {
        private static readonly FieldInfo _uiCellphoneWardrobeApp_selectedFileIconSlot = AccessTools.Field(typeof(UiCellphoneAppWardrobe), "_selectedFileIconSlot");
        private static readonly int _girlsPerPage = 12;
        public static readonly int _warbrobeStylesPerPage = 10;

        private int _girlsPage;

        private readonly Hp2ButtonWrapper _girlsLeft;
        private readonly Hp2ButtonWrapper _girlsRight;

        private readonly UiCellphoneAppWardrobe _wardrobeApp;
        private readonly int _girlsPageMax;
        private readonly PlayerFileGirl[] _metGirls;
        private readonly UiAppFileIconSlot _dummyFileIconSlot;
        private readonly RectTransform _hairPanel;
        private readonly RectTransform _outfitPanel;


        public ExpandedUiCellphoneWardrobeApp(UiCellphoneAppWardrobe wardrobeApp)
        {
            _wardrobeApp = wardrobeApp ?? throw new ArgumentNullException(nameof(wardrobeApp));

            _metGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(x => x.girlDefinition.id).ToArray();
            _girlsPageMax = _metGirls.Length > 1 ? (_metGirls.Length - 1) / _girlsPerPage : 0;

            if (_girlsPageMax > 0)
            {
                // Buttons
                var cellphoneButtonPressedKlip = new AudioKlip()
                {
                    clip = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"],
                    volume = 1f
                };

                _girlsLeft = Hp2ButtonWrapper.MakeCellphoneButton("GirlsLeft",
                                                     AssetHolder.Instance.Sprites["ui_app_setting_arrow_left"],
                                                     AssetHolder.Instance.Sprites["ui_app_setting_arrow_left_over"],
                                                     cellphoneButtonPressedKlip);

                _girlsLeft.GameObject.transform.SetParent(_wardrobeApp.transform, false);
                _girlsLeft.RectTransform.anchoredPosition = new Vector2(30, -30);
                _girlsLeft.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
                    _girlsPage--;
                    PostRefresh();
                };

                _girlsRight = Hp2ButtonWrapper.MakeCellphoneButton("GirlsRight",
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"],
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"],
                                                                 cellphoneButtonPressedKlip);

                _girlsRight.GameObject.transform.SetParent(_wardrobeApp.transform, false);
                _girlsRight.RectTransform.anchoredPosition = new Vector2(356, -30);
                _girlsRight.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
                    _girlsPage++;
                    PostRefresh();
                };

                _dummyFileIconSlot = new UiAppFileIconSlot() { button = new ButtonBehavior() };

                //shift other stuff down a bit for the buttons to better fit
                _wardrobeApp.transform.Find("FileIconSlotsContainer").GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 32);
                _wardrobeApp.transform.Find("WearOnDatesCheckBox").GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 16);

                //go to correct page
                var wardrobeGirlId = Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id");
                var index = 0;

                foreach (var girl in _metGirls)
                {
                    if (girl.girlDefinition.id == wardrobeGirlId)
                    {
                        break;
                    }

                    index++;
                }

                _girlsPage = index / _girlsPerPage;
            }

            //outfits scroll
            ExpandList(_wardrobeApp.selectListHairstyle, out _hairPanel);
            ExpandList(_wardrobeApp.selectListOutfit, out _outfitPanel);

            PreRefresh();
            PostRefresh();
        }

        public void ExpandList(UiAppStyleSelectList list, out RectTransform panel_RectTransform)
        {
            //scroll
            var scroll_GO = new GameObject($"{list.name}Scroll");
            scroll_GO.transform.SetParent(list.transform, false);

            var scroll_RectTransform = scroll_GO.AddComponent<RectTransform>();
            scroll_RectTransform.anchorMin = new Vector2(0.5f, 1);
            scroll_RectTransform.anchorMax = new Vector2(0.5f, 1);
            scroll_RectTransform.pivot = new Vector2(0.5f, 1);
            scroll_RectTransform.anchoredPosition = list.background.anchoredPosition - new Vector2(0, 32);
            scroll_RectTransform.sizeDelta = list.background.sizeDelta - new Vector2(24, 42);

            scroll_GO.AddComponent<Image>();
            var scroll_Mask = scroll_GO.AddComponent<Mask>();
            scroll_Mask.showMaskGraphic = false;

            // panel
            var panel_GO = new GameObject($"{list.name}Panel");
            panel_GO.transform.SetParent(scroll_GO.transform, false);

            panel_RectTransform = panel_GO.AddComponent<RectTransform>();
            panel_RectTransform.anchorMin = new Vector2(0.5f, 1);
            panel_RectTransform.anchorMax = new Vector2(0.5f, 1);
            panel_RectTransform.pivot = new Vector2(0.5f, 1);
            panel_RectTransform.anchoredPosition = Vector2.zero;

            //container
            var itemContainer = list.transform.Find("ListItemContainer");
            itemContainer.transform.SetParent(panel_GO.transform, true);
            var itemContainer_RectTransform = itemContainer.GetComponent<RectTransform>();
            itemContainer_RectTransform.anchorMin = new Vector2(0.5f, 1);
            itemContainer_RectTransform.anchorMax = new Vector2(0.5f, 1);
            itemContainer_RectTransform.pivot = new Vector2(0.5f, 1);
            itemContainer_RectTransform.anchoredPosition = new Vector2(-112, -20);

            //scrollrect
            var scroll_ScrollRect = scroll_GO.AddComponent<ScrollRect>();
            scroll_ScrollRect.scrollSensitivity = 24;
            scroll_ScrollRect.horizontal = false;
            scroll_ScrollRect.viewport = scroll_RectTransform;
            scroll_ScrollRect.content = panel_RectTransform;
        }

        public void PreRefresh()
        {
            var wardrobeGirlId = Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id");
            var wardrobeGirlDef = Game.Data.Girls.Get(wardrobeGirlId);
            RefreshSelectList(_wardrobeApp.selectListHairstyle, wardrobeGirlDef.hairstyles.Count, _hairPanel);
            RefreshSelectList(_wardrobeApp.selectListOutfit, wardrobeGirlDef.outfits.Count, _outfitPanel);
        }

        public void PostRefresh()
        {
            // head slots
            UiAppFileIconSlot selectedFileIconSlot = null;
            var iconIndex = _girlsPage * _girlsPerPage;
            int renderedCount = 0;
            var wardrobeGirlId = Game.Persistence.playerFile.GetFlagValue("wardrobe_girl_id");
            ModInterface.Log.LogLine($"Refresh, page {_girlsPage}, index {iconIndex}");
            foreach (var slot in _wardrobeApp.fileIconSlots.Take(_girlsPerPage))
            {
                if (iconIndex < _metGirls.Length)
                {
                    slot.button.Disable();
                    slot.girlDefinition = _metGirls[iconIndex].girlDefinition;
                    slot.Populate(false);
                    slot.canvasGroup.blocksRaycasts = true;
                    slot.canvasGroup.alpha = 1f;
                    //slot.rectTransform.anchoredPosition = new Vector2((float)(renderedCount % 3) * 120f, (float)Mathf.FloorToInt((float)renderedCount / 3f) * -120f);

                    if (slot.girlDefinition.id == wardrobeGirlId)
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
                    //slot.girlDefinition = null;
                    slot.Populate(true);
                    slot.canvasGroup.blocksRaycasts = false;
                    slot.canvasGroup.alpha = 0f;
                }
            }

            foreach (var slot in _wardrobeApp.fileIconSlots.Skip(_girlsPerPage))
            {
                slot.Populate(true);
                slot.canvasGroup.blocksRaycasts = false;
                slot.canvasGroup.alpha = 0f;
            }

            // if the selected slot is on a different page, use the dummy slot.
            var wardrobeGirlDef = Game.Data.Girls.Get(wardrobeGirlId);
            if (selectedFileIconSlot == null)
            {
                _dummyFileIconSlot.girlDefinition = wardrobeGirlDef;
                _uiCellphoneWardrobeApp_selectedFileIconSlot.SetValue(_wardrobeApp, _dummyFileIconSlot);
            }
            else
            {
                _uiCellphoneWardrobeApp_selectedFileIconSlot.SetValue(_wardrobeApp, selectedFileIconSlot);
            }

            //buttons
            if (_girlsPageMax > 0)
            {
                if (_girlsPage <= 0)
                {
                    _girlsPage = 0;
                    _girlsLeft.ButtonBehavior.Disable();
                }
                else
                {
                    _girlsLeft.ButtonBehavior.Enable();
                }

                if (_girlsPage >= _girlsPageMax)
                {
                    _girlsPage = _girlsPageMax;
                    _girlsRight.ButtonBehavior.Disable();
                }
                else
                {
                    _girlsRight.ButtonBehavior.Enable();
                }
            }
        }

        private void RefreshSelectList(UiAppStyleSelectList list, int itemTotal, RectTransform panelRectTransform)
        {
            panelRectTransform.sizeDelta = new Vector2(383, 20 + (40 * itemTotal));

            var diff = itemTotal - list.listItems.Count;

            if (diff > 0)
            {
                // add missing
                var template = list.listItems[0];

                for (var i = diff; i > 0; i--)
                {
                    var newItem = UnityEngine.Object.Instantiate(template);
                    newItem.rectTransform.SetParent(template.transform.parent, false);

                    var test = newItem.GetComponent<RectTransform>();

                    list.listItems.Add(newItem);
                }

                // reposition
                var j = 0;
                foreach (var item in list.listItems)
                {
                    var rectTransform = item.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0, -40 * j++);
                }
            }
        }
    }
}
