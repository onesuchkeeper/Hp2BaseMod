﻿using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    public class HairstyleDataMod : DataMod, IGirlSubDataMod<ExpandedHairstyleDefinition>
    {
        [UiSonTextEditUi]
        public string Name;

        [UiSonEncapsulatingUi]
        public RelativeId? FrontHairPartId;

        [UiSonEncapsulatingUi]
        public RelativeId? BackHairPartId;

        [UiSonCheckboxUi]
        public bool? IsNSFW;

        [UiSonCheckboxUi]
        public bool? HideSpecials;

        [UiSonCheckboxUi]
        public bool? TightlyPaired;

        [UiSonEncapsulatingUi]
        public RelativeId? PairOutfitId;

        /// <inheritdoc/>
        public HairstyleDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public HairstyleDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        internal HairstyleDataMod(int index,
                                  GirlDefinition girlDef, 
                                  AssetProvider assetProvider)
            : base(new RelativeId() { SourceId = -1, LocalId = index }, InsertStyle.replace, 0)
        {
            PairOutfitId = Id;
            IsNSFW = false;

            var hairstyleDef = girlDef.hairstyles[index];

            Name = hairstyleDef.hairstyleName;
            FrontHairPartId = new RelativeId(-1, hairstyleDef.partIndexFronthair);
            BackHairPartId = new RelativeId(-1, hairstyleDef.partIndexBackhair);
            HideSpecials = hairstyleDef.hideSpecials;
            TightlyPaired = hairstyleDef.tightlyPaired;
        }

        /// <inheritdoc/>
        public void SetData(ref ExpandedHairstyleDefinition def,
                            GameDefinitionProvider gameData,
                            AssetProvider assetProvider,
                            InsertStyle insertStyle,
                            RelativeId girlId)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<ExpandedHairstyleDefinition>();
            }

            var girl = gameData.GetGirl(girlId);

            ValidatedSet.SetValue(ref def.hairstyleName, Name, insertStyle);
            ValidatedSet.SetValue(ref def.IsNSFW, IsNSFW);
            ValidatedSet.SetValue(ref def.hideSpecials, HideSpecials);
            ValidatedSet.SetValue(ref def.pairOutfitIndex, ModInterface.Data.GetHairstyleIndex(girlId, PairOutfitId));
            ValidatedSet.SetValue(ref def.partIndexBackhair, ModInterface.Data.GetPartIndex(girlId, BackHairPartId));
            ValidatedSet.SetValue(ref def.partIndexFronthair, ModInterface.Data.GetPartIndex(girlId, FrontHairPartId));
        }
    }
}
