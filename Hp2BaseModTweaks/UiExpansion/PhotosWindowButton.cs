// Hp2BaseMod 2022, By OneSuchKeeper

using Hp2BaseMod.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
    internal class PhotosWindowButton : UnityButtonWrapper
    {
        public PhotosWindowButton(string name, Sprite sprite, AudioClip clickSound)
            :base (name)
        {
            var image = _gameObject.AddComponent<Image>();
            image.sprite = sprite;
            _button.targetGraphic = image;
            _button.onClick.AddListener(() =>
            {
                Game.Manager.Audio.Play(AudioCategory.SOUND, clickSound);
            });

            var over = new Color(255, 255, 255, 180);
            var normal = new Color(255, 255, 255, 100);

            _button.transition = Selectable.Transition.ColorTint;

            var colorBlock = new ColorBlock()
            {
                pressedColor = normal,
                selectedColor = normal,
                disabledColor = normal,
                normalColor = normal,
                highlightedColor = over,
                colorMultiplier = 1f,
                fadeDuration = 0.3f
            };
            
            _button.colors = colorBlock;
            _button.image.color = normal;

            _button.transform.localScale = new Vector3(0.5f,0.5f);
        }
    }
}
