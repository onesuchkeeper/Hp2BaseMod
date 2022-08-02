// Hp2BaseMod 2021, by OneSuchKeeper

using System.Collections.Generic;
using UiSon.Attribute;

namespace Hp2BaseMod
{
    public enum LoadingStage
    {
        First,
        PreNewDataIds,
        NewDataIds,
        PostNewDataIds,
        Last
    }

    [UiSonArray("LoadingStages", typeof(LoadingStage))]
    [UiSonElement]
    [UiSonGroup("Directories")]
    public class Hp2ModConfig
    {
        /// <summary>
        /// The mod's identifier
        /// </summary>
        [UiSonEncapsulatingUi]
        public SourceIdentifier Identifier;

        /// <summary>
        /// Identifiers for the mods this is dependant on.
        /// </summary>
        [UiSonTextEditUi]
        public List<Dependency> Dependencies;

        /// <summary>
        /// Assemblies loaded by the mod
        /// </summary>
        [UiSonEncapsulatingUi]
        public List<ModAssembly> Assemblies;

        /// <summary>
        /// Tags associated with the mod, to be looked up by other mods at runtime
        /// </summary>
        [UiSonEncapsulatingUi]
        public List<ModTag> Tags;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> AbilityDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> AilmentDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> CodeDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> CutsceneDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> DialogTriggerDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> DlcDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> EnergyDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> GirlDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> GirlPairDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> ItemDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> LocationDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> PhotoDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> QuestionDataModPaths;

        [UiSonTextEditUi(0, "Directories")]
        public List<string> TokenDataModPaths;

        public bool IsInvalid()
        {
            return Identifier.Name == null;
        }
    }

    public struct Dependency
    {
        [UiSonTextEditUi]
        public int AssumedId;

        [UiSonEncapsulatingUi]
        public SourceIdentifier SourceIdentifier;
    }

    public struct SourceIdentifier
    {
        /// <summary>
        /// The source's name
        /// </summary>
        [UiSonTextEditUi]
        public string Name;

        /// <summary>
        /// The source's version
        /// </summary>
        [UiSonTextEditUi]
        public string Version;

        public override string ToString() => $"{Name} {Version}";

        public override bool Equals(object obj)
        {
            return obj is SourceIdentifier identifier &&
                   Name == identifier.Name &&
                   Version == identifier.Version;
        }

        public override int GetHashCode()
        {
            int hashCode = 2112831277;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Version);
            return hashCode;
        }

        public static bool operator !=(SourceIdentifier x, SourceIdentifier y)
        {
            return !(x == y);
        }

        public static bool operator ==(SourceIdentifier x, SourceIdentifier y)
        {
            return x.Name == y.Name
                   && x.Version == y.Version;
        }
    }

    public struct ModAssembly
    {
        [UiSonTextEditUi]
        public string Path;

        [UiSonSelectorUi("LoadingStages")]
        public LoadingStage Stage;

        [UiSonTextEditUi]
        public int Priority;
    }

    public struct ModTag
    {
        [UiSonTextEditUi]
        public string Name;

        [UiSonTextEditUi]
        public string Value;
    }
}
