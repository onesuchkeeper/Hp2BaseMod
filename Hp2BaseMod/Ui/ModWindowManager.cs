using HarmonyLib;
using Hp2BaseMod.Extension.IEnumerableExtension;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hp2BaseMod.Ui
{
    public class ModWindowManager
    {
        private static readonly FieldInfo _windowManager_currentWindow = AccessTools.Field(typeof(WindowManager), "_currentWindow");
        internal WindowManager WindowManager
        {
            get => _windowManager;
            set
            {
                _windowControllers.Clear();
                _windowManager = value;
                value.WindowShowEvent += OnWindowShow;
                value.WindowHideEvent += OnWindowHide;
            }
        }
        private WindowManager _windowManager;

        private readonly Stack<List<IUiController>> _windowControllers = new Stack<List<IUiController>>();

        private readonly Dictionary<Type, List<Func<UiWindow, IUiController>>> _windowControllerSources
            = new Dictionary<Type, List<Func<UiWindow, IUiController>>>();

        public void AddController(Type appType, Func<UiWindow, IUiController> makeController)
        {
            if (!_windowControllerSources.ContainsKey(appType))
            {
                _windowControllerSources.Add(appType, new List<Func<UiWindow, IUiController>>());
            }

            _windowControllerSources[appType].Add(makeController);
        }

        public void OnWindowShow()
        {
            var currentWindow = _windowManager_currentWindow.GetValue(Game.Manager.Windows) as UiWindow;
            ApplyControllers(currentWindow);
        }

        public void OnWindowHide()
        {
            _windowControllers.Pop();
        }

        private void ApplyControllers(UiWindow window)
        {
            var controllers = new List<IUiController>();
            var windowType = window?.GetType();

            if (window != null
                && _windowControllerSources.ContainsKey(windowType))
            {
                foreach (var entry in _windowControllerSources[windowType])
                {
                    controllers.Add(entry(window));
                }
            }

            _windowControllers.Push(controllers);
        }

        internal void PreRefresh()
        {
            if (_windowControllers.Count > 0)
            {
                foreach (var controller in _windowControllers.Peek().OrEmptyIfNull())
                {
                    controller.PreRefresh();
                }
            }
        }

        internal void PostRefresh()
        {
            if (_windowControllers.Count > 0)
            {
                foreach (var controller in _windowControllers.Peek().OrEmptyIfNull())
                {
                    controller.PostRefresh();
                }
            }
        }
    }
}
