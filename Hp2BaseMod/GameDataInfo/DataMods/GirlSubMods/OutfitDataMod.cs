using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable info to modify an expanded outfit def
    /// </summary>
    public class OutfitDataMod : DataMod, IGirlSubDataMod<ExpandedOutfitDefinition>
    {
        [UiSonTextEditUi]
        public string Name;

        [UiSonEncapsulatingUi]
        public RelativeId? OutfitPartId;

        [UiSonCheckboxUi]
        public bool? IsNSFW;

        [UiSonCheckboxUi]
        public bool? HideNipples;

        [UiSonCheckboxUi]
        public bool? TightlyPaired;

        [UiSonEncapsulatingUi]
        public RelativeId? PairHairstyleId;

        /// <inheritdoc/>
        public OutfitDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public OutfitDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        internal OutfitDataMod(int index,
                               GirlDefinition girlDef,
                               AssetProvider assetProvider)
            : base(new RelativeId() { SourceId = -1, LocalId = index }, InsertStyle.replace, 0)
        {
            PairHairstyleId = Id;
            IsNSFW = false;

            var outfitDef = girlDef.outfits[index];

            Name = outfitDef.outfitName;
            OutfitPartId = new RelativeId(-1, outfitDef.partIndexOutfit);
            HideNipples = outfitDef.hideNipples;
            TightlyPaired = outfitDef.tightlyPaired;
        }

        /// <inheritdoc/>
        public void SetData(ref ExpandedOutfitDefinition def,
                            GameDefinitionProvider gameData,
                            AssetProvider assetProvider,
                            InsertStyle insertStyle,
                            RelativeId girlId)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<ExpandedOutfitDefinition>();
            }

            ValidatedSet.SetValue(ref def.outfitName, Name, insertStyle);
            ValidatedSet.SetValue(ref def.IsNSFW, IsNSFW);
            ValidatedSet.SetValue(ref def.hideNipples, HideNipples);
            ValidatedSet.SetValue(ref def.pairHairstyleIndex, ModInterface.Data.GetHairstyleIndex(girlId, PairHairstyleId));
            ValidatedSet.SetValue(ref def.partIndexOutfit, ModInterface.Data.GetPartIndex(girlId, OutfitPartId));
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;

            OutfitPartId = getNewId(OutfitPartId);

            PairHairstyleId = getNewId(PairHairstyleId);
        }
    }
}
