// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a CutsceneDefinition
    /// </summary>
    [UiSonElement]
    public class CutsceneDataMod : DataMod, IGameDataMod<CutsceneDefinition>
    {
        [UiSonSelectorUi(DefaultData.CutsceneCleanUpTypeNullable_As_String)]
        public CutsceneCleanUpType? CleanUpType;

        [UiSonMemberElement]
        public List<CutsceneStepInfo> Steps;

        public CutsceneDataMod() { }

        public CutsceneDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public CutsceneDataMod(int id,
                               CutsceneCleanUpType? cleanUpType,
                               List<CutsceneStepInfo> steps,
                               InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            CleanUpType = cleanUpType;
            Steps = steps;
        }

        public CutsceneDataMod(CutsceneDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
        {
            CleanUpType = def.cleanUpType;
            Steps = def.steps?.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList();
        }

        public void SetData(CutsceneDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.cleanUpType, CleanUpType);

            ValidatedSet.SetListValue(ref def.steps, Steps, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
