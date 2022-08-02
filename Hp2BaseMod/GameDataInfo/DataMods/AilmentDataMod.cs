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
    [UiSonGroup("Enable", -1)]
    [UiSonGroup("Disable", -2)]
    public class AilmentDataMod : DataMod, IGameDataMod<AilmentDefinition>
    {
        [UiSonElementSelectorUi(nameof(ItemDataMod), 1, null, "id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public RelativeId? ItemDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 1, null, "NullableBoolIds")]
        public bool? PersistentFlags;

        [UiSonSelectorUi(DefaultData.AilmentEnableTypeNullable, 0, "Enable")]
        public AilmentEnableType? EnableType;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Enable", "id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
        public RelativeId? EnableAbilityDefID;

        [UiSonElementSelectorUi(nameof(DialogTriggerDataMod), 0, "Enable", "id", DefaultData.DefaultDialogTriggerNames_Name, DefaultData.DefaultDialogTriggerIds_Name)]
        public int? EnableTriggerIndex;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, "Enable", "id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public RelativeId? EnableTokenDefID;

        [UiSonTextEditUi(0, "Enable")]
        public int? EnableIntVal;

        [UiSonTextEditUi(0, "Enable")]
        public float? EnableFloatVal;

        [UiSonSelectorUi("NullableBoolNames", 0, "Enable", "NullableBoolIds")]
        public bool? EnableBoolVal;

        [UiSonTextEditUi(0, "Enable")]
        public string EnableStringVal;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Disable", "id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
        public RelativeId? DisableAbilityDefID;

        [UiSonEncapsulatingUi]
        public List<AilmentHintSubDefinition> Hints;

        [UiSonEncapsulatingUi]
        public List<AilmentTriggerInfo> Triggers;

        /// <inheritdoc/>
        public AilmentDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>

        public AilmentDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        internal AilmentDataMod(AilmentDefinition def)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            PersistentFlags = def.persistentFlags;
            EnableType = def.enableType;
            EnableTriggerIndex = def.enableTriggerIndex;
            Hints = def.hints;
            EnableIntVal = def.enableIntVal;
            EnableFloatVal = def.enableFloatVal;
            EnableBoolVal = def.enableBoolVal;
            EnableStringVal = def.enableStringVal;

            EnableTokenDefID = new RelativeId(def.enableTokenDef);
            EnableAbilityDefID = new RelativeId(def.enableAbilityDef);
            DisableAbilityDefID = new RelativeId(def.disableAbilityDef);
            ItemDefinitionID = new RelativeId(def.itemDefinition);

            Triggers = def.triggers?.Select(x => new AilmentTriggerInfo(x)).ToList();
        }

        /// <inheritdoc/>
        public void SetData(AilmentDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.persistentFlags, PersistentFlags);
            ValidatedSet.SetValue(ref def.enableType, EnableType);
            ValidatedSet.SetValue(ref def.enableTriggerIndex, EnableTriggerIndex);
            ValidatedSet.SetValue(ref def.enableIntVal, EnableIntVal);
            ValidatedSet.SetValue(ref def.enableFloatVal, EnableFloatVal);
            ValidatedSet.SetValue(ref def.enableBoolVal, EnableBoolVal);
            
            ValidatedSet.SetValue(ref def.itemDefinition,
                                  (ItemDefinition)gameDataProvider.GetDefinition(GameDataType.Item, ItemDefinitionID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableTokenDef,
                                  (TokenDefinition)gameDataProvider.GetDefinition(GameDataType.Token, EnableTokenDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableAbilityDef,
                                  (AbilityDefinition)gameDataProvider.GetDefinition(GameDataType.Ability, EnableAbilityDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.disableAbilityDef,
                                  (AbilityDefinition)gameDataProvider.GetDefinition(GameDataType.Ability, DisableAbilityDefID),
                                  InsertStyle);

            ValidatedSet.SetValue(ref def.enableStringVal, EnableStringVal, InsertStyle);
            ValidatedSet.SetListValue(ref def.hints, Hints, InsertStyle);
            ValidatedSet.SetListValue(ref def.triggers, Triggers, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
