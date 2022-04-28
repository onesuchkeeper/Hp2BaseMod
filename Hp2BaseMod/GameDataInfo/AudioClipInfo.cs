// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.IO;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AudioClip
    /// </summary>
    public class AudioClipInfo : IGameDataInfo<AudioClip>
    {
        public string Path;

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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref AudioClip def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (Path == null)
            {
                if (insertStyle == InsertStyle.assignNull)
                {
                    def = null;
                }
            }
            else
            {
                if (IsExternal)
                {
                    if (File.Exists(Path))
                    {
                        byte[] fileBytes = File.ReadAllBytes(Path);
                        def =  WAV.LoadAudioClip(fileBytes, 0);

                        ModInterface.Instance.LogLine($"{(def == null ? "Failed to load" : "Loaded")} external {nameof(AudioClip)} { Path ?? null}");
                    }
                    else
                    {
                        ModInterface.Instance.LogLine($"Could not find {Path} to load {nameof(AudioClip)} from");
                    }
                }
                else
                {
                    def = (AudioClip)assetProvider.GetAsset(Path);
                }
            }
        }
    }
}
