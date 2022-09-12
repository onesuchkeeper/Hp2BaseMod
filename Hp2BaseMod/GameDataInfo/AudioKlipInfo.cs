// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    public class AudioKlipInfo : IGameDefinitionInfo<AudioKlip>
    {
        public IGameDefinitionInfo<AudioClip> AudioClipInfo;

        public float? Volume;

        /// <summary>
        /// Constructor
        /// </summary>
        public AudioKlipInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public AudioKlipInfo(AudioKlip def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            Volume = def.volume;
            if (def.clip != null) { AudioClipInfo = new AudioClipInfo(def.clip, assetProvider); }
        }

        /// <inheritdoc/>
        public void SetData(ref AudioKlip def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<AudioKlip>();
            }
            ValidatedSet.SetValue(ref def.volume, Volume);
            ValidatedSet.SetValue(ref def.clip, AudioClipInfo, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
