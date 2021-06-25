// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a DlcDefinition
    /// </summary>
    [Serializable]
    public class DlcDataMod : DataMod<DlcDefinition>
    {
        public string DlcName;

        public DlcDataMod() { }

        public DlcDataMod(int id,
                          string dlcName,
                          bool isAdditive = false)
            : base(id, isAdditive)
        {
            DlcName = dlcName;
        }

        public DlcDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public override void SetData(DlcDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            if (IsAdditive)
            {
                Access.NullSet(ref def.dlcName, DlcName);
            }
            else
            {
                def.dlcName = DlcName;
            }
        }
    }
}
