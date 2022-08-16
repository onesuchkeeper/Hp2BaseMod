using Hp2BaseMod;
using Hp2BaseMod.Extension.IEnumerableExtension;
using Hp2BaseMod.Ui;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks.CellphoneApps
{
    internal class ExpandedUiCellphoneCreditsApp : IUiController
    {
        private static readonly string _creditsBackgroundPath = "mods/Hp2BaseModTweaks/Images/ui_app_credits_modded_background.png";

        public Hp2ButtonWrapper ModCycleLeft;
        public Hp2ButtonWrapper ModCycleRight;
        public Image ModLogo;
        public GameObject ContributorsPanel;
        public RectTransform ContributorsPanel_RectTransform;

        private int _creditsIndex;
        
        private List<Hp2ButtonWrapper> _contributors = new List<Hp2ButtonWrapper>();

        public ExpandedUiCellphoneCreditsApp(UiCellphoneAppCredits creditsApp)
        {
            if (File.Exists(_creditsBackgroundPath))
            {
                var backgroundImage = creditsApp.transform.Find("Background").GetComponent<Image>();
                backgroundImage.sprite = TextureUtility.SpriteFromPath(_creditsBackgroundPath);
                ModInterface.Log.LogLine(backgroundImage.rectTransform.anchoredPosition.ToString());
                backgroundImage.SetNativeSize();
                backgroundImage.rectTransform.anchoredPosition = new Vector2(16,-18);
            }
            else
            {
                ModInterface.Log.LogError($"{_creditsBackgroundPath} not found");
            }

            var modLogoGO = new GameObject("ModLogo");
            modLogoGO.AddComponent<CanvasRenderer>();
            ModLogo = modLogoGO.AddComponent<Image>();
            modLogoGO.transform.SetParent(creditsApp.transform, false);
            ModLogo.rectTransform.anchoredPosition = new Vector2(528, -60);

            var cellphoneButtonPressedKlip = new AudioKlip()
            {
                clip = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"],
                volume = 1f
            };

            ModCycleLeft = Hp2ButtonWrapper.MakeCellphoneButton("ModCycleLeft",
                                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_left"],
                                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_left_over"],
                                                                                 cellphoneButtonPressedKlip);
            ModCycleLeft.GameObject.transform.SetParent(creditsApp.transform, false);
            ModCycleLeft.RectTransform.anchoredPosition = new Vector2(528-134, -60);
            ModCycleLeft.ButtonBehavior.ButtonPressedEvent += (x) => {
                _creditsIndex--;
                PostRefresh();
            };

            ModCycleRight = Hp2ButtonWrapper.MakeCellphoneButton("ModCycleRight",
                                                                                  AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"],
                                                                                  AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"],
                                                                                  cellphoneButtonPressedKlip);
            ModCycleRight.GameObject.transform.SetParent(creditsApp.transform, false);
            ModCycleRight.RectTransform.anchoredPosition = new Vector2(528 + 134, -60);
            ModCycleRight.ButtonBehavior.ButtonPressedEvent += (x) => {
                _creditsIndex++;
                PostRefresh();
            };

            // contributors scroll
            var contributorsScroll_GO = new GameObject("ContributorsScroll");
            contributorsScroll_GO.transform.SetParent(creditsApp.transform, false);
            var contributorsScroll_RectTransform = contributorsScroll_GO.AddComponent<RectTransform>();
            contributorsScroll_RectTransform.anchorMin = new Vector2(0.5f, 1);
            contributorsScroll_RectTransform.anchorMax = new Vector2(0.5f, 1);
            contributorsScroll_RectTransform.anchoredPosition = new Vector2(528, -318);
            contributorsScroll_RectTransform.sizeDelta = new Vector2(332, 428);

            var contributorsScroll_ScrollRect = contributorsScroll_GO.AddComponent<ScrollRect>();
            contributorsScroll_ScrollRect.scrollSensitivity = 24;
            contributorsScroll_ScrollRect.horizontal = false;
            contributorsScroll_ScrollRect.viewport = contributorsScroll_RectTransform;

            contributorsScroll_GO.AddComponent<Image>();
            var contributorsScroll_Mask = contributorsScroll_GO.AddComponent<Mask>();
            contributorsScroll_Mask.showMaskGraphic = false;

            // contributors panel
            ContributorsPanel = new GameObject("ContributorsPanel");
            ContributorsPanel.transform.SetParent(contributorsScroll_GO.transform, false);

            ContributorsPanel_RectTransform = ContributorsPanel.AddComponent<RectTransform>();
            ContributorsPanel_RectTransform.anchorMin = new Vector2(0.5f, 1);
            ContributorsPanel_RectTransform.anchorMax = new Vector2(0.5f, 1);
            ContributorsPanel_RectTransform.pivot = new Vector2(0.5f, 1);
            contributorsScroll_ScrollRect.content = ContributorsPanel_RectTransform;

            var contributorsPanel_VLG = ContributorsPanel.AddComponent<VerticalLayoutGroup>();
            contributorsPanel_VLG.spacing = 10;
            contributorsPanel_VLG.childForceExpandWidth = false;
            contributorsPanel_VLG.childForceExpandHeight = false;

            PostRefresh();
        }

        public void PreRefresh()
        {
            //noop
        }

		public void PostRefresh()
		{
            if (_creditsIndex <= 0)
            {
                _creditsIndex = 0;
                ModCycleLeft.ButtonBehavior.Disable();
            }
            else
            {
                ModCycleLeft.ButtonBehavior.Enable();
            }

            if (_creditsIndex >= Common.Credits.Count - 1)
            {
                _creditsIndex = Common.Credits.Count - 1;
                ModCycleRight.ButtonBehavior.Disable();
            }
            else
            {
                ModCycleRight.ButtonBehavior.Enable();
            }

            if (Common.Credits.Count != 0)
            {
                var creditsConfig = Common.Credits[_creditsIndex];

                ModLogo.sprite = TextureUtility.SpriteFromPath(creditsConfig.modImagePath);
                ModLogo.SetNativeSize();

                // remove contributors
                foreach (var contributor in _contributors)
                {
                    Object.Destroy(contributor.GameObject);
                }
                _contributors.Clear();

                var cellphoneButtonPressedKlip = new AudioKlip()
                {
                    clip = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"],
                    volume = 1f
                };

                // add new contributors
                ContributorsPanel_RectTransform.sizeDelta = new Vector2(319, (creditsConfig.CreditsEntries.Count * 135) - 10);

                int i = 0;
                foreach (var contributorConfig in creditsConfig.CreditsEntries.OrEmptyIfNull())
                {
                    var newContributorButton = Hp2ButtonWrapper.MakeCellphoneButton($"Contributor {i++}",
                                                                TextureUtility.SpriteFromPath(contributorConfig.creditButtonImagePath),
                                                                TextureUtility.SpriteFromPath(contributorConfig.creditButtonImageOverPath),
                                                                cellphoneButtonPressedKlip);

                    newContributorButton.GameObject.transform.SetParent(ContributorsPanel.transform, false);

                    _contributors.Add(newContributorButton);

                    newContributorButton.ButtonBehavior.ButtonPressedEvent += (e) => System.Diagnostics.Process.Start(contributorConfig.redirectLink);
                }
            }
        }
    }
}
