using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioSource levelMusic;

    void Start()
    {
        if (levelMusic != null && !levelMusic.isPlaying)
        {
            levelMusic.Play();
        }
    }
}
