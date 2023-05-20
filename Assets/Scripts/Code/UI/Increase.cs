using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Increase : MonoBehaviour
{
    [SerializeField] private Image image;

    [Header("Settings")]
    [SerializeField] private bool withMove = true;
    [SerializeField] private bool withFade = true;
    [SerializeField] private float delay = 0;
    [SerializeField] private float duration = 0.3f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void DoIncrease()
    {
        DOTween.Kill("decrease");
        transform
            .DOScale(0.65f, duration)
            .From()
            .SetEase(Ease.InSine)
            .SetDelay(delay)
            .SetId("increase")
            .OnKill(ReturnValues);
       
        if (withMove)
            Move();
        if (withFade)
        {
            image.DOFade(0, duration)
                .From()
                .SetDelay(delay);
        }
    }
    private void ReturnValues()
    {
        image.color = Color.white;
        image.transform.localScale = Vector3.one;
    }
    private void Move()
    {
        rectTransform
                    .DOAnchorPos(new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y-30), duration/2)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetDelay(delay);
    }
}
