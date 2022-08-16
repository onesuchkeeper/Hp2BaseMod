using HarmonyLib;
using Hp2BaseMod.Extension.IEnumerableExtension;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hp2BaseMod.Ui
{
    public class ModCellphoneManager
    {
        private static readonly FieldInfo _uiCellphone_currentApp = AccessTools.Field(typeof(UiCellphone), "_currentApp");
        private static readonly FieldInfo _uiCellphone_currentAppIndex = AccessTools.Field(typeof(UiCellphone), "_currentAppIndex");

        internal UiCellphone Cellphone
        {
            get => _cellphone;
            set
            {
                _appControllers.Clear();
                _cellphone = value;
            }
        }
        private UiCellphone _cellphone;

        private readonly List<IUiController> _appControllers = new List<IUiController>();

        private readonly Dictionary<Type, List<Func<UiCellphoneApp, IUiController>>> _appControllerSources 
            = new Dictionary<Type, List<Func<UiCellphoneApp, IUiController>>>();

        internal void PreRefresh()
        {
            foreach (var controller in _appControllers.OrEmptyIfNull())
            {
                controller.PreRefresh();
            }
        }

        internal void PostRefresh()
        {
            foreach (var controller in _appControllers.OrEmptyIfNull())
            {
                controller.PostRefresh();
            }
        }

        internal void CellphoneButtonPressed(int appIndex) => LoadApp(Mathf.Clamp(appIndex, 0, _cellphone.appPrefabs.Count - 1));

        internal void HardRefresh() => LoadApp((int)_uiCellphone_currentAppIndex.GetValue(_cellphone));

        internal void LoadOpenApp() => LoadApp(_cellphone.openAppIndex);

        internal void LoadClosedApp() => LoadApp(_cellphone.closedAppIndex);

        /// <summary>
        /// Adds a controller for the given app type. If the type of app is loaded, makeController will be called and
        /// the resulting <see cref="IUiController"/> will be held
        /// </summary>
        /// <param name="appType"></param>
        /// <param name="makeController"></param>
        public void AddController(Type appType, Func<UiCellphoneApp, IUiController> makeController)
        {
            if (!_appControllerSources.ContainsKey(appType))
            {
                _appControllerSources.Add(appType, new List<Func<UiCellphoneApp, IUiController>>());
            }

            _appControllerSources[appType].Add(makeController);
        }

        /// <summary>
        /// Loads the given app
        /// </summary>
        /// <param name="app"></param>
        /// <param name="index"></param>
        public void LoadApp(UiCellphoneApp app)
        {
            SetupApp(app, -1);
            ApplyControllers(app);
        }

        /// <summary>
        /// Loads the ap prefix the cellphone has at the given appIndex
        /// </summary>
        /// <param name="appIndex"></param>
        public void LoadApp(int appIndex)
        {
            _cellphone.LoadApp(appIndex);
            ApplyControllers(_uiCellphone_currentApp.GetValue(_cellphone) as UiCellphoneApp);
        }

        private void ApplyControllers(UiCellphoneApp app)
        {
            var appType = app?.GetType();

            if (app != null
                && _appControllerSources.ContainsKey(appType))
            {
                foreach (var entry in _appControllerSources[appType])
                {
                    _appControllers.Add(entry(app));
                }
            }
            else
            {
                _appControllers.Clear();
            }
        }

        /// <summary>
        /// Removes the current app
        /// </summary>
        private void CleanUpCurrentApp()
        {
            ModInterface.Log.LogLine();

            _appControllers.Clear();

            var currentApp = _uiCellphone_currentApp.GetValue(_cellphone) as UiCellphoneApp;

            if (currentApp != null)
            {
                if (_cellphone.phoneErrorMsg != null)
                {
                    _cellphone.phoneErrorMsg.ClearMessage();
                }
                if (_cellphone.cellphoneButtons.Count > 0 && currentApp.phoneButtonIndex >= 0)
                {
                    _cellphone.cellphoneButtons[currentApp.phoneButtonIndex].buttonBehavior.Enable();
                }

                UnityEngine.Object.Destroy(currentApp.gameObject);
            }
        }

        /// <summary>
        /// Sets up an app with the cellphone assuming it's at the given index
        /// </summary>
        /// <param name="app"></param>
        /// <param name="index"></param>
        private void SetupApp(UiCellphoneApp app, int index)
        {
            CleanUpCurrentApp();

            _uiCellphone_currentAppIndex.SetValue(_cellphone, index);

            if (index != _cellphone.closedAppIndex)
            {
                _cellphone.openAppIndex = index;
            }

            _uiCellphone_currentApp.SetValue(_cellphone, app);
            app.cellphone = _cellphone;
            app.rectTransform.SetParent((index != _cellphone.closedAppIndex) ? _cellphone.appContainerLow : _cellphone.appContainerHigh, false);
            app.rectTransform.anchoredPosition = Vector2.zero;

            if (_cellphone.cellphoneButtons.Count > 0
                && app.phoneButtonIndex >= 0)
            {
                _cellphone.cellphoneButtons[app.phoneButtonIndex].buttonBehavior.Disable();
            }
        }
    }
}
