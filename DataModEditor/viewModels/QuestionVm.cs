// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;

namespace DataModEditor
{
    public class QuestionVm
    {
        public string Name { get; private set; }
        public IEnumerable<string> Answers;

        public QuestionVm(string name, IEnumerable<string> answers)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Answers = answers ?? throw new ArgumentNullException(nameof(answers));
        }
    }
}
