using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Hp2BaseMod.Ui
{
    public class Hp2ButtonWrapper
    {
        private readonly static FieldInfo _buttonBehavior_overTransitions = AccessTools.Field(typeof(ButtonBehavior), "_overTransitions");
        private readonly static FieldInfo _buttonBehavior_downTransitions = AccessTools.Field(typeof(ButtonBehavior), "_downTransitions");
        private readonly static FieldInfo _buttonBehavior_disableTransitions = AccessTools.Field(typeof(ButtonBehavior), "_disableTransitions");

        public GameObject GameObject => _gameObject;
        private readonly GameObject _gameObject;

        public ButtonBehavior ButtonBehavior => _buttonBehavior;
        private readonly ButtonBehavior _buttonBehavior;

        public CanvasRenderer CanvasRenderer => _canvasRenderer;
        private readonly CanvasRenderer _canvasRenderer;

        public RectTransform RectTransform => _rectTransform;
        private readonly RectTransform _rectTransform;

        public Image Image => _image;
        private readonly Image _image;

        public Hp2ButtonWrapper(GameObject gameObject,
                                CanvasRenderer canvasRenderer,
                                RectTransform rectTransform,
                                Image image,
                                ButtonBehavior buttonBehavior)
        {
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
            _canvasRenderer = canvasRenderer ?? throw new ArgumentNullException(nameof(canvasRenderer));
            _rectTransform = rectTransform ?? throw new ArgumentNullException(nameof(rectTransform));
            _image = image ?? throw new ArgumentNullException(nameof(image));
            _buttonBehavior = buttonBehavior ?? throw new ArgumentNullException(nameof(buttonBehavior));
        }

        public void Destroy()
        {
            _gameObject.transform.parent = null;
            GameObject.Destroy(_gameObject);
        }

        public static Hp2ButtonWrapper MakeCellphoneButton(string name, Sprite sprite, Sprite sprite_over, AudioKlip pressedSfx)
        {
            var gameObject = new GameObject(name);
            var canvasRenderer = gameObject.AddComponent<CanvasRenderer>();
            var rectTransform = gameObject.AddComponent<RectTransform>();
            var image = gameObject.AddComponent<Image>();

            image.sprite = sprite;
            image.SetNativeSize();

            var buttonBehavior = gameObject.AddComponent<ButtonBehavior>();

            buttonBehavior.pressedSfx = pressedSfx;

            var overTransitions = _buttonBehavior_overTransitions.GetValue(buttonBehavior) as List<ButtonStateTransition>;
            overTransitions.Add(new ButtonStateTransition(new ButtonStateTransitionDef()
            {
                type = ButtonStateTransitionType.SPRITE,
                sprite = sprite_over,
                imageTarget = image
            }, buttonBehavior));

            var downTransitions = _buttonBehavior_downTransitions.GetValue(buttonBehavior) as List<ButtonStateTransition>;
            downTransitions.Add(new ButtonStateTransition(new ButtonStateTransitionDef()
            {
                type = ButtonStateTransitionType.SCALE,
                val = 0.88f,
                rectTransformTarget = rectTransform
            }, buttonBehavior));

            var disableTransitions = _buttonBehavior_disableTransitions.GetValue(buttonBehavior) as List<ButtonStateTransition>;
            disableTransitions.Add(new ButtonStateTransition(new ButtonStateTransitionDef()
            {
                type = ButtonStateTransitionType.ALPHA,
                val = 0.25f,
                imageTarget = image
            }, buttonBehavior));

            return new Hp2ButtonWrapper(gameObject, canvasRenderer, rectTransform, image, buttonBehavior);
        }
    }
}
