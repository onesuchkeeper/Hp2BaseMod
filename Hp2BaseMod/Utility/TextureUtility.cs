// Hp2BaseMod 2022, By OneSuchKeeper

using System.IO;
using UnityEngine;

namespace Hp2BaseMod.Utility
{
    public static class TextureUtility
    {
        public static Sprite SpriteFromPath(string path) => TextureToSprite(LoadFromPath(path));

        public static Texture2D LoadFromPath(string path)
        {
            var fileData = File.ReadAllBytes(path);
            var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(fileData);

            return texture;
        }

        public static Sprite TextureToSprite(Texture2D texture) => Sprite.Create(texture,
                                                                                  new Rect(0, 0, texture.width, texture.height),
                                                                                  new Vector2(texture.width / 2, texture.height / 2));
    }
}
