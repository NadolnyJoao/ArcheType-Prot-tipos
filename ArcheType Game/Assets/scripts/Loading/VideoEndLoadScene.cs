using UnityEngine;
using UnityEngine.Video;

public class VideoEndLoadScene : MonoBehaviour
{
    public int sceneToLoad = 1; // ID da cena que deseja carregar
    private VideoPlayer vp;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();

        // Quando o vídeo acabar, chama a função
        vp.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer source)
    {
        // Chama o carregamento da cena usando seu loading script
        LoadingScript.Instance.LoadScene(sceneToLoad);
    }
}
