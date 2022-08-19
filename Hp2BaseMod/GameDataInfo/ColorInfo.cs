// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    public class ColorInfo : IGameDefinitionInfo<Color>
    {
        public float? R;

        public float? G;

        public float? B;

        public float? A;

        /// <summary>
        /// Parameterless Constructor
        /// </summary>
        public ColorInfo() { }

        /// <summary>
        /// Constructor with rgba parameters
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public ColorInfo(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public ColorInfo(Color color)
        {
            if (color == null) { throw new ArgumentNullException(nameof(color)); }

            R = color.r;
            G = color.g;
            B = color.b;
            A = color.a;
        }

        /// <inheritdoc/>
        public void SetData(ref Color def, GameDefinitionProvider _, AssetProvider __, InsertStyle ___)
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
