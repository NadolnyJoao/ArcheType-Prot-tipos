using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBar;
    public Animator aniBackground;

    public float minLoadingTime = 2f;
    public static LoadingScript Instance { get; private set; }

      private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional se quiser que ele sobreviva entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int sceneId)
    {
        aniBackground.SetTrigger("fadeout");
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
