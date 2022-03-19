// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace DataModEditor
{
    public class QuestionChoiceVm
    {
        public string Text { get; set; }
        public string AltText { get; set; }
        public int Responce { get; set; }
        public string Name { get; private set; }
        public QuestionChoiceVm(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
