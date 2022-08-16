// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
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
        [UiSonTextEditUi]
        public string QuestionName;

        [UiSonTextEditUi]
        public string QuestionText;

        [UiSonTextEditUi]
        public List<string> QuestionAnswers;

        /// <inheritdoc/>
        public QuestionDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public QuestionDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        internal QuestionDataMod(QuestionDefinition def)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            QuestionName = def.questionName;
            QuestionText = def.questionText;
            QuestionAnswers = def.questionAnswers;
        }

        /// <inheritdoc/>
        public void SetData(QuestionDefinition def, GameDefinitionProvider _, AssetProvider __)
        {
            ValidatedSet.SetValue(ref def.questionName, QuestionName, InsertStyle);
            ValidatedSet.SetValue(ref def.questionText, QuestionText, InsertStyle);
            ValidatedSet.SetListValue(ref def.questionAnswers, QuestionAnswers, InsertStyle);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;
        }
    }
}
