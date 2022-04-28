// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a DlcDefinition
    /// </summary>
    [UiSonElement]
    public class DlcDataMod : DataMod, IGameDataMod<DlcDefinition>
    {
        [UiSonTextEditUi]
        public string DlcName;

        public DlcDataMod() { }

        public DlcDataMod(int id,
                          string dlcName,
                          InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            DlcName = dlcName;
        }

        public DlcDataMod(DlcDefinition def)
            : base(def.id, InsertStyle.replace, def.name)
        {
        }

        public void SetData(DlcDefinition def, GameDataProvider _, AssetProvider __, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.dlcName, DlcName, InsertStyle);
        }
    }
}
