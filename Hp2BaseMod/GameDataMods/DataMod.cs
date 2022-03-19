// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.GameDataMods.Interface;
using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;
using UnityEngine;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a definition
    /// </summary>
    [UiSonGroup("Mod Info", 100, DisplayMode.Vertial, Alignment.Stretch, GroupType.Expander)]
    public class DataMod<T> : IDataMod<T>
    {
        private const string intOnlyRegex = "^-?[0-9]+$";

        [UiSonTextEditUi(0, "Mod Info", intOnlyRegex)]
        [UiSonTag("Id")]
        public int Id { get; set; }

        [UiSonCheckboxUi("Mod Info")]
        public bool IsAdditive { get; set; }

        [UiSonTextEditUi(0, "Mod Info")]
        public string ModName;

        [UiSonCollection]
        [UiSonMemberClass(0, "Mod Info")]
        public List<ModTag> Tags;

        public DataMod() { }

        public DataMod(int id, bool isAdditive)
        {
            Id = id;
            IsAdditive = isAdditive;
        }

        public virtual void SetData(T def, GameData gameData, AssetProvider assetProvider) => throw new NotImplementedException();

        #region SetUtility

        internal void SetSprites(ref List<Sprite> collection, List<SpriteInfo> infos, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var sprites = infos.Select(x => x.ToSprite(assetProvider)).ToList() ?? new List<Sprite>();

                if (IsAdditive && collection != null) { sprites.AddRange(collection); }

                collection = sprites;
            }
        }

        internal void SetItems(ref List<ItemDefinition> collection, List<int> ids, GameData gameData)
        {
            if (ids != null)
            {
                var items = ids.Select(x => gameData.Items.Get(x)).ToList();

                if (IsAdditive && collection != null) { items.AddRange(collection); }

                collection = items;
            }
        }

        internal void SetGirlPairs(ref List<GirlPairDefinition> collection, List<int> ids, GameData gameData)
        {
            if (ids != null)
            {
                var girlPairs = ids.Select(x => gameData.GirlPairs.Get(x)).ToList();

                if (IsAdditive && collection != null) { girlPairs.AddRange(collection); }

                collection = girlPairs;
            }
        }

        internal void SetGirlParts(ref List<GirlPartSubDefinition> collection, List<GirlPartInfo> infos, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var girlParts = infos.Select(x => x.ToGirlPart(assetProvider)).ToList();

                if (IsAdditive && collection != null) { girlParts.AddRange(collection); }

                collection = girlParts;
            }
        }

        internal void SetAbilitySteps(ref List<AbilityStepSubDefinition> collection, List<AbilityStepInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var abilitySteps = infos.Select(x => x.ToAbilityStep(gameData, assetProvider)).ToList();

                if (IsAdditive && collection != null) { abilitySteps.AddRange(collection); }

                collection = abilitySteps;
            }
        }

        internal void SetGirlPairFavQuestions(ref List<GirlPairFavQuestionSubDefinition> collection, List<GirlPairFavQuestionInfo> infos, GameData gameData)
        {
            if (infos != null)
            {
                var girlPairFavQuestions = infos.Select(x => x.ToGirlPairFavQuestion(gameData)).ToList();

                if (IsAdditive && collection != null) { girlPairFavQuestions.AddRange(collection); }

                collection = girlPairFavQuestions;
            }
        }

        internal void SetCutscenes(ref List<CutsceneDefinition> collection, List<int> ids, GameData gameData)
        {
            if (ids != null)
            {
                var cutscenes = ids.Select(x => gameData.Cutscenes.Get(x)).ToList();

                if (IsAdditive && collection != null) { cutscenes.AddRange(collection); }

                collection = cutscenes;
            }
        }

        internal void SetLogicBundles(ref List<LogicBundle> collection, List<LogicBundleInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var logicBundles = infos.Select(x => x.ToLogicBundle(gameData, assetProvider)).ToList();

                if (IsAdditive && collection != null) { logicBundles.AddRange(collection); }

                collection = logicBundles;
            }
        }

        internal void SetCutsceneSteps(ref List<CutsceneStepSubDefinition> collection, List<CutsceneStepInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var cutsceneSteps = infos.Select(x => x.ToCutsceneStep(gameData, assetProvider)).ToList();

                if (IsAdditive && collection != null) { cutsceneSteps.AddRange(collection); }

                collection = cutsceneSteps;
            }
        }

        internal void SetDialogTriggerLineSets(ref List<DialogTriggerLineSet> collection, List<DialogTriggerLineSetInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (infos != null)
            {
                var dialogTriggers = infos.Select(x => x.ToDialogTriggerLineSet(gameData, assetProvider)).ToList();

                if (IsAdditive && collection != null) { dialogTriggers.AddRange(collection); }

                collection = dialogTriggers;
            }
        }

        internal void SetAilmentTriggers(ref List<AilmentTriggerSubDefinition> collection, List<AilmentTriggerInfo> infos, GameData gameData)
        {
            if (infos != null)
            {
                var aimentTriggers = infos.Select(x => x.ToAilmentTrigger(gameData)).ToList();

                if (IsAdditive && collection != null) { aimentTriggers.AddRange(collection); }

                collection = aimentTriggers;
            }
        }

        #endregion SetUtility
    }
}
