// Hp2BaseModTweaks 2022, By OneSuchKeeper

using DG.Tweening;
using HarmonyLib;
using Hp2BaseMod.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
    [HarmonyPatch(typeof(UiTitleCanvas), "OnInitialAnimationComplete")]
    public static class TitleCanvasPatch
    {
        public static void Prefix(UiTitleCanvas __instance)
        {
            if (Common.LogoPaths.Count > 0)
            {
                var path = Common.LogoPaths[Random.Range(0, Common.LogoPaths.Count)];

                var coverArt = AccessTools.Field(typeof(UiTitleCanvas), "coverArt").GetValue(__instance) as UiCoverArt;

                var LogoImage = coverArt.rectTransform.GetChild(5).GetComponent<Image>();

                var logoTexture = TextureUtility.LoadFromPath(path);

                LogoImage.sprite = TextureUtility.TextureToSprite(logoTexture, new Vector2(logoTexture.width / 2, logoTexture.height / 2));

                LogoImage.rectTransform.DOSpiral(0.7f, null, SpiralMode.Expand, 5).Play();
                LogoImage.rectTransform.DOShakeAnchorPos(0.7f, 40, 15, 100).Play();

                Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(9).sfxMatch);
                Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(7).sfxMatch);
                Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(5).sfxMatch);
            }
        }
    }
}
