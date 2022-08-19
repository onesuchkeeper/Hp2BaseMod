// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.IO;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    public class SpriteInfo : IGameDefinitionInfo<Sprite>
    {
        public string Path;

        public bool IsExternal;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpriteInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public SpriteInfo(Sprite def, AssetProvider assetProvider)
        {
            if (def == null) { return; }

            assetProvider.NameAndAddAsset(ref Path, def);
            IsExternal = false;
        }

        /// <inheritdoc/>
        public void SetData(ref Sprite def, GameDefinitionProvider _, AssetProvider assetProvider, InsertStyle insertStyle)
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
                        var texture = TextureUtility.LoadFromPath(Path);
                        def = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                        ModInterface.Log.LogLine($"{(def == null ? "Failed to load" : "Loaded")} external {nameof(Sprite)} {Path ?? null}");
                    }
                    else
                    {
                        ModInterface.Log.LogLine($"Could not find {Path} to load {nameof(Sprite)} from");
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
