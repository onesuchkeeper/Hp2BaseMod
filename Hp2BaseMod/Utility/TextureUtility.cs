// Hp2BaseMod 2022, By OneSuchKeeper

using System.IO;
using UnityEngine;

namespace Hp2BaseMod.Utility
{
    public static class TextureUtility
    {
        public static Texture2D LoadFromPath(string path)
        {
            var fileData = File.ReadAllBytes(path);
            var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(fileData);

            return texture;
        }
    }
}
