using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Pixygon.ButtonEffects
{
    public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] protected TextMeshProUGUI _text;
        [SerializeField] protected Image _icon;
        [SerializeField] protected AnimationType _animType = AnimationType.FullSize;
        [SerializeField] protected bool _useHoverAction;
        [SerializeField] protected float _sizeChange = 1.1f;
        protected Action ClickAction;
        protected Vector2 _startSize;
        protected AudioSource _audioHover;
        protected bool _useAudio;

        protected enum AnimationType {
            FullSize,
            Color,
            Size,
            Text
        }

        protected virtual void Awake() {
            _audioHover = GetComponent<AudioSource>();
            _useAudio = _audioHover != null;
        }

        public virtual void OnEnable() {
            switch (_animType) {
                case AnimationType.FullSize:
                    transform.localScale = Vector3.one;
                    break;
                case AnimationType.Color:
                    _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b,1f);
                    break;
                case AnimationType.Size:
                    _icon.rectTransform.sizeDelta = _startSize *_sizeChange;
                    break;
                case AnimationType.Text:
                    _text.transform.localScale = Vector3.one;
                    break;
            }
        }
        public virtual void OnPointerEnter(PointerEventData eventData) {
            Select();
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            Deselect();
        }

        

        public virtual void Activate() {
            ClickAction?.Invoke();
        }

        public void OnSelect(BaseEventData eventData) {
            Select();
        }
        public void OnDeselect(BaseEventData eventData) {
            Deselect();
        }
        
        private void Select() {
            Debug.Log("Select");
            if (_useAudio) {
                _audioHover.pitch = Random.Range(.9f, 1.1f);
                _audioHover.Play();
            }
            if (!_useHoverAction) return;
            switch (_animType) {
                case AnimationType.FullSize:
                    transform.localScale = Vector3.one * _sizeChange;
                    break;
                case AnimationType.Color:
                    _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b,.8f);
                    break;
                case AnimationType.Size:
                    _icon.rectTransform.sizeDelta = _startSize;
                    break;
                case AnimationType.Text:
                    _text.transform.localScale = Vector3.one * _sizeChange;
                    break;
            }
        }
        private void Deselect() {
            if (!_useHoverAction) return;
            switch (_animType) {
                case AnimationType.FullSize:
                    transform.localScale = Vector3.one;
                    break;
                case AnimationType.Color:
                    _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b,1f);
                    break;
                case AnimationType.Size:
                    _icon.rectTransform.sizeDelta = _startSize *_sizeChange;
                    break;
                case AnimationType.Text:
                    _text.transform.localScale = Vector3.one;
                    break;
            }
        }
    }
}