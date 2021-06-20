// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.GameDataMods.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a definition
    /// </summary>
    [Serializable]
    public class DataMod<T> : IDataMod<T>
    {
        public int Id { get; set; }
        public bool IsAdditive { get; set; }

        public DataMod() {}

        public DataMod(int id, bool isAdditive)
        {
            Id = id;
            IsAdditive = isAdditive;
        }

        public virtual void SetData(T def, GameData gameData, AssetProvider assetProvider) =>  throw new NotImplementedException();

        #region SetUtility

        internal void SetSprites(ref List<Sprite> collection, List<SpriteInfo> infos, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToSprite(assetProvider)).ToList(); }
        }

        internal void SetItems(ref List<ItemDefinition> collection, List<int> ids, GameData gameData)
        {
            if (!IsAdditive || ids != null) { collection = ids.Select(x => gameData.Items.Get(x)).ToList(); }
        }

        internal void SetGirlPairs(ref List<GirlPairDefinition> collection, List<int> ids, GameData gameData)
        {
            if (!IsAdditive || ids != null) { collection = ids.Select(x => gameData.GirlPairs.Get(x)).ToList(); }
        }

        internal void SetGirlParts(ref List<GirlPartSubDefinition> collection, List<GirlPartInfo> infos, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToGirlPart(assetProvider)).ToList(); }
        }

        internal void SetAbilitySteps(ref List<AbilityStepSubDefinition> collection, List<AbilityStepInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToAbilityStep(gameData, assetProvider)).ToList(); }
        }

        internal void SetGirlPairFavQuestions(ref List<GirlPairFavQuestionSubDefinition> collection, List<GirlPairFavQuestionInfo> infos, GameData gameData)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToGirlPairFavQuestion(gameData)).ToList(); }
        }

        internal void SetCutscenes(ref List<CutsceneDefinition> collection, List<int> ids, GameData gameData)
        {
            if (!IsAdditive || ids != null) { collection = ids.Select(x => gameData.Cutscenes.Get(x)).ToList(); }
        }

        internal void SetLogicBundles(ref List<LogicBundle> collection, List<LogicBundleInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToLogicBundle(gameData, assetProvider)).ToList(); }
        }

        internal void SetCutsceneSteps(ref List<CutsceneStepSubDefinition> collection, List<CutsceneStepInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToCutsceneStep(gameData, assetProvider)).ToList(); }
        }
        
        internal void SetDialogTriggerLineSets(ref List<DialogTriggerLineSet> collection, List<DialogTriggerLineSetInfo> infos, GameData gameData, AssetProvider assetProvider)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToDialogTriggerLineSet(gameData, assetProvider)).ToList(); }
        }

        internal void SetAilmentTriggers(ref List<AilmentTriggerSubDefinition> collection, List<AilmentTriggerInfo> infos, GameData gameData)
        {
            if (!IsAdditive || infos != null) { collection = infos.Select(x => x.ToAilmentTrigger(gameData)).ToList(); }
        }

        #endregion SetUtility
    }
}
