// Hp2BaseModTweaks 2022, By OneSuchKeeper

using DG.Tweening;
using HarmonyLib;
using Hp2BaseMod.Utility;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
    [HarmonyPatch(typeof(UiTitleCanvas), "OnInitialAnimationComplete")]
    public static class ChangeLogoPatch
    {
        public static void Prefix(UiTitleCanvas __instance)
        {
            var coverArt = AccessTools.Field(typeof(UiTitleCanvas), "coverArt").GetValue(__instance) as UiCoverArt;

            var oldLogoImage = coverArt.rectTransform.GetChild(5).GetComponent<Image>();

            oldLogoImage.sprite = TextureUtility.SpriteFromPath("mods/Hp2BaseModTweaks/Images/logo.png");

            oldLogoImage.rectTransform.DOSpiral(0.7f, null, SpiralMode.Expand, 5).Play();
            oldLogoImage.rectTransform.DOShakeAnchorPos(0.7f, 40, 15, 100).Play();

            //Game.Manager.Audio.Play(AudioCategory.SOUND, AssetHolder.Instance.AudioClips["sfx_puzzle_stamp_success"]);

            Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(9).sfxMatch);
            Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(7).sfxMatch);
            Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Data.Tokens.Get(5).sfxMatch);
        }
    }
}
