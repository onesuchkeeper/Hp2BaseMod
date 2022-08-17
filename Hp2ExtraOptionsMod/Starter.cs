// Hp2LadyJizzMod 2022, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;

namespace Hp2ExtraOptionsMod
{
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
            Constants.FemaleJizzToggleCodeID = new RelativeId(modId, 0);
            Constants.SlowAffectionDrainToggleCodeID = new RelativeId(modId, 1);
            Constants.HubStyleChangeRateUpCodeId = new RelativeId(modId, 2);
            Constants.UnpairStylesCodeId = new RelativeId(modId, 3);
            Constants.RandomStylesCodeId = new RelativeId(modId, 4);
            Constants.RunInBackgroundCodeId = new RelativeId(modId, 5);

            _codeDataMods = new[]
            {
                new CodeDataMod(Constants.FemaleJizzToggleCodeID, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("JIZZ FOR ALL"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Female 'wet' photos enabled.",
                    OffMessage = "Female 'wet' photos disabled."
                },
                new CodeDataMod(Constants.HubStyleChangeRateUpCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("HOT N COLD"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Hub girl style change rate up.",
                    OffMessage = "Hub girl style change rate normal."
                },
                new CodeDataMod(Constants.RandomStylesCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("YES N NO"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Girl sim styles randomization on.",
                    OffMessage = "Girl sim styles randomization off."
                },
                new CodeDataMod(Constants.RunInBackgroundCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("STAY FOCUSED"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "The game will continue running while unfocused.",
                    OffMessage = "The game will pause when unfocused."
                },
                new CodeDataMod(Constants.UnpairStylesCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("ZOEY APPROVED"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Random girl styles unpaired.",
                    OffMessage = "Random girl styles paired."
                }
            };
            // add toggle for slow drain on bonus round TODO

            new Harmony("Hp2BaseMod.Hp2BaseModTweaks").PatchAll();
        }
    }
}
