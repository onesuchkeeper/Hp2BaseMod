using Hp2BaseMod.Ui;
using System;
using System.Linq;
using UnityEngine;

namespace Hp2BaseModTweaks.CellphoneApps
{
    internal class ExpandedUiCellphoneGirlsApp : IUiController
    {
        private static readonly int _girlsPerPage = 12;

        private Hp2ButtonWrapper _previousPage;
        private Hp2ButtonWrapper _nextPage;

        private int _currentPage = 0;
        private readonly int _pageMax;

        private readonly UiCellphoneAppGirls _girlsApp;
        private readonly PlayerFileGirl[] _playerfileGirls;

        public ExpandedUiCellphoneGirlsApp(UiCellphoneAppGirls girlsApp)
        {
            _girlsApp = girlsApp ?? throw new ArgumentNullException(nameof(girlsApp));

            _playerfileGirls = Game.Persistence.playerFile.girls.Where(x => x.playerMet).OrderBy(x => x.girlDefinition.id).ToArray();

            _pageMax = _playerfileGirls.Length > 1 ? (_playerfileGirls.Length - 1) / _girlsPerPage : 0;

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

                _previousPage.GameObject.transform.SetParent(girlsApp.transform, false);
                _previousPage.RectTransform.anchoredPosition = new Vector2(30, -30);
                _previousPage.ButtonBehavior.ButtonPressedEvent += (e) => {
                    _currentPage--;
                    PostRefresh();
                };

                _nextPage = Hp2ButtonWrapper.MakeCellphoneButton("NextPage",
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right"],
                                                                 AssetHolder.Instance.Sprites["ui_app_setting_arrow_right_over"],
                                                                 cellphoneButtonPressedKlip);

                _nextPage.GameObject.transform.SetParent(girlsApp.transform, false);
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
            //girls
            var girlIndex = _currentPage * _girlsPerPage;
            int renderedCount = 0;

            foreach (var slot in _girlsApp.girlSlots.Take(_girlsPerPage))
            {
                if (girlIndex < _playerfileGirls.Length)
                {
                    slot.girlDefinition = _playerfileGirls[girlIndex].girlDefinition;
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

            foreach (var slot in _girlsApp.girlSlots.Skip(_girlsPerPage))
            {
                slot.Clear();
            }

            _girlsApp.girlSlotsContainer.anchoredPosition = new Vector2(528, -284);

            _girlsApp.girlSlotsContainer.anchoredPosition += new Vector2((float)Mathf.Min(renderedCount - 1, 5) * -86f,
                                                                         (float)Mathf.Max(Mathf.CeilToInt((float)renderedCount / 6f) - 1, 0) * 136f);

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
