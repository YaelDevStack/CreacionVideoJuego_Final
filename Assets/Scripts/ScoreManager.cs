using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int totalDrops = 10;
    private int collectedDrops = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasigna el marcador en cada escena
        var binder = FindObjectOfType<HUDScoreBinder>();
        if (binder != null)
        {
            scoreText = binder.scoreText;
            UpdateScoreUI();
        }
        ResetScore();
    }

    public void CollectDrop()
    {
        collectedDrops++;
        UpdateScoreUI();

        if (collectedDrops >= totalDrops)
        {
            LevelComplete();
        }
    }

    // Ahora es público para que HUDScoreBinder pueda llamarlo
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = collectedDrops + "/" + totalDrops;
        }
    }


    void LevelComplete()
    {

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "FirstLevel")
        {
            SceneManager.LoadScene("SecondLevel");
        }
        else if (currentScene == "SecondLevel")
        {
            SceneManager.LoadScene("Trailer2");
        }
        else if (currentScene == "ThirdLevel")
        {
            SceneManager.LoadScene("WinScene");
        }
    }

     // Reinicia el contador (modo clásico: al morir vuelves a empezar con 0/10)
    public void ResetScore()
    {
        collectedDrops = 0;
        UpdateScoreUI();
    }
}
