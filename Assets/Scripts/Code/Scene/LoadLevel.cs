using DG.Tweening;
using System;
using UnityEngine;

public class LoadLevel : MonoBehaviour, ICoroutineRunner, IInit<Action<int>>
{
    [SerializeField] private CanvasGroup loudScreen;
    [SerializeField] private float duration;
    private SceneLoader _sceneLoader;
    public static LoadLevel instance = null;
    private event Action<int> onLoaded;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
        _sceneLoader = new SceneLoader(this);
    }

    public void LoudScene(int index)
    {
        loudScreen.DOFade(1, duration);
        
        _sceneLoader.Load(index, (int sceneID) =>
        { 
            OnSceneLoud();
            onLoaded?.Invoke(sceneID);
        });
    }
    private void OnSceneLoud()
    {
        loudScreen.DOFade(0, duration); 
    }

    public void Initialize(Action<int> @delegate)
    {
        onLoaded +=@delegate;
    }

    public void Deinitialize(Action<int> @delegate)
    {
        onLoaded -= @delegate;

    }
}
