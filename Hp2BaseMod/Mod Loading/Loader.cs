// Hp2BaseMododLoader 2021, by OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Hp2BaseMod.Save;
using Newtonsoft.Json;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// The Loader must be public to be accessed. 
    /// </summary>
    public static class Loader
    {
        public static void Main(string[] args)
        {
            TextWriter tw = File.CreateText(@"mods\Hp2BaseModLoader.log");
            GameDataModder gameDataMod = new GameDataModder();

            try
            {
                if (!Directory.Exists("mods"))
                {
                    Directory.CreateDirectory("mods");
                    return;
                }
               
                //Discover mods
                tw.WriteLine("------Discovering mods------");
                var modsCatalog = new List<ConfigEntry>();

                foreach (var dir in Directory.GetDirectories("mods"))
                {
                    tw.WriteLine("Looking in " + dir);
                    var configPath = dir + @"\Hp2BaseMod.config";

                    if (File.Exists(configPath))
                    {
                        tw.WriteLine("  Hp2BaseMod.config found!");
                        string[] lines = File.ReadAllLines(configPath);

                        foreach (var line in lines)
                        {
                            var configEntry = new ConfigEntry(line);

                            if (!configEntry.IsValid)
                            {
                                tw.WriteLine("    Bad config entry [" + line + "], skipping");
                                break;
                            }

                            if (File.Exists(configEntry.path))
                            {
                                tw.WriteLine("    dll found! - " + configEntry.path);
                                modsCatalog.Add(configEntry);
                            }
                            else
                            {
                                tw.WriteLine("    Failed to find " + configEntry.path);
                            }
                        }
                    }
                    else
                    {
                        tw.WriteLine("  Failed to find Hp2BaseMod.config");
                    }
                }

                // Load mods
                tw.WriteLine("------Loading mods------");
                foreach (var mod in modsCatalog)
                {
                    tw.WriteLine("Loading " + mod.path);

                    var dll = Assembly.LoadFile(mod.path);
                    if (dll == null)
                    {
                        tw.WriteLine("  Failed to load " + mod.path);
                    }
                    else
                    {
                        tw.WriteLine("  Loading dll types:");
                        foreach (Type type in dll.ExportedTypes)
                        {
                            tw.WriteLine($"    Loading type: {type.Name}");

                            //actually load the type
                            if (typeof(IHp2BaseModStarter).IsAssignableFrom(type))
                            {
                                tw.WriteLine("      type is a starter, starting");
                                var c = Activator.CreateInstance(type);
                                (c as IHp2BaseModStarter).Start(gameDataMod);
                            }
                        }
                    }
                }

                // Game Data Modifications
                var harmony = new Harmony("Hp2BaseModLoader.Hp2BaseMod");

                tw.WriteLine("----Applying Game Data Modifications----");
                tw.WriteLine("Generating data mod Json");
                var AddRemoveConfig = File.CreateText(@"mods\GameDataModifier.json");
                var jsonStr = JsonConvert.SerializeObject(gameDataMod);
                AddRemoveConfig.Write(jsonStr);
                AddRemoveConfig.Flush();
                tw.WriteLine("Applying data Patch");
                GameDataPatcher.Patch(harmony);
                tw.WriteLine("Finished!");

                // Save file diff
                tw.WriteLine("----Setting up data mod savefile----");
                SaveLoadPatcher.Patch(harmony);
                tw.WriteLine("Finished!");

                // Log
                tw.Flush();
            }
            catch (Exception e)
            {
                tw.WriteLine($"EXCEPTION:\n>Source:\n{e.Source}\n>Message:\n{e.Message}\n>StackTrace:\n{e.StackTrace}");
                tw.Flush();
            }
        }
    }
}