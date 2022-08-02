// Hp2BaseMod 2022, By OneSuchKeeper

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hp2BaseMod.Ui
{
    /// <summary>
    /// Holds a <see cref="GameObject"/> with <see cref="EventTrigger"/> and <see cref="Button"/>
    /// components. Triggers call thair respective handlers on the button.
    /// </summary>
    public abstract class UnityButtonWrapper
    {
        protected GameObject _gameObject;
        protected Button _button;
        
        public UnityButtonWrapper(string name)
        {
            _gameObject = new GameObject(name);

            var eventTrigger = _gameObject.AddComponent<EventTrigger>();
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.PointerDown,
                                                       (data) => { _button.OnPointerDown((PointerEventData)data); }));
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.PointerEnter,
                                                       (data) => { _button.OnPointerEnter((PointerEventData)data); }));
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.PointerExit,
                                                       (data) => { _button.OnPointerExit((PointerEventData)data); }));
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.PointerUp,
                                                       (data) => { _button.OnPointerUp((PointerEventData)data); }));
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.Submit,
                                                       (data) => { }));
            eventTrigger.triggers.Add(MakeTriggerEntry(EventTriggerType.Cancel,
                                                       (data) => { }));

            _button = _gameObject.AddComponent<Button>();
            _button.interactable = true;
            _button.navigation = new Navigation() { mode = Navigation.Mode.None };
        }

        private EventTrigger.Entry MakeTriggerEntry(EventTriggerType type, UnityAction<BaseEventData> func)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(func);
            return entry;
        }

        public void AddClickListener(UnityAction call) => _button.onClick.AddListener(call);

        public void Show()
        {
            _gameObject.SetActive(true);
            _button.transform.SetAsLastSibling();
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }

        public void SetPostion(Vector2 pos) => _button.transform.position = pos;

        public void SetLocalPostion(Vector2 pos) => _button.transform.localPosition = pos;

        public void SetParent(Transform parent) => _button.transform.SetParent(parent);

        public void Destroy() => Object.Destroy(_gameObject);
    }
}
