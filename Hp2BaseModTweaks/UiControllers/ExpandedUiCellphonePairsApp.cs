using Hp2BaseMod.Ui;
using System;
using System.Linq;
using UnityEngine;

namespace Hp2BaseModTweaks.CellphoneApps
{
    internal class ExpandedUiCellphonePairsApp : IUiController
    {
        private static readonly int _pairsPerPage = 24;

        private Hp2ButtonWrapper _previousPage;
        private Hp2ButtonWrapper _nextPage;

        private int _currentPage = 0;
        private readonly int _pageMax;

        private readonly UiCellphoneAppPairs _pairsApp;
        private readonly GirlPairDefinition[] _metPairs;

        public ExpandedUiCellphonePairsApp(UiCellphoneAppPairs pairsApp)
        {
            _pairsApp = pairsApp ?? throw new ArgumentNullException(nameof(pairsApp));
            _metPairs = Game.Persistence.playerFile.metGirlPairs.ToArray();

            _pageMax = _metPairs.Length > 1 ? (_metPairs.Length - 1) / _pairsPerPage : 0;

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

                _previousPage.GameObject.transform.SetParent(pairsApp.transform, false);
                _previousPage.RectTransform.anchoredPosition = new Vector2(30, -30);
                _previousPage.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
                    _currentPage--;
                    PostRefresh();
                };

                _nextPage = Hp2ButtonWrapper.MakeCellphoneButton("NextPage",
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"],
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"],
                                                                 cellphoneButtonPressedKlip);

                _nextPage.GameObject.transform.SetParent(pairsApp.transform, false);
                _nextPage.RectTransform.anchoredPosition = new Vector2(1024, -30);
                _nextPage.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
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
            // pairs
            var renderCount = 0;

            var current = _currentPage * _pairsPerPage;

            foreach (var entry in _pairsApp.pairSlots.Take(_pairsPerPage))
            {
                if (current < _metPairs.Length)
                {
                    entry.Populate(_metPairs[current]);
                    entry.canvasGroup.alpha = 1f;
                    entry.canvasGroup.blocksRaycasts = true;
                    entry.button.Enable();
                    //entry.rectTransform.anchoredPosition = new Vector2((float)(renderCount % 4) * 256f,
                    //                                                  (float)Mathf.FloorToInt((float)renderCount / 4f) * -90f);
                    renderCount++;
                    current++;
                }
                else
                {
                    entry.Populate(null, null);
                }
            }

            foreach (var entry in _pairsApp.pairSlots.Skip(_pairsPerPage))
            {
                entry.Populate(null, null);
            }

            //_pairsApp.pairSlotsContainer.anchoredPosition = new Vector2(528, -284);

            //_pairsApp.pairSlotsContainer.anchoredPosition += new Vector2(Mathf.Min(renderCount - 1, 3) * -128f,
            //                                                             Mathf.Max(Mathf.CeilToInt((float)renderCount / 4f) - 1, 0) * 45f);

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
