using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartGameBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private CanvasGroup playButton;
    [SerializeField] private float duration;
    [SerializeField] private float durationBauutonFade;
    private static bool _isFirstTime = true;
    private void Start()
    {
        if (!_isFirstTime)
            return;
        _isFirstTime = false;
        if(bar == null)
            return;
        bar.DOFillAmount(1, duration).SetEase(Ease.OutSine)
        .OnKill(() => playButton?.DOFade(1, durationBauutonFade).OnKill(() =>
        {
            playButton.interactable = true;
            playButton.blocksRaycasts = true;
        }).SetEase(Ease.InSine));
    }
}
