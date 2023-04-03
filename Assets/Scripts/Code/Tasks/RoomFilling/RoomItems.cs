using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class RoomItems : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] roomItems;
    [SerializeField] private float duration = 1;
    [SerializeField] private float fadeValue = 0;
    private void Start()
    {
        if (PlayerPrefs.HasKey(name))
        {
            if (PlayerPrefs.GetInt(name)==1)
            {
                SwitchFade(null);
            }
        }
    }
    public void SwitchFade(UnityAction onSwiched)
    {
        bool isFirstItem = true;
        foreach (var item in roomItems)
        {
            if (fadeValue == 1)
            {
                item.gameObject.SetActive(true);
            }
            item.DOFade(fadeValue, duration).OnKill(() =>
            {
                if(isFirstItem&& onSwiched!= null)
                    onSwiched?.Invoke();
                isFirstItem = false;
                if (fadeValue == 0)
                {
                    item.gameObject.SetActive(false);
                }
            });
        }
        PlayerPrefs.SetInt(name, 1);
    }
}
