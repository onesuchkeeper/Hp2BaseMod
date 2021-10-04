// Hp2DataModLoader 2021, by onesuchkeeper

using System.Collections.Generic;
using System.IO;
using Hp2BaseMod;

namespace Hp2SampleMod
{
    public class Hp2DataModLoader : IHp2BaseModStarter
    {
        private Dictionary<GameDataType, string> dirs = new Dictionary<GameDataType, string>
        {
            { GameDataType.Ability, "mods/Hp2DataModLoader/abilityMods" },
            { GameDataType.Ailment, "mods/Hp2DataModLoader/ailmentMods" },
            { GameDataType.Code, "mods/Hp2DataModLoader/codeMods" },
            { GameDataType.Cutscene, "mods/Hp2DataModLoader/cutsceneMods" },
            { GameDataType.DialogTrigger, "mods/Hp2DataModLoader/dialogTriggerMods" },
            { GameDataType.Energy, "mods/Hp2DataModLoader/energyMods" },
            { GameDataType.Girl, "mods/Hp2DataModLoader/girlMods" },
            { GameDataType.GirlPair, "mods/Hp2DataModLoader/girlPairMods" },
            { GameDataType.Item, "mods/Hp2DataModLoader/itemMods" },
            { GameDataType.Location, "mods/Hp2DataModLoader/locationMods" },
            { GameDataType.Photo, "mods/Hp2DataModLoader/photoMods" },
            { GameDataType.Question, "mods/Hp2DataModLoader/questionMods" },
            { GameDataType.Token, "mods/Hp2DataModLoader/tokenMods" }
        };

        public void Start(GameDataModder gameDataModder)
        {
            foreach (var dir in dirs)
            {
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
