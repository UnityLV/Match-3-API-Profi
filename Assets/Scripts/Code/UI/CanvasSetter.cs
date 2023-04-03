using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Code.UI
{
    public class CanvasSetter
    {
        private CanvasGroup _curentCanvas;
        public CanvasGroup CurentCanvas => _curentCanvas;
        public void SetCanvasGroup(CanvasGroup newGroup)
        {
            if (_curentCanvas != null)
            {
                _curentCanvas.alpha = 0;
                _curentCanvas.interactable = false;
                _curentCanvas.blocksRaycasts = false;
            }
            if (newGroup == null)
                return;
            _curentCanvas = newGroup;
            _curentCanvas.alpha = 1;
            _curentCanvas.interactable = true;
            _curentCanvas.blocksRaycasts = true;
        }
        /// <summary>
        /// Use only with separate UI element
        /// </summary>
        public void TurnOffCanvasGroup(CanvasGroup group)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
        /// <summary>
        /// Use only with separate UI element
        /// </summary>
        public void TurnOnCanvasGroup(CanvasGroup group)
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }
}