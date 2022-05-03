// Hp2BaseModTweaks 2021, By OneSuchKeeper

using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    public class AssetHolder
    {
        public static AssetHolder Instance;

        public UnityEngine.Object[] Assets;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "hp2basemodtweaksprefabs");

                if (!File.Exists(path)) { throw new Exception($"File doesn't exist: {path}"); }

                var bundle = AssetBundle.LoadFromFile(path);

                if (bundle == null)
                {
                    throw new Exception("hp2basemodtweaksprefabs failed to load from: " + path);
                }

                Assets = bundle.LoadAllAssets();
            }
        }
    }
}
