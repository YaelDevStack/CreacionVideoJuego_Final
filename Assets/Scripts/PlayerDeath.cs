using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject explosionAnimation;
    public AudioSource gameOverAudio;

    private LifeManager lifeManager;

    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FloorDestruction")
        {
            Die();
        }
        else if (collision.gameObject.name == "smoke")
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FloorDestruction")
        {
            Die();
        } else if (collision.gameObject.name == "smoke"){
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionAnimation != null)
        {
            explosionAnimation.SetActive(true);
            explosionAnimation.transform.parent = null;
        }

        if (gameOverAudio != null)
        {
            gameOverAudio.Play();
        }

        // Notifica al LifeManager que el jugador murió
        if (lifeManager != null)
        {
            lifeManager.PlayerDied();
        }

        Destroy(gameObject);
    }
}
