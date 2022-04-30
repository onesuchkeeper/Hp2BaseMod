// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a definition
    /// </summary>
    [UiSonGroup("Mod Info", 100, DisplayMode.Vertial)]
    public abstract class DataMod
    {
        [UiSonTextEditUi(0, "Mod Info")]
        [UiSonTag("id")]
        public int Id { get; set; }

        [UiSonTextEditUi(0, "Mod Info")]
        public int LoadPriority { get; set; }

        [UiSonSelectorUi(DefaultData.InsertStyle_As_String, 0, "Mod Info")]
        public InsertStyle InsertStyle { get; set; }

        [UiSonTextEditUi(0, "Mod Info")]
        public string ModName;

        [UiSonCollection]
        [UiSonMemberElement(0, "Mod Info")]
        public List<ModTag> Tags;

        public DataMod() { }

        public DataMod(int id, InsertStyle insertStyle, string name = null)
        {
            Id = id;
            InsertStyle = insertStyle;
            ModName = name;
        }
    }
}
