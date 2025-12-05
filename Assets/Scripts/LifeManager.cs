using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;


    [Header("Config")]
    public int maxLives = 3;
    public string gameOverSceneName = "GameOverScene";

    public static string lastLevel;
    public int CurrentLives { get; private set; }

    // Referencias del HUD (se asignan cada vez que carga una escena)
    [Header("HUD Hearts (scene-specific)")]
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    void Awake()
    {
        // Singleton persistente
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Inicializa solo una vez al arrancar el juego/menú
        if (CurrentLives <= 0) CurrentLives = maxLives;

        // Actualiza HUD al cargar escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca y enlaza corazones del HUD en la nueva escena (si cambian por escena)
        var binder = FindObjectOfType<HUDHeartsBinder>();
        if (binder != null)
        {
            SetHUD(binder.heart1, binder.heart2, binder.heart3);
        }
        UpdateHearts();
    }

    public void SetHUD(GameObject h1, GameObject h2, GameObject h3)
    {
        heart1 = h1;
        heart2 = h2;
        heart3 = h3;
        UpdateHearts();
    }

    public void PlayerDied()
    {
        lastLevel = SceneManager.GetActiveScene().name;
        CurrentLives = Mathf.Max(0, CurrentLives - 1);
        ScoreManager.Instance.ResetScore();
        UpdateHearts();


        if (CurrentLives > 0)
        {
            // Reinicia la escena actual SIN resetear vidas
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Sin vidas ? Game Over
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
// Restaurar vidas
    public void ResetLives()
    {
        CurrentLives = maxLives;
        UpdateHearts();
    }


    public void NewGame()
    {
        // Llamar desde el botón "Continuar" o "Nueva partida"
        CurrentLives = maxLives;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        if (heart1 != null) heart1.SetActive(CurrentLives >= 1);
        if (heart2 != null) heart2.SetActive(CurrentLives >= 2);
        if (heart3 != null) heart3.SetActive(CurrentLives >= 3);
    }
}
