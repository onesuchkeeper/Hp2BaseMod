// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using System.IO;
using UnityEngine;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    [Serializable]
    public class SpriteInfo
    {
        public string Path;
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

        public Sprite ToSprite(AssetProvider assetProvider)
        {
            if (Path == null) { return null; };
            if (IsExternal)
            {
                if (File.Exists(Path))
                {
                    var fileData = File.ReadAllBytes(Path);
                    var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                    texture.LoadImage(fileData);

                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
                return null;
            }
            else
            {
                return (Sprite)assetProvider.GetAsset(Path);
            }
        }
    }
}
