using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Notifica al ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.CollectDrop();
            }

            // Destruye la gota recogida
            Destroy(gameObject, 0.05f);

        }
    }
}
