// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Interfaces;
using System;

namespace DataModEditor
{
    public class DollPartVm : BaseVM
    {
        public string Path { get; set; } = "null";

        //validate these as doubles
        public string X { get; set; } = "null";
        public string Y { get; set; } = "null";

        public string Name => _name;
        private string _name;

        // make another constructor for populate
        public DollPartVm(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
