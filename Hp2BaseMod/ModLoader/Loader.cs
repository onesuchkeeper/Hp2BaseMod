// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod.Save;
using Newtonsoft.Json;
using System;
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
        private static readonly string _version = "Beta 1.1";

        public static void Main(string[] args)
        {
            // Make sure we have a mods dir
            if (!Directory.Exists("mods"))
            {
                Directory.CreateDirectory("mods");
                return;
            }

            ModInterface modInterface = new ModInterface(File.CreateText(@"mods\Hp2BaseMod.log"));
            modInterface.Awake();

            modInterface.LogLine($"Starting HP2BaseMod, Version {_version} By OneSuchKeeper");

            try
            {
                // Discover mods
                modInterface.LogTitle("Discovering Mods");
                modInterface.IncreaseLogIndent();

                foreach (var dir in Directory.GetDirectories("mods"))
                {
                    modInterface.LogLine($"Looking in {dir}");
                    modInterface.IncreaseLogIndent();

                    var configPath = dir + @"\Hp2ModConfig.json";

                    if (File.Exists(configPath))
                    {
                        modInterface.LogLine($"{configPath} found!");
                        modInterface.IncreaseLogIndent();

                        var newMod = JsonConvert.DeserializeObject(File.ReadAllText(configPath), typeof(Hp2Mod)) as Hp2Mod;

                        if (newMod != null)
                        {
                            var badAssembly = false;

                            foreach (var assemblyInfo in newMod.Assemblies)
                            {
                                if (File.Exists(assemblyInfo.Path))
                                {
                                    modInterface.LogLine("assembly found! - " + assemblyInfo.Path);
                                }
                                else
                                {
                                    modInterface.LogLine("Failed to find " + assemblyInfo.Path);
                                    badAssembly = true;
                                }
                            }

                            if (badAssembly)
                            {
                                modInterface.LogLine("Due to missing assemblies above, mod will be ignored.");
                            }
                            else
                            {
                                modInterface.AddMod(newMod);
                            }
                        }
                        else
                        {
                            modInterface.LogLine($"Failed to deserialize. Check your file format.");
                        }

                        modInterface.DecreaseLogIndent();
                    }
                    else
                    {
                        modInterface.LogLine($"  Failed to find {configPath}");
                    }

                    modInterface.DecreaseLogIndent();
                }

                modInterface.DecreaseLogIndent();

                // Load mods
                modInterface.LogTitle("Loading Mods");
                modInterface.IncreaseLogIndent();

                // order all of the assemblies by their priority
                foreach (var assemblyInfo in modInterface.Mods.SelectMany(x => x.Assemblies).OrderBy(x => x.Priority))
                {
                    modInterface.LogLine("Loading " + assemblyInfo.Path);
                    modInterface.IncreaseLogIndent();

                    var dll = Assembly.LoadFile(assemblyInfo.Path);
                    if (dll == null)
                    {
                        modInterface.LogLine("Failed to load " + assemblyInfo.Path);
                    }
                    else
                    {
                        foreach (Type type in dll.ExportedTypes)
                        {
                            //actually load the type
                            if (typeof(IHp2BaseModStarter).IsAssignableFrom(type))
                            {
                                modInterface.LogLine($"type {type.Name} is a starter, starting");
                                modInterface.IncreaseLogIndent();
                                var c = Activator.CreateInstance(type);
                                (c as IHp2BaseModStarter).Start(modInterface);
                                modInterface.DecreaseLogIndent();
                            }
                        }
                    }

                    modInterface.DecreaseLogIndent();
                }
                modInterface.DecreaseLogIndent();

                // Game Data Modifications
                modInterface.LogTitle("Applying Patches");
                modInterface.IncreaseLogIndent();
                new Harmony("Hp2BaseMod.Hp2BaseModLoader").PatchAll();
                modInterface.LogLine("Finished!");
                modInterface.DecreaseLogIndent();
            }
            catch (Exception e)
            {
                modInterface.LogLine($"EXCEPTION:\n>Source:\n{e.Source}\n>Message:\n{e.Message}\n>StackTrace:\n{e.StackTrace}");
            }

            modInterface.IsAtRuntime = true;

            modInterface.LogLine(Art.Random());
        }
    }
}
