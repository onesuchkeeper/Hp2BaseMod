// Hp2BaseModTweaks 2021, By OneSuchKeeper

using Hp2BaseMod;
using System.Collections.Generic;
using UnityEngine;

namespace Hp2BaseModTweaks
{
    public class AssetHolder
    {
        public static AssetHolder Instance;

        public readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>
        {
            { "ui_photo_album_slot", null },
            { "ui_photo_button_left", null },
            { "ui_photo_button_right", null },
            { "ui_app_setting_arrow_left", null },
            { "ui_app_setting_arrow_left_over", null },
            { "ui_app_setting_arrow_right", null },
            { "ui_app_setting_arrow_right_over", null }
        };

        public readonly Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>
        {
            { "sfx_phone_app_button_pressed", null }
        };

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                foreach (var sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (Sprites.ContainsKey(sprite.name))
                    {
                        Sprites[sprite.name] = sprite;
                        ModInterface.Log.LogLine($"Found {sprite.name}");
                    }
                }

                foreach (var clip in Resources.FindObjectsOfTypeAll<AudioClip>())
                {
                    if (AudioClips.ContainsKey(clip.name))
                    {
                        AudioClips[clip.name] = clip;
                        ModInterface.Log.LogLine($"Found {clip.name}");
                    }
                }
            }
        }
    }
}
