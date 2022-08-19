// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a DlcDefinition
    /// </summary>
    public class DlcDataMod : DataMod, IGameDataMod<DlcDefinition>
    {
        public string DlcName;

        /// <inheritdoc/>
        public DlcDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public DlcDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        internal DlcDataMod(DlcDefinition def)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            DlcName = def.dlcName;
        }

        /// <inheritdoc/>
        public void SetData(DlcDefinition def, GameDefinitionProvider _, AssetProvider __)
        {
            ValidatedSet.SetValue(ref def.dlcName, DlcName, InsertStyle);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;
        }
    }
}
