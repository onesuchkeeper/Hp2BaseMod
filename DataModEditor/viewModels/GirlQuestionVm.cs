// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataModEditor
{
    public class GirlQuestionVm
    {
        public string Choice1 { get; set; } = "null";
        public string Choice1Alt { get; set; } = "null";
        public int Responce1 { get; set; } = 0;
        public string Choice2 { get; set; } = "null";
        public string Choice2Alt { get; set; } = "null";
        public int Responce2 { get; set; } = 0;
        public string Choice3 { get; set; } = "null";
        public string Choice3Alt { get; set; } = "null";
        public int Responce3 { get; set; } = 0;

        public IEnumerable<string> ResponceOptions { get; } = Default.DialogTriggers.Select(x => x.Value);

        private ObservableCollection<GirlQuestionVm> _parent;

        public GirlQuestionVm(ObservableCollection<GirlQuestionVm> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public void Remove()
        {
            _parent.Remove(this);
        }

        public void Populate(GirlQuestionSubDefinition girlQuestion)
        {
            //There are always 3, except now that I'm thinking about it there could maybe be more, shit. For now there's 3 ok?
            var answers = girlQuestion.answers[0];
            Choice1 = answers.answerText;
            Choice1Alt = answers.answerTextAlt;
            Responce1 = answers.responseIndex;//fix this

            answers = girlQuestion.answers[1];
            Choice2 = answers.answerText ?? "null";
            Choice2Alt = answers.answerTextAlt ?? "null";
            Responce2 = answers.responseIndex;//fix this

            answers = girlQuestion.answers[2];
            Choice3 = answers.answerText ?? "null";
            Choice3Alt = answers.answerTextAlt ?? "null";
            Responce3 = answers.responseIndex;//fix this when dialog trigger managher is a thing
        }
    }
}
