// Hp2Sample 2021, By OneSuchKeeper

using System.IO;
using Hp2BaseMod;

namespace Hp2SampleMod
{
    public class SampleMod : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            TextWriter tw = File.CreateText("Hp2SampleModOutput.txt");
            tw.WriteLine("Hello world!");
            tw.Flush();
        }
    }
}
