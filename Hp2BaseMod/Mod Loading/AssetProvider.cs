// Hp2BaseMod 2021, By OneSuchKeeper

using System.Collections.Generic;
using UnityEngine;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// Holding space for unity objects like audio files, prefabs and images from the game while setting up data mods
    /// </summary>
    public class AssetProvider
    {
        private Dictionary<string, Object> Assets;

        public AssetProvider(Dictionary<string, Object> assets)
        {
            Assets = assets;
        }

        public void AddAsset(string identifier, Object asset)
        {
            if (identifier == null) { return; }
            if (!Assets.ContainsKey(identifier))
            {
                Assets.Add(identifier, asset);
            }
        }

        public object GetAsset(string identifier)
        {
            if (identifier == null) { return null; }
            if (Assets.ContainsKey(identifier))
            {
                return Assets[identifier];
            }
            return null;
        }

        public void NameAndAddAsset(ref string target, Object unityObj)
        {
            if (unityObj != null)
            {
                target = unityObj.name;
                AddAsset(target, unityObj);
            }
        }
    }
}
