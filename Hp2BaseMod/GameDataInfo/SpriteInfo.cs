// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using System;
using System.IO;
using UiSon.Attribute;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    public class SpriteInfo : IGameDataInfo<Sprite>
    {
        [UiSonTextEditUi]
        public string Path;

        [UiSonCheckboxUi]
        public bool IsExternal;

        public SpriteInfo() { }

        public SpriteInfo(string path,
                          bool isExternal)
        {
            Path = path;
            IsExternal = isExternal;
        }

        public SpriteInfo(Sprite sprite, AssetProvider assetProvider)
        {
            if (sprite == null) { return; }

            assetProvider.NameAndAddAsset(ref Path, sprite);
            IsExternal = false;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref Sprite def, GameDataProvider _, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (Path == null)
            {
                if (insertStyle == InsertStyle.assignNull)
                {
                    def = null;
                }
            }
            else
            {
                if (IsExternal)
                {
                    if (File.Exists(Path))
                    {
                        var fileData = File.ReadAllBytes(Path);
                        var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                        texture.LoadImage(fileData);

                        def = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                        ModInterface.Instance.LogLine($"{(def == null ? "Failed to load" : "Loaded")} external {nameof(Sprite)} { Path ?? null}");
                    }
                    else
                    {
                        ModInterface.Instance.LogLine($"Could not find {Path} to load {nameof(Sprite)} from");
                    }
                }
                else
                {
                    def = (Sprite)assetProvider.GetAsset(Path);
                }
            }
        }
    }
}
