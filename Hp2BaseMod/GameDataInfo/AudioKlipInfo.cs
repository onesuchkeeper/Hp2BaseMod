// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    public class AudioKlipInfo : IGameDataInfo<AudioKlip>
    {
        [UiSonMemberElement]
        public AudioClipInfo AudioClipInfo;

        [UiSonSliderUi(0, 1, 3)]
        public float? Volume;

        public AudioKlipInfo() { }

        public AudioKlipInfo(AudioClipInfo audioClipInfo,
                             float volume)
        {
            AudioClipInfo = audioClipInfo;
            Volume = volume;
        }

        public AudioKlipInfo(AudioKlip audioKlip, AssetProvider assetProvider)
        {
            if (audioKlip == null) { throw new ArgumentNullException(nameof(audioKlip)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            Volume = audioKlip.volume;
            if (audioKlip.clip != null) { AudioClipInfo = new AudioClipInfo(audioKlip.clip, assetProvider); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref AudioKlip def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for an audio klip");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<AudioKlip>();
            }
            ValidatedSet.SetValue(ref def.volume, Volume);
            ValidatedSet.SetValue(ref def.clip, AudioClipInfo, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
