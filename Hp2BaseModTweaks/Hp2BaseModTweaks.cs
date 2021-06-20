// Hp2BaseMod 2021

using System;
using HarmonyLib;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Hp2BaseMod;

namespace Hp2NudeOutfitMod
{
    public class Hp2BaseModTweaks : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            try
            {
                var harmony = new Harmony("Hp2BaseModUtility.Hp2BaseMod");

                var mOrigional = AccessTools.Constructor(typeof(GameData));
                var mPostfix = SymbolExtensions.GetMethodInfo(() => MyPostfix(null));

                harmony.Patch(mOrigional, null, new HarmonyMethod(mPostfix));
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION Hp2BaseModTweaks: " + e.Message);
            }
        }

        public static void MyPostfix(GameData __instance)
        {
            var path = "mods/Hp2BaseModTweaks/hp2basemodtweaksassets";

            if (!File.Exists(path)) { throw new Exception("Asset file doesn't exsist :("); }

            var assetBundle = AssetBundle.LoadFromFile(path);

            var newLogo = assetBundle.LoadAsset<Texture2D>("ui_titlescreen_logo_modded");

            var allImages = Resources.FindObjectsOfTypeAll<Image>();

            Harmony.DEBUG = true;
            
            for (int i = 0; i < allImages.Length; i++)
            {
                if (allImages[i].name == "Logo")
                {
                    allImages[i].sprite = Sprite.Create(newLogo, allImages[i].sprite.rect, allImages[i].sprite.pivot);
                    break;
                }
            }
        }
    }
}
