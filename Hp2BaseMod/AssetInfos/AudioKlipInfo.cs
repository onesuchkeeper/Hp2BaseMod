﻿// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    public class AudioKlipInfo
    {
        [UiSonMemberClass]
        public AudioClipInfo AudioClipInfo;

        [UiSonTextEditUi]
        public float Volume;

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

        public AudioKlip ToAudioKlip(AssetProvider assetProvider)
        {
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            var newKlip = new AudioKlip();

            newKlip.volume = Volume;
            if (assetProvider != null) { newKlip.clip = AudioClipInfo.ToAudioClip(assetProvider); }

            return newKlip;
        }
    }
}
