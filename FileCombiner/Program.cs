using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hp2BaseMod.Utility;

namespace FileCombiner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileUtility.CombineFiles(@"C:\filejointest\combined.dat",
                                     new List<string>() { @"C:\filejointest\loc_bg_sim_hiddenwaterfall_0.png",
                                                          @"C:\filejointest\loc_bg_sim_hiddenwaterfall_1.png",
                                                          @"C:\filejointest\loc_bg_sim_hiddenwaterfall_2.png",
                                                          @"C:\filejointest\loc_bg_sim_hiddenwaterfall_3.png",
                                                          @"C:\filejointest\ui_location_icon_hiddenwaterfall.png",
                                                          @"C:\filejointest\33 Hidden Waterfall (Bonus).wav"});
        }
    }
}
