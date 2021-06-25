// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a CutsceneDefinition
    /// </summary>
    [Serializable]
    public class CutsceneDataMod : DataMod<CutsceneDefinition>
    {
        public CutsceneCleanUpType? CleanUpType;
        public List<CutsceneStepInfo> Steps;

        public CutsceneDataMod() { }

        public CutsceneDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public CutsceneDataMod(int id,
                               CutsceneCleanUpType? cleanUpType,
                               List<CutsceneStepInfo> steps,
                               bool isAdditive = false)
            : base(id, isAdditive)
        {
            CleanUpType = cleanUpType;
            Steps = steps;
        }

        public CutsceneDataMod(CutsceneDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            CleanUpType = def.cleanUpType;
            Steps = def.steps?.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList();
        }

        public override void SetData(CutsceneDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.cleanUpType, CleanUpType);

            SetCutsceneSteps(ref def.steps, Steps, gameData, assetProvider);
        }
    }
}
