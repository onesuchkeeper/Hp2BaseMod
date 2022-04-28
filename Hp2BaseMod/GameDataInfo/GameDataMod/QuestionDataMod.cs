// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a QuestionDefinition
    /// </summary>
    [UiSonElement]
    public class QuestionDataMod : DataMod, IGameDataMod<QuestionDefinition>
    {
        public string QuestionName;
        public string QuestionText;
        public List<string> QuestionAnswers;

        public QuestionDataMod() { }

        public QuestionDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public QuestionDataMod(int id,
                               string questionName,
                               string questionText,
                               List<string> questionAnswers,
                               InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            Id = id;
            QuestionName = questionName;
            QuestionText = questionText;
            QuestionAnswers = questionAnswers;
        }

        public QuestionDataMod(QuestionDefinition def)
            : base(def.id, InsertStyle.replace, def.name)
        {
            Id = def.id;
            QuestionName = def.questionName;
            QuestionText = def.questionText;
            QuestionAnswers = def.questionAnswers;
        }

        public void SetData(QuestionDefinition def, GameDataProvider _, AssetProvider __, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.questionName, QuestionName, insertStyle);
            ValidatedSet.SetValue(ref def.questionText, QuestionText, insertStyle);
            ValidatedSet.SetListValue(ref def.questionAnswers, QuestionAnswers, insertStyle);
        }
    }
}
