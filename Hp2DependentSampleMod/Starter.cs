// Hp2Sample 2022, By OneSuchKeeper

using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;

namespace Hp2DependentSampleMod
{
    /// <summary>
    /// This mod mods the "Hello World" code added in the Hp2SampleMod to prepend to word "Modified" to its on and off messages
    /// </summary>
    public class Starter : IHp2ModStarter
    {
        IEnumerable<IGameDataMod<CodeDefinition>> IProvideGameDataMods.CodeDataMods => _codeDataMods;
        private CodeDataMod[] _codeDataMods;

        IEnumerable<IGameDataMod<AbilityDefinition>> IProvideGameDataMods.AbilityDataMods => null;
        IEnumerable<IGameDataMod<AilmentDefinition>> IProvideGameDataMods.AilmentDataMods => null;
        IEnumerable<IGameDataMod<CutsceneDefinition>> IProvideGameDataMods.CutsceneDataMods => null;
        IEnumerable<IGameDataMod<DialogTriggerDefinition>> IProvideGameDataMods.DialogTriggerDataMods => null;
        IEnumerable<IGameDataMod<DlcDefinition>> IProvideGameDataMods.DlcDataMods => null;
        IEnumerable<IGameDataMod<EnergyDefinition>> IProvideGameDataMods.EnergyDataMods => null;
        IEnumerable<IGirlDataMod> IProvideGameDataMods.GirlDataMods => null;
        IEnumerable<IGirlPairDataMod> IProvideGameDataMods.GirlPairDataMods => null;
        IEnumerable<IGameDataMod<ItemDefinition>> IProvideGameDataMods.ItemDataMods => null;
        IEnumerable<ILocationDataMod> IProvideGameDataMods.LocationDataMods => null;
        IEnumerable<IGameDataMod<PhotoDefinition>> IProvideGameDataMods.PhotoDataMods => null;
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => null;
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        public void Start(int modId)
        {
            // get the id for the sample mod which this mod is dependant on.
            var sampleModId = ModInterface.FindMod("Hp2SampleMod", new System.Version(1, 0))?.Id;

            // if our dependency is missing, we should not proceed and log the issue
            if (!sampleModId.HasValue)
            {
                ModInterface.Log.LogError("Missing dependancy Hp2SampleMod, halting start.");
                return;
            }

            // by using the sampleModID, we are saying this code is for what the SampleMod defines as id 1
            // which is the hello world code
            // by setting the priority to 10000, we ensure this data mod is applied after the mod from the sample mod which has the default priority of 0
            // leaving the large gap between the priorities leaves room for other data mods to correctly position themselfs in the load sequence.
            // this mod replaces the on and off messages of the hello world code with it's own values.
            _codeDataMods = new CodeDataMod[]
            {
                new CodeDataMod(new RelativeId() { SourceId = sampleModId.Value, LocalId = 1 }, InsertStyle.replace, 10000)
                {
                    OnMessage = "Modified Hello world on!",
                    OffMessage = "Modified Hello world off!"
                }
            };
        }
    }
}
