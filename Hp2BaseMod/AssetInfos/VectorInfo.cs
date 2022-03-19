// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using UnityEngine;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    [Serializable]
    public class VectorInfo
    {
        public float Xpos;
        public float Ypos;

        public VectorInfo() { }

        public VectorInfo(float x, float y)
        {
            Xpos = x;
            Ypos = y;
        }

        public VectorInfo(Vector2 vector)
        {
            if (vector == null) { throw new ArgumentNullException(nameof(vector)); }

            Xpos = vector.x;
            Ypos = vector.y;
        }

        public Vector2 ToVector2()
        {
            var newVector = new Vector2();

            newVector.x = Xpos;
            newVector.y = Ypos;

            return newVector;
        }
    }
}
