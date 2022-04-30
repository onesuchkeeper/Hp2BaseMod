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

        public int GirlPageIndex = 0;
        public bool GirlAppNeedsRefresh = true;
        public static readonly int GirlsPerPage = 12;

        public int PairPageIndex = 0;
        public static readonly int PairsPerPage = 24;

        public int WarbrobeGirlPageIndex = 0;
        public bool WardropeNeedsRefresh = true;
        public static readonly int WarbrobeGirlsPerPage = 12;

        public int WarbrobeOutfitPageIndex = 0;
        public static readonly int WarbrobeOutfitsPerPage = 12;

        public int PhotoPageIndex = 0;
        public bool PhotoWindowNeedsRefresh = true;
        public static readonly int PhotossPerPage = 29;

        public bool SubscribedOpenEvent = false;

        public void Awake()
        {
            if (Instance != null) { throw new Exception("AssetHolder already exists"); }

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
