using System.IO;
using Hp2BaseMod.Interface;

namespace Hp2SampleMod
{
    public class SampleMod : IHp2BaseModStarter
    {
        public void Start()
        {
            TextWriter tw = File.CreateText("Hp2SampleModOutput.txt");
            tw.WriteLine("Hello world!");
            tw.Flush();
        }
    }
}
