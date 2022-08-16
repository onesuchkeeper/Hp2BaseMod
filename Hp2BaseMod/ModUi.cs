using Hp2BaseMod.Ui;
using System;

namespace Hp2BaseMod
{
    public class ModUi
    {
        public bool GameInited { get; set; } = false;

        private readonly ModWindowManager _windowManager = new ModWindowManager();
        internal void SetWindowManager(WindowManager windowManager) => _windowManager.WindowManager = windowManager;
        internal void WindowPreRefresh() => _windowManager.PreRefresh();
        internal void WindowPostRefresh() => _windowManager.PostRefresh();
        public void AddUiWindowController(Type windowType, Func<UiWindow, IUiController> makeController) => _windowManager.AddController(windowType, makeController);

        private readonly ModCellphoneManager _titleCellphoneManager = new ModCellphoneManager();
        internal void SetTitleCellphone(UiCellphone cellphone) => _titleCellphoneManager.Cellphone = cellphone;
        public void AddTitleAppController(Type appType, Func<UiCellphoneApp, IUiController> makeController) => _titleCellphoneManager.AddController(appType, makeController);

        private readonly ModCellphoneManager _mainCellphoneManager = new ModCellphoneManager();
        internal void SetMainCellphone(UiCellphone cellphone) => _mainCellphoneManager.Cellphone = cellphone;
        public void AddMainAppController(Type appType, Func<UiCellphoneApp, IUiController> makeController) => _mainCellphoneManager.AddController(appType, makeController);

        private readonly ModCellphoneManager _headerCellphoneManager = new ModCellphoneManager();
        internal void SetHeaderCellphone(UiCellphone cellphone) => _headerCellphoneManager.Cellphone = cellphone;
        public void AddHeaderAppController(Type appType, Func<UiCellphoneApp, IUiController> makeController) => _headerCellphoneManager.AddController(appType, makeController);

        public ModCellphoneManager GetCellphoneManager(UiCellphone __instance)
        {
            if (_titleCellphoneManager.Cellphone == __instance)
            {
                return _titleCellphoneManager;
            }
            else if (_mainCellphoneManager.Cellphone == __instance)
            {
                return _mainCellphoneManager;
            }
            else if (_headerCellphoneManager.Cellphone == __instance)
            {
                return _headerCellphoneManager;
            }

            return null;
        }
    }
}
