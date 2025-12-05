using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TrailerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // Asigna el VideoPlayer en el Inspector
    public string nextSceneName = "FirstLevel"; // Escena del juego

    void Start()
    {
        // Reproduce el video
        videoPlayer.Play();

        // Suscribirse al evento de fin de reproducción
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Cuando termina el video, carga la escena del juego
        SceneManager.LoadScene(nextSceneName);
    }
}
