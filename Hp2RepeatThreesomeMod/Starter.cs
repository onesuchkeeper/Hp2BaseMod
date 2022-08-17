// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;

namespace Hp2RepeatThreesomeMod
{
    public class Starter : IHp2ModStarter
    {
        IEnumerable<IGameDataMod<CodeDefinition>> IProvideGameDataMods.CodeDataMods => _codeDataMods;
        private CodeDataMod[] _codeDataMods;

        IEnumerable<IGirlDataMod> IProvideGameDataMods.GirlDataMods => _girlDataMods;
        private List<GirlDataMod> _girlDataMods = new List<GirlDataMod>();

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

        public void Start(int modId)
        {
            Constants.LocalCodeId = new RelativeId(modId, 0);
            Constants.NudeCodeId = new RelativeId(modId, 1);
            Constants.NudeOutfitId = new RelativeId(modId, 0);

            var nudeOutfitPart = new GirlPartDataMod(Constants.NudeOutfitId, InsertStyle.replace)
            {
                PartType = GirlPartType.OUTFIT,
                PartName = "nudeOutfit",
                X = 0,
                Y = 0,
                MirroredPartId = RelativeId.Default,
                AltPartId = RelativeId.Default,
                SpriteInfo = new SpriteInfo()
                {
                    IsExternal = false,
                    Path = "EmptySprite"
                }
            };

            var pollyNudeOutfitPartAltId = new RelativeId(modId, 1);

            var pollyNudeOutfitPart = new GirlPartDataMod(Constants.NudeOutfitId, InsertStyle.replace)
            {
                PartType = GirlPartType.OUTFIT,
                PartName = "nudeOutfitPolly",
                X = 0,
                Y = 0,
                MirroredPartId = RelativeId.Default,
                AltPartId = pollyNudeOutfitPartAltId,
                SpriteInfo = new SpriteInfo()
                {
                    IsExternal = false,
                    Path = "EmptySprite"
                }
            };

            var pollyNudeOutfitPartAlt = new GirlPartDataMod(pollyNudeOutfitPartAltId, InsertStyle.replace)
            {
                PartType = GirlPartType.OUTFIT,
                PartName = "nudeOutfitPollyAlt",
                X = 604,
                Y = 165,
                MirroredPartId = RelativeId.Default,
                AltPartId = RelativeId.Default,
                SpriteInfo = new SpriteInfo()
                {
                    IsExternal = true,
                    Path = @"mods\Hp2RepeatThreesomeMod\Images\alt_polly_nude.png"
                }
            };

            // add codes
            _codeDataMods = new CodeDataMod[]
            {
                new CodeDataMod(Constants.LocalCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("OH THE PLACES YOULL GO"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Lovers' threesome location requirement off.",
                    OffMessage = "Lovers' threesome location requirement on."
                },
                new CodeDataMod(Constants.NudeCodeId, InsertStyle.replace)
                {
                    CodeHash = MD5Utility.Encrypt("DOHOONKABHANKOLOOS"),
                    CodeType = CodeType.TOGGLE,
                    OnMessage = "Nudity durring bonus rounds on.",
                    OffMessage = "Nudity durring bonus rounds off."
                }
            };

            // add nude outfits for girls
            foreach (var girlId in ModInterface.Data.GetIds(GameDataType.Girl))
            {
                // polly has an alt
                if (girlId.SourceId == -1 && girlId.LocalId == 12)
                {
                    ModInterface.Log.LogLine($"Adding nude oufit for polly {girlId}");

                    _girlDataMods.Add(new GirlDataMod(girlId, InsertStyle.append)
                    {
                        parts = new List<GirlPartDataMod>() { pollyNudeOutfitPart, pollyNudeOutfitPartAlt },
                        outfits = new List<OutfitDataMod>()
                        {
                            new OutfitDataMod(Constants.NudeOutfitId, InsertStyle.replace)
                            {
                                Name = "Nude",
                                OutfitPartId = pollyNudeOutfitPart.Id,
                                IsNSFW = true,
                                HideNipples = false,
                                TightlyPaired = false,
                                PairHairstyleId = null
                            }
                        }
                    });
                }
                // all others
                else
                {
                    ModInterface.Log.LogLine($"Adding nude oufit for girl {girlId}");

                    _girlDataMods.Add(new GirlDataMod(girlId, InsertStyle.append)
                    {
                        parts = new List<GirlPartDataMod>() { nudeOutfitPart },
                        outfits = new List<OutfitDataMod>()
                    {
                        new OutfitDataMod(Constants.NudeOutfitId, InsertStyle.replace)
                        {
                            Name = "Nude",
                            OutfitPartId = nudeOutfitPart.Id,
                            IsNSFW = true,
                            HideNipples = false,
                            TightlyPaired = false,
                            PairHairstyleId = null
                        }
                    }
                    });
                }
            }

            ModInterface.Log.LogLine("Patching");
            using (ModInterface.Log.MakeIndent())
            {
                new Harmony("Hp2BaseMod.Hp2RepeatThreesomeMod").PatchAll();
            }
        }
    }
}
