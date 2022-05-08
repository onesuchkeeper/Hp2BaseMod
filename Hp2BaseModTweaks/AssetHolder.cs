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

                //var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "hp2basemodtweaksprefabs");

                //if (!File.Exists(path)) { throw new Exception($"File doesn't exist: {path}"); }

                //var bundle = AssetBundle.LoadFromFile(path);

                //if (bundle == null)
                //{
                //    throw new Exception("hp2basemodtweaksprefabs failed to load from: " + path);
                //}

                //Assets = bundle.LoadAllAssets();

                //var newLogoTexture = TextureUtility.LoadFromPath("mods/Hp2BaseModTweaks/Images/ui_photo_album_slot.png");

                //EmptyPhotoSlot = Sprite.Create(newLogoTexture, new Rect(0, 0, 312,182), new Vector2(156, 91));

                foreach (var sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (Sprites.ContainsKey(sprite.name))
                    {
                        Sprites[sprite.name] = sprite;
                        ModInterface.Instance.LogLine($"Found {sprite.name}");
                    }
                }

                foreach (var clip in Resources.FindObjectsOfTypeAll<AudioClip>())
                {
                    if (AudioClips.ContainsKey(clip.name))
                    {
                        AudioClips[clip.name] = clip;
                        ModInterface.Instance.LogLine($"Found {clip.name}");
                    }
                }
            }
        }
    }
}
