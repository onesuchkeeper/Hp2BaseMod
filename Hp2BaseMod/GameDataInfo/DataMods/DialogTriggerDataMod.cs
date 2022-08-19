// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a DialogTriggerDefinition
    /// </summary>
    public class DialogTriggerDataMod : DataMod, IGameDataMod<DialogTriggerDefinition>
    {
        public DialogTriggerForceType? ForceType;

        public RelativeId? ResponseTriggerDefinitionID;

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

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;

            ResponseTriggerDefinitionID = getNewId(ResponseTriggerDefinitionID);
        }
    }
}
