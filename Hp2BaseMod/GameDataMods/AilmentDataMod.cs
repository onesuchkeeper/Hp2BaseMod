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
    /// Serializable information to make an AilmentDefinition
    /// </summary>
    [Serializable]
    public class AilmentDataMod : DataMod<AilmentDefinition>
    {
        public int? ItemDefinitionID;
		public bool? PersistentFlags;
		public AilmentEnableType? EnableType;
		public int? EnableTriggerIndex;
		public int? EnableTokenDefID;
		public int? EnableIntVal;
		public float? EnableFloatVal;
		public bool? EnableBoolVal;
		public string EnableStringVal;
		public int? EnableAbilityDefID;
		public int? DisableAbilityDefID;
		public List<AilmentHintSubDefinition> Hints;
		public List<AilmentTriggerInfo> Triggers;

        public AilmentDataMod() { }

        public AilmentDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public AilmentDataMod(int id,
                              int? itemDefinitionID,
                              bool? persistentFlags,
                              AilmentEnableType? enableType,
                              int? enableTriggerIndex,
                              int? enableTokenDefID,
                              int? enableIntVal,
                              float? enableFloatVal,
                              bool? enableBoolVal,
                              string enableStringVal,
                              int? enableAbilityDefID,
                              int? disableAbilityDefID,
                              List<AilmentHintSubDefinition> hints,
                              List<AilmentTriggerInfo> triggers,
                              bool isAdditive = false)
            :base(id, isAdditive)
        {
            ItemDefinitionID = itemDefinitionID;
            PersistentFlags = persistentFlags;
            EnableType = enableType;
            EnableTriggerIndex = enableTriggerIndex;
            EnableTokenDefID = enableTokenDefID;
            EnableIntVal = enableIntVal;
            EnableFloatVal = enableFloatVal;
            EnableBoolVal = enableBoolVal;
            EnableStringVal = enableStringVal;
            EnableAbilityDefID = enableAbilityDefID;
            DisableAbilityDefID = disableAbilityDefID;
            Hints = hints;
            Triggers = triggers;
        }

        public AilmentDataMod(AilmentDefinition def)
            :base(def.id, false)
        {
            PersistentFlags = def.persistentFlags;
            EnableType = def.enableType;
            EnableTriggerIndex = def.enableTriggerIndex;
            Hints = def.hints;
            EnableIntVal = def.enableIntVal;
            EnableFloatVal = def.enableFloatVal;
            EnableBoolVal = def.enableBoolVal;
            EnableStringVal = def.enableStringVal;

            EnableTokenDefID = def.enableTokenDef?.id ?? -1;
            EnableAbilityDefID = def.enableAbilityDef?.id ?? -1;
            DisableAbilityDefID = def.disableAbilityDef?.id ?? -1;
            ItemDefinitionID = def.itemDefinition?.id ?? -1;

            Triggers = def.triggers?.Select(x => new AilmentTriggerInfo(x)).ToList();
        }

        public override void SetData(AilmentDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.persistentFlags, PersistentFlags);
            Access.NullableSet(ref def.enableType, EnableType);
            Access.NullableSet(ref def.enableTriggerIndex, EnableTriggerIndex);
            Access.NullableSet(ref def.enableIntVal, EnableIntVal);
            Access.NullableSet(ref def.enableFloatVal, EnableFloatVal);
            Access.NullableSet(ref def.enableBoolVal, EnableBoolVal);

            if (ItemDefinitionID.HasValue) { def.itemDefinition = gameData.Items.Get(ItemDefinitionID.Value); }
            if (EnableTokenDefID.HasValue) { def.enableTokenDef = gameData.Tokens.Get(EnableTokenDefID.Value); }
            if (EnableAbilityDefID.HasValue) { def.enableAbilityDef = gameData.Abilities.Get(EnableAbilityDefID.Value); }
            if (DisableAbilityDefID.HasValue) { def.disableAbilityDef = gameData.Abilities.Get(DisableAbilityDefID.Value); }

            Access.NullSet(ref def.enableStringVal, EnableStringVal);
            Access.NullSet(ref def.hints, Hints);

            SetAilmentTriggers(ref def.triggers, Triggers, gameData);
        }
    }
}
