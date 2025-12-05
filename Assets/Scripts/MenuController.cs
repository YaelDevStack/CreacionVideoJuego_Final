using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método para el botón Jugar
    public void Jugar()
    {
        SceneManager.LoadScene("Trailer");
    }

    // Método para el botón Salir
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    // Método para el botón Reintentar
    public void Reintentar()
    {
        if (!string.IsNullOrEmpty(LifeManager.lastLevel))
        {
            // Restaurar las vidas
            LifeManager.Instance.ResetLives();
            SceneManager.LoadScene(LifeManager.lastLevel);
        }
        else
        {
            Debug.LogWarning("No hay nivel guardado para reintentar");
        }
    }
}
