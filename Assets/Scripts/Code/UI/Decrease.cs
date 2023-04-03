using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Assets.Scripts.Code.UI.Decrease;

namespace Assets.Scripts.Code.UI
{
    public delegate void DecreaseComplete();

    public class Decrease : MonoBehaviour, IInit<DecreaseComplete>
    {
        private event DecreaseComplete _decreaseComplete;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private bool isSeparateCanvasGroup;
        [SerializeField] private CanvasGroup group;
        private CanvasController _canvasControl;
        private void Start()
        {
            _canvasControl = CanvasController.instance;
        }
        public void DoDecrease()
        {
            DOTween.Kill("increase");
            transform
                .DOScale(1.05f, duration)
                .OnKill(() => Complete()).SetId("decrease");

        }
        private void Complete()
        {
            if (group!=null)
            {
                if (!isSeparateCanvasGroup)
                {
                    _canvasControl.MenuDeactivate();
                }
                else
                {
                    _canvasControl.Setter.TurnOffCanvasGroup(group);
                }
            }
            _decreaseComplete?.Invoke();
            transform.localScale = Vector3.one;
        }

        public void Initialize(DecreaseComplete @delegate)
        {
            _decreaseComplete += @delegate;
        }

        public void Deinitialize(DecreaseComplete @delegate)
        {
            _decreaseComplete -= @delegate;
        }
    }
}