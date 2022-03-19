// Hp2Sample 2021, By OneSuchKeeper

using System;
using System.IO;
using Hp2BaseMod;

namespace Hp2SampleMod
{
    public class SampleMod : IHp2BaseModStarter
    {
        public void Start(ModInterface modInterface)
        {
            modInterface.LogLine("Hello world!");

            try
            {
                TextWriter tw = File.CreateText("Hp2SampleModOutput.txt");
                tw.WriteLine("Hello world!");
                tw.Flush();
                tw.Close();
                modInterface.LogLine("Done!");
            }
            catch (Exception e)
            {
                modInterface.LogLine("Sample Mod Experianced an error: " + e.Message);
            }
        }
    }
}
