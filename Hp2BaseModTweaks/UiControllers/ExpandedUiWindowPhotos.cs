using HarmonyLib;
using Hp2BaseMod.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    public class ExpandedUiWindowPhotos : IUiController
    {
        private static readonly FieldInfo _uiPhotoSlot_photoDefinition = AccessTools.Field(typeof(UiPhotoSlot), "_photoDefinition");
        private static readonly FieldInfo _uiWindowPhotos_earnedPhotos = AccessTools.Field(typeof(UiWindowPhotos), "_earnedPhotos");
        private static readonly int _photosPerPage = 29;
        private static Sprite _emptyPhotoSlot;

        private int _pageIndex;
        private int _pageMax;

        private readonly UiWindowPhotos _photosWindow;
        private readonly PhotoDefinition[] _photosArray;
        private readonly Hp2ButtonWrapper _previousPage;
        private readonly Hp2ButtonWrapper _nextPage;

        public ExpandedUiWindowPhotos(UiWindowPhotos photosWindow)
        {
            _photosWindow = photosWindow ?? throw new ArgumentNullException(nameof(photosWindow));

            _photosArray = (_uiWindowPhotos_earnedPhotos.GetValue(photosWindow) as List<PhotoDefinition>).ToArray();
            _pageMax = _photosArray.Length > 1 ? (_photosArray.Length - 1) / _photosPerPage : 0;
            _emptyPhotoSlot = AssetHolder.Instance.Sprites["ui_photo_album_slot"];

            if (_pageMax > 0)
            {
                var albumContainer = photosWindow.transform.Find("AlbumContainer");

                var cellphoneButtonPressedKlip = new AudioKlip()
                {
                    clip = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"],
                    volume = 1f
                };

                _previousPage = Hp2ButtonWrapper.MakeCellphoneButton("PreviousPage",
                                                         AssetHolder.Instance.Sprites["ui_photo_button_left"],
                                                         AssetHolder.Instance.Sprites["ui_photo_button_left"],
                                                         cellphoneButtonPressedKlip);

                _previousPage.GameObject.transform.SetParent(albumContainer, false);
                _previousPage.RectTransform.anchoredPosition = new Vector2(42, 1038);
                _previousPage.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
                    _pageIndex--;
                    PostRefresh();
                };

                _nextPage = Hp2ButtonWrapper.MakeCellphoneButton("NextPage",
                                                                 AssetHolder.Instance.Sprites["ui_photo_button_right"],
                                                                 AssetHolder.Instance.Sprites["ui_photo_button_right"],
                                                                 cellphoneButtonPressedKlip);

                _nextPage.GameObject.transform.SetParent(albumContainer, false);
                _nextPage.RectTransform.anchoredPosition = new Vector2(1878, 1038);
                _nextPage.ButtonBehavior.ButtonPressedEvent += (e) =>
                {
                    _pageIndex++;
                    PostRefresh();
                };
            }

            PostRefresh();
        }

        public void PreRefresh()
        {
            //noop
        }

        public void PostRefresh()
        {
            //photos
            var photoIndex = _pageIndex * _photosPerPage;

            foreach (var slot in _photosWindow.photoSlots.Take(_photosPerPage))
            {
                if (photoIndex < _photosArray.Length)
                {
                    _uiPhotoSlot_photoDefinition.SetValue(slot, _photosArray[photoIndex]);
                    slot.buttonBehavior.Enable();
                    slot.Refresh(1);
                    photoIndex++;
                }
                else
                {
                    slot.buttonBehavior.Disable();
                    slot.thumbnailImage.sprite = _emptyPhotoSlot;
                }
            }

            foreach (var slot in _photosWindow.photoSlots.Skip(_photosPerPage))
            {
                slot.buttonBehavior.Disable();
                slot.thumbnailImage.sprite = _emptyPhotoSlot;
            }

            //buttons
            if (_pageMax > 0)
            {
                if (_pageIndex <= 0)
                {
                    _pageIndex = 0;
                    _previousPage.ButtonBehavior.Disable();
                }
                else
                {
                    _previousPage.ButtonBehavior.Enable();
                }

                if (_pageIndex >= _pageMax)
                {
                    _pageIndex = _pageMax;
                    _nextPage.ButtonBehavior.Disable();
                }
                else
                {
                    _nextPage.ButtonBehavior.Enable();
                }
            }
        }
    }
}
