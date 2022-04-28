// Hp2BaseMod 2021, By OneSuchKeeper

using System.Collections.Generic;
using UnityEngine;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// Holding space for unity objects like audio files, prefabs and images from the game while setting up data mods
    /// </summary>
    public class AssetProvider
    {
        private Dictionary<string, Object> Assets;

        public AssetProvider(Dictionary<string, Object> assets)
        {
            Assets = assets;
        }

        public void AddAsset(string identifier, Object asset)
        {
            if (identifier == null) { return; }
            if (!Assets.ContainsKey(identifier))
            {
                Assets.Add(identifier, asset);
            }
        }

        public object GetAsset(string identifier)
        {
            if (identifier == null) { return null; }
            if (Assets.ContainsKey(identifier))
            {
                ModInterface.Instance.LogLine($"Loaded internal asset { identifier }");
                return Assets[identifier];
            }
            else
            {
                ModInterface.Instance.LogLine($"Failed to load internal asset { identifier ?? "null"}");
            }
            return null;
        }

        public void NameAndAddAsset(ref string target, Object unityObj)
        {
            if (unityObj != null)
            {
                target = unityObj.name;
                AddAsset(target, unityObj);
            }
        }

        #region Load
        public void Load(AbilityDefinition def) => def?.steps?.ForEach(x => Load(x));

        public void Load(AbilityStepSubDefinition def) => Load(def?.audioKlip);

        public void Load(AudioKlip def) => Load(def?.clip);

        public void Load(AudioClip def) => AddAsset(def?.name, def);

        public void Load(CutsceneDefinition def) => def?.steps?.ForEach(x => Load(x));

        public void Load(CutsceneStepSubDefinition def)
        {
            AddAsset(def?.specialStepPrefab?.name, def?.specialStepPrefab);
            AddAsset(def?.windowPrefab?.name, def?.windowPrefab);
            AddAsset(def?.emitterBehavior?.name, def?.emitterBehavior);
            AddAsset(def?.bannerTextPrefab?.name, def?.bannerTextPrefab);

            Load(def?.audioKlip);
            Load(def?.dialogLine);
            Load(def?.logicAction);

            def?.dialogOptions?.ForEach(x => Load(x));
            def?.branches?.ForEach(x => Load(x));
        }

        public void Load(DialogLine def)
        {
            Load(def?.yuriAudioClip);
            Load(def?.audioClip);
        }

        public void Load(LogicAction def) => Load(def?.backgroundMusic);

        public void Load(CutsceneDialogOptionSubDefinition def) => def?.steps?.ForEach(x => Load(x));

        public void Load(CutsceneBranchSubDefinition def) => def?.steps?.ForEach(x => Load(x));

        public void Load(DialogTriggerDefinition def) => def?.dialogLineSets?.ForEach(x => Load(x));

        public void Load(DialogTriggerLineSet def) => def?.dialogLines?.ForEach(x => Load(x));

        public void Load(GirlDefinition def)
        {
            AddAsset(def?.specialEffectPrefab?.name, def?.specialEffectPrefab);

            Load(def?.cellphonePortrait);
            Load(def?.cellphonePortraitAlt);
            Load(def?.cellphoneHead);
            Load(def?.cellphoneHeadAlt);
            Load(def?.cellphoneMiniHead);
            Load(def?.cellphoneMiniHeadAlt);
            def?.parts?.ForEach(x => Load(x, def?.id));
        }

        public void Load(Sprite def) => AddAsset(def?.name, def);

        public void Load(GirlPartSubDefinition def, int? girlId)
        {
            if (def?.sprite != null && girlId != null)
            {
                AddAsset($"{girlId.Value}_{(def.sprite.name)}", def?.sprite);
            }
        }

        public void Load(EnergyDefinition def)
        {
            def?.burstSprites?.ForEach(x => Load(x));
            def?.trailSprites?.ForEach(x => Load(x));
            def?.splashSprites?.ForEach(x => Load(x));
            def?.surgeSprites?.ForEach(x => Load(x));

            AddAsset(def?.textMaterial?.name, def?.textMaterial);
        }

        public void Load(ItemDefinition def) => Load(def?.itemSprite);

        public void Load(LocationDefinition def)
        {
            Load(def?.bgMusic);
            Load(def?.finderLocationIcon);
            def?.backgrounds?.ForEach(x => Load(x));
            def?.arriveBundleList?.ForEach(x => Load(x));
            def?.departBundleList?.ForEach(x => Load(x));
        }

        public void Load(LogicBundle def) => def?.actions?.ForEach(x => Load(x));

        public void Load(PhotoDefinition def)
        {
            def?.bigPhotoImages?.ForEach(x => Load(x));
            def?.thumbnailImages?.ForEach(x => Load(x));
        }

        public void Load(TokenDefinition def)
        {
            Load(def?.tokenSprite);
            Load(def?.overSprite);
            Load(def?.altTokenSprite);
            Load(def?.altOverSprite);
            Load(def?.sfxMatch);
        }

        #endregion Load
    }
}
