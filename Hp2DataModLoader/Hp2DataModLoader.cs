// Hp2DataModLoader 2021, by onesuchkeeper

using System;
using System.IO;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;

namespace Hp2SampleMod
{
    public class Hp2DataModLoader : IHp2BaseModStarter
    {
        private readonly string BasePath = "mods/Hp2DataModLoader";

        public void Start(GameDataModder gameDataModder)
        {
            foreach(var path in Directory.GetFiles(BasePath + "/abilityMods"))
            {
                gameDataModder.AddData<AbilityDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/ailmentMods"))
            {
                gameDataModder.AddData<AilmentDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/codeMods"))
            {
                gameDataModder.AddData<CodeDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/cutsceneMods"))
            {
                gameDataModder.AddData<CutsceneDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/dialogTriggerMods"))
            {
                gameDataModder.AddData<DialogTriggerDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/energyMods"))
            {
                gameDataModder.AddData<EnergyDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/girlMods"))
            {
                gameDataModder.AddData<GirlDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/girlPairMods"))
            {
                gameDataModder.AddData<GirlPairDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/itemMods"))
            {
                gameDataModder.AddData<ItemDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/locationMods"))
            {
                gameDataModder.AddData<LocationDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/photoMods"))
            {
                gameDataModder.AddData<PhotoDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/questionMods"))
            {
                gameDataModder.AddData<QuestionDataMod>(path);
            }
            foreach (var path in Directory.GetFiles(BasePath + "/tokenMods"))
            {
                gameDataModder.AddData<TokenDataMod>(path);
            }
        }
    }
}
