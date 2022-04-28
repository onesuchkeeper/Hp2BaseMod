// Hp2DataModLoader 2021, by onesuchkeeper

using System.Collections.Generic;
using System.IO;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;

namespace Hp2SampleMod
{
    public class Hp2DataModLoader : IHp2BaseModStarter
    {
        private Dictionary<GameDataType, string> dirs = new Dictionary<GameDataType, string>
        {
            { GameDataType.Ability, $"mods/Hp2DataModLoader/{nameof(AbilityDataMod)}" },
            { GameDataType.Ailment, $"mods/Hp2DataModLoader/{nameof(AilmentDataMod)}" },
            { GameDataType.Code, $"mods/Hp2DataModLoader/{nameof(CodeDataMod)}" },
            { GameDataType.Cutscene, $"mods/Hp2DataModLoader/{nameof(CutsceneDataMod)}" },
            { GameDataType.DialogTrigger, $"mods/Hp2DataModLoader/{nameof(DialogTriggerDataMod)}" },
            { GameDataType.Energy, $"mods/Hp2DataModLoader/{nameof(EnergyDataMod)}" },
            { GameDataType.Girl, $"mods/Hp2DataModLoader/{nameof(GirlDataMod)}" },
            { GameDataType.GirlPair, $"mods/Hp2DataModLoader/{nameof(GirlPairDataMod)}" },
            { GameDataType.Item, $"mods/Hp2DataModLoader/{nameof(ItemDataMod)}" },
            { GameDataType.Location, $"mods/Hp2DataModLoader/{nameof(LocationDataMod)}" },
            { GameDataType.Photo, $"mods/Hp2DataModLoader/{nameof(PhotoDataMod)}" },
            { GameDataType.Question, $"mods/Hp2DataModLoader/{nameof(QuestionDataMod)}" },
            { GameDataType.Token, $"mods/Hp2DataModLoader/{nameof(TokenDataMod)}" }
        };

        public void Start(ModInterface gameDataModder)
        {
            foreach (var dir in dirs)
            {
                if (!Directory.Exists(dir.Value))
                {
                    Directory.CreateDirectory(dir.Value);
                }

                try
                {
                    foreach (var path in Directory.GetFiles(dir.Value))
                    {
                        gameDataModder.LogLine($"Adding Data Mod: {path}");
                        gameDataModder.AddData(dir.Key, path);
                    }
                }
                catch (DirectoryNotFoundException e)
                {
                    gameDataModder.LogLine("Error: " + e.Message);
                }
            }
        }
    }
}
