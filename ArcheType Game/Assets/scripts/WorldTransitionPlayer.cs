using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;

public class WorldTransitionPlayer : MonoBehaviour
{
  public VideoPlayer videoPlayer;
    public DimensionTravel dimensionTravel; // arraste o script aqui no Inspector

    private System.Action callback;

    void Start()
    {
        gameObject.SetActive(false);

        // Evento quando o vídeo termina
        videoPlayer.loopPointReached += OnVideoFinished;

        // Evento quando o vídeo começa
        videoPlayer.started += OnVideoStarted;

        // Garantir que não toque sozinho
        videoPlayer.playOnAwake = false;
    }

    public void PlayTransition(System.Action onFinish)
    {
        callback = onFinish;

        gameObject.SetActive(true);

        // IMPORTANTE: preparar antes de tocar (senão falha na primeira execução)
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        videoPlayer.prepareCompleted -= OnPrepared;
        videoPlayer.Play(); // Agora pode tocar sem falhar
    }

    void OnVideoStarted(VideoPlayer vp)
    {
        // 🔥 Toca o som sincronizado com o frame exato do início do vídeo
        if (dimensionTravel != null)
        {
            if (dimensionTravel.mundonormal)
            {
                RuntimeManager.PlayOneShot(dimensionTravel.mundoSurreal, transform.position);
            }
            else if (dimensionTravel.mundosonho)
            {
                RuntimeManager.PlayOneShot(dimensionTravel.mundoNormal, transform.position);
            }
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
        gameObject.SetActive(false);

        callback?.Invoke();
        callback = null;
    }
}
