 using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;
    public void Load(int index, Action<int> onLoaded = null) =>
         _coroutineRunner.StartCoroutine(LoadScene(index, onLoaded));

    public IEnumerator LoadScene(int index, Action<int> onLoaded = null)
    {
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(index);

        while (!waitNextScene.isDone)
            yield return null;

        onLoaded?.Invoke(index);
    }
}