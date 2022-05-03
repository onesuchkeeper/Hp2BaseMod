﻿// Hp2BaseMod 2021, By OneSuchKeeper

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
    /// Serializable information to make an AilmentDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Enable", -1)]
    [UiSonGroup("Disable", -2)]
    public class AilmentDataMod : DataMod, IGameDataMod<AilmentDefinition>
    {
        [UiSonElementSelectorUi(nameof(ItemDataMod), 1, null, "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public int? ItemDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 1)]
        public bool? PersistentFlags;

        [UiSonSelectorUi(DefaultData.AilmentEnableTypeNullable_As_String, 0, "Enable")]
        public AilmentEnableType? EnableType;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Enable", "Id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
        public int? EnableAbilityDefID;

        [UiSonElementSelectorUi(nameof(DialogTriggerDataMod), 0, "Enable", "Id", DefaultData.DefaultDialogTriggerNames_Name, DefaultData.DefaultDialogTriggerIds_Name)]
        public int? EnableTriggerIndex;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, "Enable", "Id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public int? EnableTokenDefID;

        [UiSonTextEditUi(0, "Enable")]
        public int? EnableIntVal;

        [UiSonTextEditUi(0, "Enable")]
        public float? EnableFloatVal;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Enable")]
        public bool? EnableBoolVal;

        [UiSonTextEditUi(0, "Enable")]
        public string EnableStringVal;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Disable", "Id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
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
            ModInterface.Instance.LogLine("Setting data for an ailment");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;

            ValidatedSet.SetValue(ref def.persistentFlags, PersistentFlags);
            ValidatedSet.SetValue(ref def.enableType, EnableType);
            ValidatedSet.SetValue(ref def.enableTriggerIndex, EnableTriggerIndex);
            ValidatedSet.SetValue(ref def.enableIntVal, EnableIntVal);
            ValidatedSet.SetValue(ref def.enableFloatVal, EnableFloatVal);
            ValidatedSet.SetValue(ref def.enableBoolVal, EnableBoolVal);
            
            ValidatedSet.SetValue(ref def.itemDefinition,
                                  gameDataProvider.GetItem(ItemDefinitionID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableTokenDef,
                                  gameDataProvider.GetToken(EnableTokenDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableAbilityDef,
                                  gameDataProvider.GetAbility(EnableAbilityDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.disableAbilityDef,
                                  gameDataProvider.GetAbility(DisableAbilityDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableStringVal, EnableStringVal, InsertStyle);
            ValidatedSet.SetListValue(ref def.hints, Hints, InsertStyle);
            ValidatedSet.SetListValue(ref def.triggers, Triggers, InsertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
