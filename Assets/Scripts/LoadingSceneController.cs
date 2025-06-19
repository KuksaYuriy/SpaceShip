using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public Slider loadingProgressSlider;
    private void Start()
    {
        StartCoroutine(AsyncLoadingScene());
    }
    
    IEnumerator AsyncLoadingScene()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(2);
        if (loadAsync == null) Debug.LogError("loadAsync variable in LoadingSceneController is null");
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            float progress = loadAsync.progress;
            loadingProgressSlider.value = progress;
            
            if (progress >= 0.9f) loadAsync.allowSceneActivation = true;
            
            yield return new WaitForSeconds(1);
        }
    }
}