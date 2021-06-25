// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;

namespace Hp2RepeatThreesomeMod
{
    public class Hp2RepeatThreesomeMod : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            try
            {
                var harmony = new Harmony("Hp2RepeatThreesomeMod.Hp2BaseMod");

                var localCode = new CodeDataMod(Constants.LocalCodeId, 
                                                "EA29B6A7A0AB1F669743E6C792F930F7",
                                                CodeType.TOGGLE,
                                                false,
                                                "Lovers' threesome location requirement on.",
                                                "Lovers' threesome location requirement off.");
                gameDataMod.AddData(localCode);

                var nudeCode = new CodeDataMod(Constants.NudeCodeId, 
                                               "40F45CA75FE6A9E007131D26FF9D36F6",
                                               CodeType.TOGGLE,
                                               false,
                                               "Nudity durring bonus rounds off.",
                                               "Nudity durring bonus rounds on.");
                gameDataMod.AddData(nudeCode);

                var PuzzleManager_OnRoundOver = AccessTools.Method(typeof(PuzzleManager), "OnRoundOver");
                var LeversThreesomePrefix = SymbolExtensions.GetMethodInfo(() => LoversThreesome(null));
                harmony.Patch(PuzzleManager_OnRoundOver, new HarmonyMethod(LeversThreesomePrefix));

                var UiDoll_ChangeOutfit = AccessTools.Method(typeof(UiDoll), "ChangeOutfit");
                var N69AddNudePrefix = SymbolExtensions.GetMethodInfo(() => N69AddNude(null, -1));
                harmony.Patch(UiDoll_ChangeOutfit, new HarmonyMethod(N69AddNudePrefix));
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION Hp2RepeatThreesomeMod: " + e.Message);
            }
        }

        /// <summary>
        /// Patch to have changeOutfit accept -69 to select a nude outfit
        /// </summary>
        /// <param name="__instance">UiDoll instance</param>
        /// <param name="outfitIndex">index of selected outfit</param>
        /// <returns></returns>
        public static bool N69AddNude(UiDoll __instance, int outfitIndex)
        {
            var privateFeilds = typeof(UiDoll).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var girlDefinition = (privateFeilds[2].GetValue(__instance) as GirlDefinition);

            if (girlDefinition == null) { return false; }

            var loadPartFunc = typeof(UiDoll).GetMethod("LoadPart", BindingFlags.NonPublic | BindingFlags.Instance);

            if (outfitIndex == -69)
            {
                GirlOutfitSubDefinition girlOutfitSubDefinition = new GirlOutfitSubDefinition();
                girlOutfitSubDefinition.hideNipples = false;
                girlOutfitSubDefinition.partIndexOutfit = -1;
                girlOutfitSubDefinition.outfitName = "Nude";

                loadPartFunc.Invoke(__instance, new object[] { __instance.partOutfit, girlOutfitSubDefinition.partIndexOutfit, -1f });
                __instance.partNipples.Show();
                return false;
            }
            PlayerFileGirl playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDefinition);
            outfitIndex = ((outfitIndex == -1) ? playerFileGirl.outfitIndex : outfitIndex);

            outfitIndex = Mathf.Clamp(outfitIndex, 0, girlDefinition.outfits.Count - 1);

            privateFeilds[6].SetValue(__instance, outfitIndex);

            if (outfitIndex >= 0 && outfitIndex < girlDefinition.outfits.Count)
            {
                GirlOutfitSubDefinition girlOutfitSubDefinition = girlDefinition.outfits[outfitIndex];
                loadPartFunc.Invoke(__instance, new object[] { __instance.partOutfit, girlOutfitSubDefinition.partIndexOutfit, -1f });
                if (girlOutfitSubDefinition.hideNipples)
                {
                    __instance.partNipples.Hide();
                    return false;
                }
                __instance.partNipples.Show();
            }
            return false;
        }

        /// <summary>
        /// Chenges checks to start threesome if lovers
        /// </summary>
        /// <param name="__instance">puzzlemanager instance</param>
        /// <returns></returns>
        public static bool LoversThreesome(PuzzleManager __instance)
        {
            //Access private memebrs
            var privateFeilds = typeof(PuzzleManager).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var puzzleGrid = privateFeilds[0].GetValue(__instance) as UiPuzzleGrid;

            var puzzleStatus = privateFeilds[1].GetValue(__instance) as PuzzleStatus;

            //5 is roundOverCutscene, 6 is newRoundCutscene
            privateFeilds[5].SetValue(__instance, __instance.cutsceneFailure);
            privateFeilds[6].SetValue(__instance, null);
            bool flag = true;
            switch (puzzleStatus.statusType)
            {
                case PuzzleStatusType.NORMAL:
                    {
                        GirlPairDefinition currentGirlPair = Game.Session.Location.currentGirlPair;
                        PlayerFileGirlPair playerFileGirlPair = Game.Persistence.playerFile.GetPlayerFileGirlPair(currentGirlPair);
                        if (playerFileGirlPair != null)
                        {
                            Game.Persistence.playerFile.GetPlayerFileGirl(currentGirlPair.girlDefinitionOne);
                            Game.Persistence.playerFile.GetPlayerFileGirl(currentGirlPair.girlDefinitionTwo);
                            if (puzzleGrid.roundState == PuzzleRoundState.SUCCESS)
                            {
                                if (playerFileGirlPair.relationshipType == GirlPairRelationshipType.COMPATIBLE)
                                {
                                    privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessCompatible);
                                    playerFileGirlPair.RelationshipLevelUp();
                                }
                                else if
                                (
                                    (
                                        (Game.Session.Location.currentLocation == currentGirlPair.sexLocationDefinition)
                                        &&
                                        (
                                            (playerFileGirlPair.relationshipType == GirlPairRelationshipType.ATTRACTED)
                                            ||
                                            (playerFileGirlPair.relationshipType == GirlPairRelationshipType.LOVERS)
                                        )
                                    )
                                    ||
                                    (
                                        Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.LocalCodeId)
                                        &&
                                        (playerFileGirlPair.relationshipType == GirlPairRelationshipType.LOVERS)
                                    )
                                )
                                {
                                    if (!puzzleStatus.bonusRound)
                                    {
                                        privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessAttracted);
                                        privateFeilds[6].SetValue(__instance, __instance.cutsceneNewroundBonus);
                                        flag = false;
                                    }
                                    else
                                    {
                                        privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessBonus);
                                        playerFileGirlPair.RelationshipLevelUp();
                                    }
                                }
                                else
                                {
                                    privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccess);
                                }
                                if (flag && Game.Persistence.playerFile.storyProgress >= 14 && !Game.Persistence.playerData.unlockedCodes.Contains(Game.Session.Puzzle.noAlphaModeCode))
                                {
                                    Game.Persistence.playerFile.alphaDateCount++;
                                }
                            }
                        }
                        else if (puzzleGrid.roundState == PuzzleRoundState.SUCCESS)
                        {
                            privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccess);
                        }
                        break;
                    }
                case PuzzleStatusType.NONSTOP:
                    if (puzzleGrid.roundState == PuzzleRoundState.SUCCESS)
                    {
                        privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessNonstop);
                        privateFeilds[6].SetValue(__instance, __instance.cutsceneNewroundNonstop);
                        flag = false;
                    }
                    else
                    {
                        if (puzzleStatus.roundIndex > 0)
                        {
                            privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccess);
                        }
                        if (puzzleStatus.roundIndex > Game.Persistence.playerFile.nonstopDateCount)
                        {
                            Game.Persistence.playerFile.nonstopDateCount = puzzleStatus.roundIndex;
                        }
                    }
                    break;
                case PuzzleStatusType.BOSS:
                    if (puzzleGrid.roundState == PuzzleRoundState.SUCCESS)
                    {
                        if(puzzleStatus.bonusRound)
                        {
                            privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessBonus);
                            if (Game.Persistence.playerFile.storyProgress <= 11)
                            {
                                Game.Persistence.playerFile.storyProgress = 12;
                            }
                        }
                        else if (puzzleStatus.girlListCount <= 2)
                        {
                            privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessBoss);
                            privateFeilds[6].SetValue(__instance, __instance.cutsceneNewroundBossBonus);
                            flag = false;
                        }
                        else
                        {
                            privateFeilds[5].SetValue(__instance, __instance.cutsceneSuccessBoss);
                            privateFeilds[6].SetValue(__instance, __instance.cutsceneNewroundBoss);
                            flag = false;
                        }
                    }
                    else if (Game.Persistence.playerFile.storyProgress < 12 && Game.Persistence.playerFile.GetFlagValue("nymphojinn_failure") < 0)
                    {
                        Game.Persistence.playerFile.SetFlagValue("nymphojinn_failure", 0);
                    }
                    break;
            }
            puzzleStatus.gameOver = flag;

            PuzzleManagerEvents pmEvents = new PuzzleManagerEvents(__instance, privateFeilds);
            pmEvents.AddOnRoundOverCutsceneComplete();

            Game.Session.Cutscenes.StartCutscene(privateFeilds[5].GetValue(__instance) as CutsceneDefinition, null);

            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PuzzleManagerEvents
    {
        PuzzleManager _puzzleManager;
        FieldInfo[] _privateFeilds;
        public PuzzleManagerEvents(PuzzleManager puzzleManager, FieldInfo[] privateFeilds)
        {
            _puzzleManager = puzzleManager;
            _privateFeilds = privateFeilds;
        }

        public void AddOnRoundOverCutsceneComplete()
        {
            Game.Session.Cutscenes.CutsceneCompleteEvent += OnRoundOverCutsceneComplete;
        }

        public void OnRoundOverCutsceneComplete()
        {
            var cutscene = _privateFeilds[6].GetValue(_puzzleManager) as CutsceneDefinition;
            var nodeCode = Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.NudeCodeId);

            Game.Session.Cutscenes.CutsceneCompleteEvent -= OnRoundOverCutsceneComplete;
            if (nodeCode && cutscene == _puzzleManager.cutsceneNewroundBonus)
            {
                Game.Session.gameCanvas.dollLeft.ChangeOutfit(-69);
                Game.Session.gameCanvas.dollRight.ChangeOutfit(-69);
            }

            typeof(PuzzleManager).GetMethod("OnRoundOverCutsceneComplete", BindingFlags.NonPublic | BindingFlags.Instance)
                                 .Invoke(_puzzleManager, null);

            if (nodeCode && cutscene == _puzzleManager.cutsceneNewroundBossBonus)
            {
                Game.Session.gameCanvas.dollLeft.ChangeOutfit(-69);
                Game.Session.gameCanvas.dollRight.ChangeOutfit(-69);
            }
        }

        public static bool unlockHash(string hash)
        {//I dont want to add in linq
            foreach(var code in Game.Persistence.playerData.unlockedCodes)
            {
                if (code.codeHash == hash) { return true; }
            }
            return false;
        }
    }
}
