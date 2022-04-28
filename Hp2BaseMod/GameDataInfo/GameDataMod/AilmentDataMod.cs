// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AilmentDefinition
    /// </summary>
    [UiSonElement]
    public class AilmentDataMod : DataMod, IGameDataMod<AilmentDefinition>
    {
        [UiSonTextEditUi]
        public int? ItemDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? PersistentFlags;

        [UiSonSelectorUi(DefaultData.AilmentEnableTypeNullable_As_String)]
        public AilmentEnableType? EnableType;

        [UiSonTextEditUi]
        public int? EnableTriggerIndex;

        [UiSonTextEditUi]
        public int? EnableTokenDefID;

        [UiSonTextEditUi]
        public int? EnableIntVal;

        [UiSonTextEditUi]
        public float? EnableFloatVal;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? EnableBoolVal;

        [UiSonTextEditUi]
        public string EnableStringVal;

        [UiSonTextEditUi]
        public int? EnableAbilityDefID;

        [UiSonTextEditUi]
        public int? DisableAbilityDefID;

        [UiSonMemberElement]
        public List<AilmentHintSubDefinition> Hints;

        [UiSonMemberElement]
        public List<AilmentTriggerInfo> Triggers;

        public AilmentDataMod() { }

        public AilmentDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
                              InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
            : base(def.id, InsertStyle.replace, def.name)
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

        public void SetData(AilmentDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.persistentFlags, PersistentFlags);
            ValidatedSet.SetValue(ref def.enableType, EnableType);
            ValidatedSet.SetValue(ref def.enableTriggerIndex, EnableTriggerIndex);
            ValidatedSet.SetValue(ref def.enableIntVal, EnableIntVal);
            ValidatedSet.SetValue(ref def.enableFloatVal, EnableFloatVal);
            ValidatedSet.SetValue(ref def.enableBoolVal, EnableBoolVal);

            ValidatedSet.SetValue(ref def.itemDefinition, gameDataProvider.GetItem(ItemDefinitionID.Value), InsertStyle);
            ValidatedSet.SetValue(ref def.enableTokenDef, gameDataProvider.GetToken(EnableTokenDefID.Value), InsertStyle);
            ValidatedSet.SetValue(ref def.enableAbilityDef, gameDataProvider.GetAbility(EnableAbilityDefID.Value), InsertStyle);
            ValidatedSet.SetValue(ref def.disableAbilityDef, gameDataProvider.GetAbility(DisableAbilityDefID.Value), InsertStyle);

            ValidatedSet.SetValue(ref def.enableStringVal, EnableStringVal, InsertStyle);
            ValidatedSet.SetListValue(ref def.hints, Hints, InsertStyle);

            ValidatedSet.SetListValue(ref def.triggers, Triggers, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
