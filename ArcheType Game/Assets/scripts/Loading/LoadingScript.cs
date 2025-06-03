using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBar;

    public float minLoadingTime = 2f;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

IEnumerator LoadSceneAsync(int sceneId)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
    operation.allowSceneActivation = false;

    LoadingScreen.SetActive(true);

    float Tempopassado = 0f;

    while (!operation.isDone)
    {
        Tempopassado += Time.deltaTime;

        float TempoProgresso = Mathf.Clamp01(Tempopassado / minLoadingTime);

        float ProgressoReal = Mathf.Clamp01(operation.progress / 0.9f);

        LoadingBar.value = Mathf.Min(TempoProgresso, ProgressoReal);

        if (operation.progress >= 0.9f && Tempopassado >= minLoadingTime)
        {
            operation.allowSceneActivation = true;
        }

        yield return null;
    }
}
}
