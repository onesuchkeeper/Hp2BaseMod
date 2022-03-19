// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.IO;
using UnityEngine;
using Hp2BaseMod.Utility;
using Hp2BaseMod.ModLoader;
using UiSon.Attribute;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make an AudioClip
    /// </summary>
    public class AudioClipInfo
    {
        [UiSonTextEditUi]
        public string Path;

        [UiSonCheckboxUi]
        public bool IsExternal;

        public AudioClipInfo() { }

        public AudioClipInfo(string path,
                             bool isExternal)
        {
            Path = path;
            IsExternal = isExternal;
        }

        public AudioClipInfo(AudioClip audioClip, AssetProvider assetProvider)
        {
            if (audioClip == null) { throw new ArgumentNullException(nameof(audioClip)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            assetProvider.NameAndAddAsset(ref Path, audioClip);

            IsExternal = false;
        }

        public AudioClip ToAudioClip(AssetProvider assetProvider)
        {
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            if (Path == null)
            {
                return null;
            }
            else if (IsExternal)
            {
                if (File.Exists(Path))
                {
                    byte[] fileBytes = File.ReadAllBytes(Path);
                    return WAV.LoadAudioClip(fileBytes, 0);
                }
                return null;
            }
            else
            {
                return (AudioClip)assetProvider.GetAsset(Path);
            }
        }
    }
}
