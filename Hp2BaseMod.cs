using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Hp2BaseMod.Interface;

namespace Hp2BaseMod
{
    public static class Loader
    {
        public static void Main(string[] args)
        {
            TextWriter tw = File.CreateText("Hp2BaseMod.log");

            try
            {
                if (!Directory.Exists("mods"))
                {
                    Directory.CreateDirectory("mods");
                    return;
                }

                //Discover mods
                tw.WriteLine("Discovering mods...");
                var modsCatalog = new List<Tuple<string, int>>();

                foreach(var dir in Directory.GetDirectories("mods"))
                {
                    tw.WriteLine(">Looking in " + dir);
                    var configPath = dir + @"\Hp2BaseMod.config";

                    if(File.Exists(configPath))
                    {
                        tw.WriteLine(">Hp2BaseMod.config found!");
                        string[] lines = File.ReadAllLines(configPath);

                        foreach(var line in lines)
                        {
                            var elements = ParseConfig(line);

                            if (elements == null)
                            {
                                tw.WriteLine(">Bad config entry :( - " + line);
                                break;
                            }

                            if (File.Exists(elements[0]))
                            {
                                tw.WriteLine(">dll found! - " + elements[0]);
                                modsCatalog.Add(new Tuple<string, int>(elements[0], int.Parse(elements[1])));
                            }
                            else
                            {
                                tw.WriteLine(">Failed to find " + elements[0]);
                            }
                        }
                    }
                    else
                    {
                        tw.WriteLine(">Failed to find Hp2BaseMod.config");
                    }
                }

                // Load mods
                tw.WriteLine("Loading mods...");
                foreach (var mod in modsCatalog)
                {
                    tw.WriteLine("Loading " + mod.Item1);

                    var dll = Assembly.LoadFile(mod.Item1);
                    if(dll == null)
                    {
                        tw.WriteLine(">Failed to load " + mod.Item1);
                    }
                    else
                    {
                        tw.WriteLine(">Loading dll types:");
                        foreach(Type type in dll.ExportedTypes)
                        {
                            tw.WriteLine(">>Loading type:" + type.Name);

                            //actually load the type
                            if(typeof(IHp2BaseModStarter).IsAssignableFrom(type))
                            {
                                tw.WriteLine(">>>type is a starter, starting");
                                var c = Activator.CreateInstance(type);
                                (c as IHp2BaseModStarter).Start();
                            }
                        }
                    }
                }

                // Log
                tw.Flush();
            }
            catch (Exception e)
            {
                tw.WriteLine("EXCEPTION: " + e.Message);
                tw.Flush();
            }
        }

        /// <summary>
        /// Parses a config file line, return null if invalid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string[] ParseConfig(string str)
        {
            //find index of the comma, idk why but Hp2 doesn't like str.Split()
            string path = "";
            string prio = "";

            bool comma = false;
            foreach(var c in str)
            {
                if(c == ',')
                {
                    comma = true;
                }
                else if(comma)
                {
                    prio += c;
                }
                else
                {
                    path += c;
                }
            }

            if(!path.EndsWith(".dll")
               || prio == "")
            {
                return null;
            }
            return new string[] {path, prio};
        }
    }
}