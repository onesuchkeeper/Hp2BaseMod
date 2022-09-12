// Hp2BaseMod 2022, By OneSuchKeeper

using System.IO;
using UnityEngine;

namespace Hp2BaseMod.Utility
{
    public static class TextureUtility
    {
        public static Sprite SpriteFromPath(string path) => TextureToSprite(LoadFromPath(path), Vector2.zero);

        public static Texture2D LoadFromPath(string path) => LoadFromBytes(File.ReadAllBytes(path));

        public static Texture2D LoadFromBytes(byte[] bytes)
        {
            var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(bytes);

            return texture;
        }

        public static Texture2D Empty() => new Texture2D(0, 0, TextureFormat.ARGB32, false);

        public static Sprite TextureToSprite(Texture2D texture, Vector2 pivot) => Sprite.Create(texture,
                                                                                  new Rect(0, 0, texture.width, texture.height),
                                                                                  pivot);
    }
}
