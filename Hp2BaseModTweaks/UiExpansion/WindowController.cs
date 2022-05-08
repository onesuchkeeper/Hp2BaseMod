// Hp2BaseMod 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    /// <summary>
    /// So, we have a window queue with nested windows, which means we need to have a way to control all of them
    /// </summary>
    public static class WindowController
    {
        private static readonly Vector2 _windowTopLeft = new Vector2(-908f, 496f);
        private static readonly Vector2 _windowTopRight = new Vector2(908f, 496f);

        private static PhotosWindowButton _leftButton;
        private static PhotosWindowButton _rightButton;
        private static Sprite _emptyPhotoSlot;

        private static int _windowPageIndex;
        private static Stack<UiWindow> shownWindows;

        private static bool _hasInit;

        public static void Init()
        {
            var parent = Game.Session?.gameCanvas?.cellphoneContainer?.parent;

            if (!_hasInit && parent != null)
            {
                _hasInit = true;

                var cellphoneButtonPressedSfx = AssetHolder.Instance.AudioClips["sfx_phone_app_button_pressed"];

                _leftButton = new PhotosWindowButton("Hp2BaseModTweaks.CellphoneController.leftButton",
                                                     AssetHolder.Instance.Sprites["ui_photo_button_left"],
                                                     cellphoneButtonPressedSfx);

                _leftButton.AddClickListener(() =>
                {
                    _windowPageIndex--;
                    SetupCurrentWindow();
                });

                _rightButton = new PhotosWindowButton("Hp2BaseModTweaks.CellphoneController.rightButton",
                                                      AssetHolder.Instance.Sprites["ui_photo_button_right"],
                                                      cellphoneButtonPressedSfx);

                _rightButton.AddClickListener(() =>
                {
                    _windowPageIndex++;
                    SetupCurrentWindow();
                });

                _emptyPhotoSlot = AssetHolder.Instance.Sprites["ui_photo_album_slot"];

                _leftButton.SetParent(parent);
                _rightButton.SetParent(parent);

                _leftButton.Hide();
                _rightButton.Hide();
            }
        }

        public static void OnWindowShow()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            Init();

            var currentWindow = AccessTools.Field(typeof(WindowManager), "_currentWindow")
                                           .GetValue(Game.Manager.Windows)
                                           as UiWindow;

            if (shownWindows.Count == 0
                || shownWindows.Peek() != currentWindow)
            {
                shownWindows.Push(AccessTools.Field(typeof(WindowManager), "_currentWindow")
                             .GetValue(Game.Manager.Windows)
                             as UiWindow);

                if (currentWindow is UiWindowPhotos windowPhotos)
                {
                    foreach (var slot in windowPhotos.photoSlots)
                    {
                        slot.PhotoSelectedEvent += WindowPhotos_OnPhotoSelected;
                    }

                    windowPhotos.closeButton.ButtonPressedEvent += WindowPhotos_OnCloseButtonPressed;
                }
            }

            SetupCurrentWindow(true);

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void OnWindowHide()
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            _windowPageIndex = 0;
            shownWindows.Pop();
            _leftButton.Hide();
            _rightButton.Hide();

            ModInterface.Instance.DecreaseLogIndent();
        }

        public static void OnWindowManagerConstructor()
        {
            _windowPageIndex = 0;
            shownWindows = new Stack<UiWindow>();
            _hasInit = false;
        }

        public static void WindowPhotos_OnPhotoSelected(UiPhotoSlot photoSlot)
        {
            _leftButton.Hide();
            _rightButton.Hide();
        }

        public static void WindowPhotos_OnCloseButtonPressed(ButtonBehavior buttonBehavior) => SetupCurrentWindow(true);

        #region setup window

        private static void SetupCurrentWindow(bool buttonsOnly = false)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            bool isPastTutorial = Game.Persistence.playerFile.storyProgress >= 7;

            if (isPastTutorial)
            {
                // position buttons
                _leftButton.SetLocalPostion(_windowTopLeft);
                _rightButton.SetLocalPostion(_windowTopRight);

                var pageMax = 0;

                if (shownWindows.Count != 0
                    && shownWindows.Peek() is UiWindowPhotos photosWindow)
                {
                    var photoViewMode = (int)AccessTools.Field(typeof(UiWindowPhotos),
                                                               "_photoViewMode")
                                                        .GetValue(photosWindow);

                    if (photoViewMode == 1)
                    {
                        var photosArray = (AccessTools.Field(typeof(UiWindowPhotos), "_earnedPhotos")
                                                      .GetValue(photosWindow) as List<PhotoDefinition>).ToArray();

                        pageMax = photosArray.Length > 1 ? (photosArray.Length - 1) / Constants.PhotosPerPage : 0;

                        _windowPageIndex = Utility.WrapIndex(_windowPageIndex, pageMax);

                        if (!buttonsOnly)
                        {
                            SetupPhotosWindow(photosWindow, photosArray);
                        }
                    }
                }

                if (pageMax > 0)
                {
                    _leftButton.Show();
                    _rightButton.Show();
                }
                else
                {
                    _leftButton.Hide();
                    _rightButton.Hide();
                }
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static void SetupPhotosWindow(UiWindowPhotos photosWindow, PhotoDefinition[] photosArray)
        {
            ModInterface.Instance.LogLine();
            ModInterface.Instance.IncreaseLogIndent();

            var photoIndex = _windowPageIndex * Constants.PhotosPerPage;
            var photoDefinitionAccess = AccessTools.Field(typeof(UiPhotoSlot), "_photoDefinition");
            foreach (var slot in photosWindow.photoSlots.Take(Constants.PhotosPerPage))
            {
                if (photoIndex < photosArray.Length)
                {
                    photoDefinitionAccess.SetValue(slot, photosArray[photoIndex]);
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

            foreach (var slot in photosWindow.photoSlots.Skip(Constants.PhotosPerPage))
            {
                slot.buttonBehavior.Disable();
                slot.thumbnailImage.sprite = _emptyPhotoSlot;
            }

            ModInterface.Instance.DecreaseLogIndent();
        }

        #endregion setup window
    }
}
