// Hp2BaseMod 2022, By OneSuchKeeper

using Hp2BaseMod.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseModTweaks
{
    internal class CellphoneButton : UnityButtonWrapper
    {
        public CellphoneButton(string name, Sprite sprite, Sprite overSprite, AudioClip clickSound)
            :base (name)
        {
            var image = _gameObject.AddComponent<Image>();
            image.sprite = sprite;
            _button.targetGraphic = image;
            _button.onClick.AddListener(() =>
            {
                Game.Manager.Audio.Play(AudioCategory.SOUND, clickSound);
            });

            _button.transition = Selectable.Transition.SpriteSwap;

            _button.spriteState = new SpriteState()
            {
                pressedSprite = sprite,
                selectedSprite = sprite,
                disabledSprite = sprite,
                highlightedSprite = overSprite,
            };

            _button.transform.localScale = new Vector3(0.35f,0.35f);
        }
    }
}
