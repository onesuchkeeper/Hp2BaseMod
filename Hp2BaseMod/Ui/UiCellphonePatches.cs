using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hp2BaseMod.Ui
{
    [HarmonyPatch(typeof(UiGameCanvas))]
    public static class UiGameCanvasPatch
    {
        /// <summary>
        /// Not a great injection point, But I couldn't find anything better
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        public static void UpdatePrefix(UiGameCanvas __instance)
        {
            if (!ModInterface.Ui.GameInited)
            {
                ModInterface.Ui.GameInited = true;

                ModInterface.Ui.SetMainCellphone(__instance.cellphone);
                ModInterface.Ui.SetHeaderCellphone(__instance.header);
            }
        }
    }

    [HarmonyPatch(typeof(UiTitleCanvas), "OnInitialAnimationComplete")]
    public static class TitleCanvasPatch
    {
        public static void Prefix(UiTitleCanvas __instance)
        {
            ModInterface.Ui.GameInited = false;
            ModInterface.Ui.SetTitleCellphone(__instance.cellphone);
        }
    }

    [HarmonyPatch(typeof(UiCellphone))]
    public static class UiCellphoneMethodsPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnCellphoneButtonPressed")]
        public static bool PreCellphoneButtonPressed(UiCellphone __instance, UiCellphoneButton cellphoneButton)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance);

            if (manager == null)
            {
                return true;
            }

            manager.LoadApp(cellphoneButton.appIndex);
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("LoadOpenApp")]
        public static bool LoadOpenApp(UiCellphone __instance)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance);

            if (manager == null)
            {
                return true;
            }

            manager.LoadOpenApp();
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("LoadClosedApp")]
        public static bool LoadClosedApp(UiCellphone __instance)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance);

            if (manager == null)
            {
                return true;
            }

            manager.LoadClosedApp();
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Refresh")]
        public static bool RefreshPre(UiCellphone __instance, bool hard)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance);

            if (manager == null)
            {
                return true;
            }

            if (hard)
            {
                manager.HardRefresh();
                return false;
            }
            else
            {
                manager.PreRefresh();
                return true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("Refresh")]
        public static void RefreshPost(UiCellphone __instance, bool hard)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance);

            if (!hard
                && manager != null)
            {
                manager.PostRefresh();
            }
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppContinue))]
    public static class UiCellphoneAppContinuePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnSaveFileIconPressed")]
        public static bool OnSaveFileIconPressed(UiCellphoneAppContinue __instance, UiAppSaveFile saveFile)
        {
            if (saveFile.eraseMode || !saveFile.playerFile.started)
            {
                return false;
            }

            var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

            if (manager == null)
            {
                return true;
            }

            __instance.cellphone.SetCellFlag("reconfig_save_file_index", saveFile.saveFileIndex);

            manager.LoadApp(__instance.reconfigAppIndex);

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("OnSaveFileButtonPressed")]
        public static bool OnSaveFileButtonPressed(UiCellphoneAppContinue __instance, UiAppSaveFile saveFile)
        {
            if (saveFile.eraseMode)
            {
                return false;
            }
            else if (!saveFile.playerFile.started)
            {
                var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

                if (manager == null)
                {
                    return true;
                }

                __instance.cellphone.SetCellFlag("new_save_file_index", saveFile.saveFileIndex);

                manager.LoadApp(__instance.newAppIndex);

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppFinder))]
    public static class UiCellphoneAppFinderPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnHeadSlotPressed")]
        public static bool OnHeadSlotPressed(UiCellphoneAppFinder __instance, GirlDefinition girlDef)
        {
            if (girlDef == null)
            {
                return false;
            }

            var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

            if (manager == null)
            {
                return true;
            }

            Game.Manager.Audio.Play(AudioCategory.SOUND, __instance.sfxProfilePressed, __instance.cellphone.pauseBehavior.pauseDefinition);
            __instance.cellphone.SetCellFlag("profile_girl_id", girlDef.id);

            manager.LoadApp(__instance.profileAppIndex);

            return false;
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppGirls))]
    public static class UiCellphoneAppGirlsPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnSlotPressed")]
        public static bool OnSlotPressed(UiCellphoneAppGirls __instance, UiGirlSlot girlSlot)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

            if (manager == null)
            {
                return true;
            }

            __instance.cellphone.SetCellFlag("profile_girl_id", girlSlot.girlDefinition.id);

            manager.LoadApp(__instance.profileAppIndex);
            return false;
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppReconfig))]
    public static class UiCellphoneAppReconfigPatch
    {
        private static FieldInfo _uiCellphoneAppReconfig_reconfigPlayerFile = AccessTools.Field(typeof(UiCellphoneAppReconfig), "_reconfigPlayerFile");
        private static FieldInfo _uiCellphoneAppReconfig_reconfigSaveFileIndex = AccessTools.Field(typeof(UiCellphoneAppReconfig), "_reconfigSaveFileIndex");

        [HarmonyPrefix]
        [HarmonyPatch("OnApplyButtonPressed")]
        public static bool OnApplyButtonPressed(UiCellphoneAppReconfig __instance, ButtonBehavior buttonBehavior)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

            if (manager == null)
            {
                return true;
            }

            var reconfigPlayerFile = _uiCellphoneAppReconfig_reconfigPlayerFile.GetValue(__instance) as PlayerFile;
            reconfigPlayerFile.settingGender = (SettingGender)__instance.settingSelectorGender.selectedIndex;
            reconfigPlayerFile.settingDifficulty = (SettingDifficulty)__instance.settingSelectorDifficulty.selectedIndex;
            reconfigPlayerFile.SetFlagValue("pollys_junk", __instance.settingSelectorPolly.selectedIndex);
            reconfigPlayerFile.SetFlagValue("alpha_mode", __instance.settingSelectorAlpha.selectedIndex);
            Game.Persistence.Apply((int)_uiCellphoneAppReconfig_reconfigSaveFileIndex.GetValue(__instance));
            Game.Persistence.SaveGame();

            manager.LoadApp(__instance.continueAppIndex);
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("OnCancelButtonPressed")]
        public static bool OnCancelButtonPressed(UiCellphoneAppReconfig __instance, ButtonBehavior buttonBehavior)
        {
            var manager = ModInterface.Ui.GetCellphoneManager(__instance.cellphone);

            if (manager == null)
            {
                return true;
            }

            manager.LoadApp(__instance.continueAppIndex);

            return false;
        }
    }

    //[HarmonyPatch(typeof(UiCellphoneAppWardrobe))]
    public static class UiCellphoneAppWardrobePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnFileIconSlotSelected")]
        public static void PreFileIconSlotSelected(UiCellphoneAppWardrobe __instance, UiAppFileIconSlot fileIconSlot)
        {
            //CellphoneController.UiCellphoneAppWardrobe_PreFileIconSlotSelected(__instance, fileIconSlot);
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnFileIconSlotSelected")]
        public static void PostFileIconSlotSelected(UiCellphoneAppWardrobe __instance, UiAppFileIconSlot fileIconSlot)
        {
            //CellphoneController.UiCellphoneAppWardrobe_PostFileIconSlotSelected(__instance, fileIconSlot);
        }

        [HarmonyPrefix]
        [HarmonyPatch("Refresh")]
        public static void Refresh(UiCellphoneAppWardrobe __instance)
        {
            //CellphoneController.UiCellphoneAppWardrobe_PostWardrobeRefresh(__instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch("OnListItemSelected")]
        public static void OnListItemSelected(UiCellphoneAppWardrobe __instance, UiAppStyleSelectList selectList, bool unlocked)
        {
            //CellphoneController.UiCellphoneAppWardrobe_PreListItemSelected(selectList);
        }
    }
}
