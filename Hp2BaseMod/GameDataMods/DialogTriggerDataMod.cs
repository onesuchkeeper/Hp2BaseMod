// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a DialogTriggerDefinition
    /// </summary>
    [Serializable]
    public class DialogTriggerDataMod : DataMod<DialogTriggerDefinition>
    {
        public EditorDialogTriggerTab? EditorTab;
        public DialogTriggerForceType? ForceType;
        public int? ResponseTriggerDefinitionID;
        public int? Priority;
        public List<DialogTriggerLineSetInfo> DialogLineSetInfos;

        public DialogTriggerDataMod() { }

        public DialogTriggerDataMod(int id,
                                    EditorDialogTriggerTab? editorTab, 
                                    DialogTriggerForceType? forceType, 
                                    int? responseTriggerDefinitionID, 
                                    int? priority, 
                                    List<DialogTriggerLineSetInfo> dialogLineSetInfos,
                                    bool isAdditive = false)
            : base(id, isAdditive)
        {
            EditorTab = editorTab;
            ForceType = forceType;
            ResponseTriggerDefinitionID = responseTriggerDefinitionID;
            Priority = priority;
            DialogLineSetInfos = dialogLineSetInfos;
        }

        public DialogTriggerDataMod(DialogTriggerDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            EditorTab = def.editorTab;
            ForceType = def.forceType;
            ResponseTriggerDefinitionID = def.responseTriggerDefinition?.id ?? -1;
            Priority = def.priority;
            DialogLineSetInfos = def.dialogLineSets?.Select(x => new DialogTriggerLineSetInfo(x, assetProvider)).ToList();
        }

        public override void SetData(DialogTriggerDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.editorTab, EditorTab);
            Access.NullableSet(ref def.forceType, ForceType);
            Access.NullableSet(ref def.priority, Priority);

            if (ResponseTriggerDefinitionID.HasValue) { def.responseTriggerDefinition = gameData.DialogTriggers.Get(ResponseTriggerDefinitionID.Value); }

            SetDialogTriggerLineSets(ref def.dialogLineSets, DialogLineSetInfos, gameData, assetProvider);
        }
    }
}
