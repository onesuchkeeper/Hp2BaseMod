// Hp2Sample 2021, By OneSuchKeeper

using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;

namespace Hp2SampleMod
{
    /// <summary>
    /// Adds a code and changes Jessie's name to "Misty"
    /// </summary>
    public class Starter : IHp2ModStarter
    {
        IEnumerable<IGameDataMod<CodeDefinition>> IProvideGameDataMods.CodeDataMods => _codeDataMods;
        private CodeDataMod[] _codeDataMods;

        IEnumerable<IGirlDataMod> IProvideGameDataMods.GirlDataMods => _girlDataMods;
        private GirlDataMod[] _girlDataMods;

        IEnumerable<IGameDataMod<AbilityDefinition>> IProvideGameDataMods.AbilityDataMods => null;
        IEnumerable<IGameDataMod<AilmentDefinition>> IProvideGameDataMods.AilmentDataMods => null;
        IEnumerable<IGameDataMod<CutsceneDefinition>> IProvideGameDataMods.CutsceneDataMods => null;
        IEnumerable<IGameDataMod<DialogTriggerDefinition>> IProvideGameDataMods.DialogTriggerDataMods => null;
        IEnumerable<IGameDataMod<DlcDefinition>> IProvideGameDataMods.DlcDataMods => null;
        IEnumerable<IGameDataMod<EnergyDefinition>> IProvideGameDataMods.EnergyDataMods => null;
        IEnumerable<IGirlPairDataMod> IProvideGameDataMods.GirlPairDataMods => null;
        IEnumerable<IGameDataMod<ItemDefinition>> IProvideGameDataMods.ItemDataMods => null;
        IEnumerable<ILocationDataMod> IProvideGameDataMods.LocationDataMods => null;
        IEnumerable<IGameDataMod<PhotoDefinition>> IProvideGameDataMods.PhotoDataMods => null;
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => null;
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        /// <summary>
        /// The start method is called by the base mod before accessing the data mod enumerables
        /// defined in <see cref="IProvideGameDataMods"/>. This is the place to create your data mods and
        /// start whatever processes are needed for your mod.
        /// </summary>
        /// <param name="modId">This mod's id</param>
        public void Start(int modId)
        {
            // Log to the Hp2BaseMod.log (created in the mods folder at runtime) so we can see where this is executing
            ModInterface.Log.LogLine("Hello world!");

            // Let's make a data mod
            // Data mods have a parameterless constructor to allow them to be serialized,
            // however using it is discouraged. Mods need a valid id and insert style to function.
            // instead:

            // For a simple example, let's change Jessie's name to "Misty"
            // Jessie is a default GirlDefinition with id 2.
            // Since 2 is the id of a default GirlDefinition, the mod id needs to be -1
            _girlDataMods = new GirlDataMod[]
            {
                new GirlDataMod(new RelativeId(-1, 2), InsertStyle.replace)
                {
                    GirlName = "Misty"
                }
            };

            // What if we want to make new data instead of modifying default data?
            // Let's make a new code mod because it's fairly simple
            // The new data needs a modId to handle id conflicts and to be looked up by this mod's dependants.
            // 1 is the id of an exsisting default CodeDefinition, but it's okay for us to use here because the modId isn't -1
            // The id for this mod was passed in as an argument in this method; "modId".
            _codeDataMods = new CodeDataMod[]
            {
                new CodeDataMod( new RelativeId(modId, 1), InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("HELLO WORLD!"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Hello world on!",
                    OffMessage = "Hello world off!"
                }
            };

            // at runtime we can access a game data instance using its id and modId
            ModInterface.PreSave += () =>
            {
                var id = new RelativeId(modId, 1);

                if (ModInterface.IsCodeUnlocked(id))
                {
                    var runtimeId = ModInterface.Data.GetRuntimeDataId(GameDataType.Code, id);
                    var code = Game.Data.Codes.Get(runtimeId);
                    ModInterface.Log.LogLine($"Hp2SampleMod: Hello world! My runtime id is {runtimeId} and my hash is {code.codeHash}");
                }
            };

            // For an example on mods dependant on other mods, please see Hp2DependentSampleMod
        }
    }
}
