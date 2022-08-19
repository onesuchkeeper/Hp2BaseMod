// Hp2BaseMod 2021, by OneSuchKeeper

using System.Collections.Generic;

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

    public class Hp2ModConfig
    {
        /// <summary>
        /// The mod's identifier
        /// </summary>
        public SourceIdentifier Identifier;

        /// <summary>
        /// Identifiers for the mods this is dependant on.
        /// </summary>
        public List<Dependency> Dependencies;

        /// <summary>
        /// Assemblies loaded by the mod
        /// </summary>
        public List<ModAssembly> Assemblies;

        /// <summary>
        /// Tags associated with the mod, to be looked up by other mods at runtime
        /// </summary>
        public List<ModTag> Tags;

        public List<string> AbilityDataModPaths;

        public List<string> AilmentDataModPaths;

        public List<string> CodeDataModPaths;

        public List<string> CutsceneDataModPaths;

        public List<string> DialogTriggerDataModPaths;

        public List<string> DlcDataModPaths;

        public List<string> EnergyDataModPaths;

        public List<string> GirlDataModPaths;

        public List<string> GirlPairDataModPaths;

        public List<string> ItemDataModPaths;

        public List<string> LocationDataModPaths;

        public List<string> PhotoDataModPaths;

        public List<string> QuestionDataModPaths;

        public List<string> TokenDataModPaths;

        public bool IsInvalid()
        {
            return Identifier.Name == null;
        }
    }

    public struct Dependency
    {
        public int AssumedId;

        public SourceIdentifier SourceIdentifier;
    }

    public struct SourceIdentifier
    {
        /// <summary>
        /// The source's name
        /// </summary>
        public string Name;

        /// <summary>
        /// The source's version
        /// </summary>
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
        public string Path;

        public LoadingStage Stage;

        public int Priority;
    }

    public struct ModTag
    {
        public string Name;

        public string Value;
    }
}
