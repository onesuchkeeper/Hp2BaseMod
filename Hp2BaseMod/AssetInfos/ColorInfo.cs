// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using UnityEngine;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    [Serializable]
    public class ColorInfo
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public ColorInfo() { }

        public ColorInfo(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorInfo(Color color)
        {
            if (color == null) { throw new ArgumentNullException(nameof(color)); }

            R = color.r;
            G = color.g;
            B = color.b;
            A = color.a;
        }

        public Color ToColor()
        {
            return new Color(R, G, B, A);
        }
    }
}
