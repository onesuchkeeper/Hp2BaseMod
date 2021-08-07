// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataMods.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a QuestionDefinition
    /// </summary>
    [Serializable]
    public class QuestionDataMod : DataMod<QuestionDefinition>
    {
        public string QuestionName;
        public string QuestionText;
        public List<string> QuestionAnswers;

        public QuestionDataMod() { }

        public QuestionDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public QuestionDataMod(int id,
                               string questionName,
                               string questionText,
                               List<string> questionAnswers,
                               bool isAdditive = false)
            : base(id, isAdditive)
        {
            Id = id;
            QuestionName = questionName;
            QuestionText = questionText;
            QuestionAnswers = questionAnswers;
        }

        public QuestionDataMod(QuestionDefinition def)
            : base(def.id, false)
        {
            Id = def.id;
            QuestionName = def.questionName;
            QuestionText = def.questionText;
            QuestionAnswers = def.questionAnswers;
        }

        public override void SetData(QuestionDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullSet(ref def.questionName, QuestionName);
            Access.NullSet(ref def.questionText, QuestionText);
            Access.NullSet(ref def.questionAnswers, QuestionAnswers);
        }
    }
}
