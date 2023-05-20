using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSetter : MonoBehaviour
{
    [SerializeField] private Image _bagroundImage;
    [SerializeField] private BackgroundConfig[] _configs;

    private void Start()
    {
        SetBaground();
    }

    private void SetBaground()
    {
        _bagroundImage.sprite = GetBackground();
    }

    private Sprite GetBackground()
    {
        foreach (var config in _configs)
        {
            if (IsSutableConfig(config))
            {
                return config.Sprite;
            }
        }

        return GetHighestLevelBackground();
    }

    private bool IsSutableConfig(BackgroundConfig config)
    {
        return config.StartLevel <= LevelSelector.SelectedLevel &&
            config.EndLevel >= LevelSelector.SelectedLevel;
    }

    private Sprite GetHighestLevelBackground()
    {
        return _configs.OrderByDescending(c => c.EndLevel).Select(c => c.Sprite).First(); ;
    }
}
