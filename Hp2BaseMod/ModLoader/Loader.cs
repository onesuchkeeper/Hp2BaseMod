// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod.Extension.StringExtension;
using Hp2BaseMod.GameDataInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// The Loader must be public to be accessed. 
    /// </summary>
    public static class Loader
    {
        private static readonly string _version = "1.1";
        private static readonly string _modsDir = "mods";

        private readonly static List<Hp2ModConfig> _modConfigs = new List<Hp2ModConfig>();
        private readonly static List<KeyValuePair<Hp2ModConfig, Hp2Mod>> _configModPairs = new List<KeyValuePair<Hp2ModConfig, Hp2Mod>>();

        public static void Main(string[] args)
        {
            try
            {
                // Make sure we have a mods dir
                if (!Directory.Exists(_modsDir))
                {
                    Directory.CreateDirectory(_modsDir);
                }

                ModInterface.Log.LogTitle($"HP2BaseMod, Version {_version} By OneSuchKeeper");
            }
            catch (Exception e)
            {
                File.WriteAllText(Path.Combine(_modsDir, "PreLogException.txt"), e.ToString());
            }

            using (ModInterface.Log.MakeIndent())
            {
                try
                {
                    // register defaults
                    ModInterface.Log.LogLine("Registering default data");
                    using (ModInterface.Log.MakeIndent())
                    {
                        foreach (var ability in DefaultData.DefaultAbilityIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Ability, ability.LocalId);
                        }
                        foreach (var ailment in DefaultData.DefaultAilmentIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Ailment, ailment.LocalId);
                        }
                        foreach (var code in DefaultData.DefaultCodeIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Code, code.LocalId);
                        }
                        foreach (var cutscene in DefaultData.DefaultCutsceneIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Cutscene, cutscene.LocalId);
                        }
                        foreach (var dt in DefaultData.DefaultDialogTriggerIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.DialogTrigger, dt.LocalId);
                        }
                        foreach (var dlc in DefaultData.DefaultDlcIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Dlc, dlc.LocalId);
                        }
                        foreach (var energy in DefaultData.DefaultEnergyIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Energy, energy.LocalId);
                        }
                        foreach (var girl in DefaultData.DefaultGirlIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Girl, girl.LocalId);
                        }
                        foreach (var pair in DefaultData.DefaultGirlPairIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.GirlPair, pair.LocalId);
                        }
                        foreach (var item in DefaultData.DefaultItemIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Item, item.LocalId);
                        }
                        foreach (var location in DefaultData.DefaultLocationIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Location, location.LocalId);
                        }
                        foreach (var photo in DefaultData.DefaultPhotoIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Photo, photo.LocalId);
                        }
                        foreach (var question in DefaultData.DefaultQuestionIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Question, question.LocalId);
                        }
                        foreach (var token in DefaultData.DefaultTokenIds)
                        {
                            ModInterface.Data.RegisterDefaultData(GameDataType.Token, token.LocalId);
                        }
                    }

                    // Discover mods
                    ModInterface.Log.LogLine("Discovering Mods");
                    using (ModInterface.Log.MakeIndent())
                    {
                        foreach (var dir in Directory.GetDirectories(_modsDir))
                        {
                            ModInterface.Log.LogLine($"Looking in {dir}");
                            using (ModInterface.Log.MakeIndent())
                            {
                                var configPath = Path.Combine(dir, @"Hp2ModConfig.json");

                                if (File.Exists(configPath))
                                {
                                    ModInterface.Log.LogLine($"{configPath} found!");
                                    using (ModInterface.Log.MakeIndent())
                                    {
                                        if (JsonConvert.DeserializeObject(File.ReadAllText(configPath), typeof(Hp2ModConfig)) is Hp2ModConfig config)
                                        {
                                            if (config.IsInvalid())
                                            {
                                                ModInterface.Log.LogError("Invalid config");
                                            }
                                            else
                                            {
                                                if (config.Assemblies == null)
                                                {
                                                    _modConfigs.Add(config);
                                                }
                                                else
                                                {
                                                    var badAssembly = false;

                                                    foreach (var assemblyInfo in config.Assemblies)
                                                    {
                                                        if (File.Exists(assemblyInfo.Path))
                                                        {
                                                            ModInterface.Log.LogLine("assembly found! - " + assemblyInfo.Path);
                                                        }
                                                        else
                                                        {
                                                            ModInterface.Log.LogError("Failed to find " + assemblyInfo.Path ?? "null");
                                                            badAssembly = true;
                                                        }
                                                    }

                                                    if (badAssembly)
                                                    {
                                                        ModInterface.Log.LogError("Due to missing assemblies, the mod will be ignored");
                                                    }
                                                    else
                                                    {
                                                        _modConfigs.Add(config);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ModInterface.Log.LogError($"Failed to deserialize. Check your file format.");
                                        }
                                    }
                                }
                                else
                                {
                                    ModInterface.Log.LogError($"Failed to find {configPath}");
                                }
                            }
                        }
                    }

                    // Load mods
                    ModInterface.Log.LogLine("Validating Mods");
                    using (ModInterface.Log.MakeIndent())
                    {
                        foreach (var mod in _modConfigs)
                        {
                            TryLoadMod(mod);
                        }
                    }

                    // Start mods
                    ModInterface.Log.LogLine("Starting Mods");
                    using (ModInterface.Log.MakeIndent())
                    {
                        // split mod assemblies by their stage
                        var assemblies = new Dictionary<LoadingStage, List<KeyValuePair<Hp2Mod, ModAssembly>>>()
                    {
                        {LoadingStage.First, new List<KeyValuePair<Hp2Mod, ModAssembly>>() },
                        {LoadingStage.PreNewDataIds, new List<KeyValuePair<Hp2Mod, ModAssembly>>() },
                        {LoadingStage.NewDataIds, new List<KeyValuePair<Hp2Mod, ModAssembly>>() },
                        {LoadingStage.PostNewDataIds, new List<KeyValuePair<Hp2Mod, ModAssembly>>() },
                        {LoadingStage.Last, new List<KeyValuePair<Hp2Mod, ModAssembly>>() }
                    };

                        foreach (var configToMod in _configModPairs)
                        {
                            foreach (var assembly in configToMod.Key.Assemblies)
                            {
                                assemblies[assembly.Stage].Add(new KeyValuePair<Hp2Mod, ModAssembly>(configToMod.Value, assembly));
                            }
                        }

                        // First
                        foreach (var modToAssembly in assemblies[LoadingStage.First].OrderBy(x => x.Value.Priority))
                        {
                            LoadAssembly(modToAssembly.Key, modToAssembly.Value.Path);
                        }

                        // pre new
                        foreach (var modToAssembly in assemblies[LoadingStage.PreNewDataIds].OrderBy(x => x.Value.Priority))
                        {
                            LoadAssembly(modToAssembly.Key, modToAssembly.Value.Path);
                        }

                        // add mods from files
                        foreach (var configToMod in _configModPairs)
                        {
                            configToMod.Value.AddDataMods(new FileDataModsProvider(configToMod.Key, configToMod.Value.Id));
                        }

                        // new
                        foreach (var modToAssembly in assemblies[LoadingStage.NewDataIds].OrderBy(x => x.Value.Priority))
                        {
                            LoadAssembly(modToAssembly.Key, modToAssembly.Value.Path);
                        }

                        // post new
                        foreach (var modToAssembly in assemblies[LoadingStage.PostNewDataIds].OrderBy(x => x.Value.Priority))
                        {
                            LoadAssembly(modToAssembly.Key, modToAssembly.Value.Path);
                        }

                        // last
                        foreach (var modToAssembly in assemblies[LoadingStage.Last].OrderBy(x => x.Value.Priority))
                        {
                            LoadAssembly(modToAssembly.Key, modToAssembly.Value.Path);
                        }
                    }

                    // check for options mod
                    var optiondMod = ModInterface.FindMod("Hp2ExtraOptionsMod", new Version());
                    if (optiondMod != null)
                    {
                        Codes.HubStyleChangeRateUpCodeId = new RelativeId(optiondMod.Id, 2);
                        Codes.UnpairStylesCodeId = new RelativeId(optiondMod.Id, 3);
                        Codes.RandomStylesCodeId = new RelativeId(optiondMod.Id, 4);
                    }

                    // Game Data Modifications
                    ModInterface.Log.LogLine("Applying Patches");
                    using (ModInterface.Log.MakeIndent())
                    {
                        new Harmony("Hp2BaseMod").PatchAll();
                    }
                }
                catch (Exception e)
                {
                    ModInterface.Log.LogError(e.ToString());
                }
            }

            ModInterface.Log.LogLine(Art.Random());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool TryLoadMod(Hp2ModConfig config)
        {
            // make sure it hasn't already been loaded, if so ignore
            if (_configModPairs.Any(x => x.Key == config))
            {
                return true;
            }

            ModInterface.Log.LogLine(config.Identifier.ToString());
            using (ModInterface.Log.MakeIndent())
            {
                // make sure dependencies are loaded
                if (config.Dependencies != null)
                {
                    foreach (var sourceId in config.Dependencies)
                    {
                        var dependentConfig = _modConfigs.FirstOrDefault(x => x.Identifier.Name == sourceId.Name);

                        if (dependentConfig == null)
                        {
                            ModInterface.Log.LogError($"dependency {sourceId} is missing");
                            return false;
                        }

                        var minVersion = sourceId.Version.ToVersion();

                        if (dependentConfig.Identifier.Version.ToVersion() < minVersion)
                        {
                            ModInterface.Log.LogError($"dependency {sourceId} does not meet minimum version {minVersion}, please update");
                            return false;
                        }

                        if (!TryLoadMod(dependentConfig))
                        {
                            ModInterface.Log.LogError($"dependency {sourceId} failed to load");
                            return false;
                        }
                    }
                }

                // make mod
                _configModPairs.Add(new KeyValuePair<Hp2ModConfig, Hp2Mod>(config, ModInterface.MakeNewMod(config)));
            }

            return true;
        }

        /// <summary>
        /// starts the starter and assigns local ids for its data mods
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="starter"></param>
        public static void HandleStarter(Hp2Mod mod, IHp2ModStarter starter)
        {
            var providerName = starter.GetType().Name;

            ModInterface.Log.LogLine($"Starting {mod.SourceId}'s {providerName} with id {mod.Id}");
            using (ModInterface.Log.MakeIndent())
            {
                // start
                try
                {
                    starter.Start(mod.Id);
                }
                catch (Exception e)
                {
                    ModInterface.Log.LogError(e.ToString());
                    return;
                }
            }

            // data mods
            ModInterface.Log.LogLine($"Adding data mods");
            using (ModInterface.Log.MakeIndent())
            {
                mod.AddDataMods(starter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="assemblyPath"></param>
        public static void LoadAssembly(Hp2Mod mod, string assemblyPath)
        {
            ModInterface.Log.LogLine($"Validating {assemblyPath}");
            using (ModInterface.Log.MakeIndent())
            {
                var dll = Assembly.LoadFile(assemblyPath);
                if (dll == null)
                {
                    ModInterface.Log.LogLine($"Failed to load {assemblyPath}");
                }
                else
                {
                    foreach (Type type in dll.ExportedTypes)
                    {
                        // start starters
                        if (typeof(IHp2ModStarter).IsAssignableFrom(type))
                        {
                            ModInterface.Log.LogLine($"type {type.FullName} is a starter");

                            ModInterface.Log.IncreaseIndent();

                            if (type.GetConstructor(Array.Empty<Type>()) != null)
                            {
                                HandleStarter(mod, (Activator.CreateInstance(type) as IHp2ModStarter));
                            }
                            else
                            {
                                ModInterface.Log.LogLine($"Lacking a parameterless constructor, cannot start");
                            }

                            ModInterface.Log.DecreaseIndent();
                        }
                    }
                }
            }
        }
    }
}
