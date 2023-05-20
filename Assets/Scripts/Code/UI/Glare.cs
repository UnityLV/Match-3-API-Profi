using DG.Tweening;
using UnityEngine;

public class Glare : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float delay;
    [SerializeField] private RectTransform rectTransform;

    private float _startPosition;
    private void Start()
    {
        _startPosition = rectTransform.anchoredPosition.x;
    }
    public void DoGlare()
    {
        rectTransform.DOAnchorPosX(300, duration)
            .SetDelay(delay)
            .SetRelative()
            .OnKill(ReturnValues);
    }
    private void ReturnValues()
    {
        rectTransform.anchoredPosition = new Vector2(_startPosition, rectTransform.anchoredPosition.y);
    }
}
