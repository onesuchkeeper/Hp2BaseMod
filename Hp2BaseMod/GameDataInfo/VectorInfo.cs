// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    public class VectorInfo : IGameDataInfo<Vector2>
    {
        public float? Xpos;
        public float? Ypos;

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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref Vector2 def, GameDataProvider _, AssetProvider __, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<Vector2>();
            }

            ValidatedSet.SetValue(ref def.x, Xpos);
            ValidatedSet.SetValue(ref def.y, Ypos);
        }
    }
}
