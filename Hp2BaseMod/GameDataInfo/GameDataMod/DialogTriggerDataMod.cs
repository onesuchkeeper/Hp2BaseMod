// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a DialogTriggerDefinition
    /// </summary>
    [UiSonElement]
    public class DialogTriggerDataMod : DataMod, IGameDataMod<DialogTriggerDefinition>
    {
        // not used in game
        public EditorDialogTriggerTab? EditorTab;

        [UiSonSelectorUi(DefaultData.DialogTriggerForceTypeNullable_As_String)]
        public DialogTriggerForceType? ForceType;

        [UiSonElementSelectorUi(nameof(DialogTriggerDataMod), 0, null, "Id", DefaultData.DefaultDialogTriggerNames_Name, DefaultData.DefaultDialogTriggerIds_Name)]
        public int? ResponseTriggerDefinitionID;

        [UiSonTextEditUi]
        public int? Priority;

        [UiSonMemberElement]
        public List<DialogTriggerLineSetInfo> DialogLineSetInfos;

        public DialogTriggerDataMod() { }

        public DialogTriggerDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public DialogTriggerDataMod(int id,
                                    EditorDialogTriggerTab? editorTab,
                                    DialogTriggerForceType? forceType,
                                    int? responseTriggerDefinitionID,
                                    int? priority,
                                    List<DialogTriggerLineSetInfo> dialogLineSetInfos,
                                    InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            EditorTab = editorTab;
            ForceType = forceType;
            ResponseTriggerDefinitionID = responseTriggerDefinitionID;
            Priority = priority;
            DialogLineSetInfos = dialogLineSetInfos;
        }

        public DialogTriggerDataMod(DialogTriggerDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
        {
            EditorTab = def.editorTab;
            ForceType = def.forceType;
            ResponseTriggerDefinitionID = def.responseTriggerDefinition?.id ?? -1;
            Priority = def.priority;
            DialogLineSetInfos = def.dialogLineSets?.Select(x => new DialogTriggerLineSetInfo(x, assetProvider)).ToList();
        }

        public void SetData(DialogTriggerDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.editorTab, EditorTab);
            ValidatedSet.SetValue(ref def.forceType, ForceType);
            ValidatedSet.SetValue(ref def.priority, Priority);

            ValidatedSet.SetValue(ref def.responseTriggerDefinition, gameDataProvider.GetDialogTrigger(ResponseTriggerDefinitionID), InsertStyle);
            
            ValidatedSet.SetListValue(ref def.dialogLineSets, DialogLineSetInfos, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
