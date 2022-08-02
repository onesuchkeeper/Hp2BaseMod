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
    /// Serializable information to make a DialogTriggerDefinition
    /// </summary>
    [UiSonElement]
    public class DialogTriggerDataMod : DataMod, IGameDataMod<DialogTriggerDefinition>
    {
        [UiSonSelectorUi(DefaultData.DialogTriggerForceTypeNullable)]
        public DialogTriggerForceType? ForceType;

        [UiSonElementSelectorUi(nameof(DialogTriggerDataMod), 0, null, "id", DefaultData.DefaultDialogTriggerNames_Name, DefaultData.DefaultDialogTriggerIds_Name)]
        public RelativeId? ResponseTriggerDefinitionID;

        [UiSonTextEditUi]
        public int? Priority;

        /// <inheritdoc/>
        public DialogTriggerDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public DialogTriggerDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal DialogTriggerDataMod(DialogTriggerDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            ForceType = def.forceType;
            ResponseTriggerDefinitionID = new RelativeId(def.responseTriggerDefinition);
            Priority = def.priority;
        }

        /// <inheritdoc/>
        public void SetData(DialogTriggerDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.forceType, ForceType);
            ValidatedSet.SetValue(ref def.priority, Priority);

            ValidatedSet.SetValue(ref def.responseTriggerDefinition,
                                  (DialogTriggerDefinition)gameDataProvider.GetDefinition(GameDataType.DialogTrigger, ResponseTriggerDefinitionID),
                                  InsertStyle);
        }
    }
}
