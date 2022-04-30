// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    public class ColorInfo : IGameDataInfo<Color>
    {
        [UiSonSliderUi(0, 255, 0)]
        public float? R;

        [UiSonSliderUi(0, 255, 0)]
        public float? G;

        [UiSonSliderUi(0, 255, 0)]
        public float? B;

        [UiSonSliderUi(0, 255, 0)]
        public float? A;

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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref Color def, GameDataProvider _, AssetProvider __, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<Color>();
            }

            ValidatedSet.SetValue(ref def.r, R);
            ValidatedSet.SetValue(ref def.g, G);
            ValidatedSet.SetValue(ref def.b, B);
            ValidatedSet.SetValue(ref def.a, A);
        }
    }
}
