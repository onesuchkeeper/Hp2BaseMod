// Hp2Sample 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using UnityEngine.SceneManagement;

namespace Hp2BaseModTweaks
{
    /// <summary>
    /// Loads button prefabs into asset manager
    /// </summary>
    [HarmonyPatch(typeof(SceneManager), "LoadScene")]
    public static class InitUiCOntrollersPatch
    {
        //public static void Postfix(string sceneName, LoadSceneParameters parameters)
        //{
        //    ModInterface.Instance.LogTitle("Initing Hp2BaseModTweaks Ui Controlers");
        //    ModInterface.Instance.IncreaseLogIndent();
        //    ModInterface.Instance.LogLine(nameof(CellphoneController));
        //    CellphoneController.Instance.Init();
        //    ModInterface.Instance.LogLine(nameof(WindowController));
        //    WindowController.Instance.Init();
        //    ModInterface.Instance.LogLine("Done");
        //    ModInterface.Instance.DecreaseLogIndent();
        //}
    }
}
