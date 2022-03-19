// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using DataModEditor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataModEditor
{
    public class GirlQuestionVm : NPCBase
    {
        public string Dialog
        {
            get => _dialog;
            set
            {
                if (_dialog != value)
                {
                    _dialog = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _dialog = "null";

        public string AudioPath
        {
            get => _audioPath;
            set
            {
                if (_audioPath != value)
                {
                    _audioPath = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _audioPath = "null";

        public IEnumerable<QuestionChoiceVm> Choices => _choices;
        private QuestionChoiceVm[] _choices =
        {
            new QuestionChoiceVm("Correct"),
            new QuestionChoiceVm("Wrong A"),
            new QuestionChoiceVm("Wrong B")
        };

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
            for (int i = 0; i < 3; i++)
            {
                _choices[i].Text = girlQuestion.answers[i].answerText;
                _choices[i].AltText = girlQuestion.answers[i].answerTextAlt;
                _choices[i].Responce = girlQuestion.answers[i].responseIndex;
            }
        }
    }
}
