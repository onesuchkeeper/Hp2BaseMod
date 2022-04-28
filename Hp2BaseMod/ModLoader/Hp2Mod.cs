﻿// Hp2BaseMod 2021, by OneSuchKeeper

using System.Collections.Generic;
using UiSon.Attribute;

namespace Hp2BaseMod
{
    [UiSonElement]
    public class Hp2Mod
    {
        [UiSonTextEditUi]
        public string Name;

        [UiSonTextEditUi]
        public string Version;

        [UiSonTextEditUi]
        public string Description;

        /// <summary>
        /// Assemblies loaded by the mod
        /// </summary>
        [UiSonCollection]
        [UiSonMemberElement]
        public List<ModAssembly> Assemblies;

        /// <summary>
        /// Tags associated with the mod, to be accessed by other mods at runtime
        /// </summary>
        [UiSonCollection]
        [UiSonMemberElement]
        public List<ModTag> Tags;
    }
}