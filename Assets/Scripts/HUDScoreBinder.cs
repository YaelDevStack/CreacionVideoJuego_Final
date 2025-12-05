using UnityEngine;
using TMPro;

public class HUDScoreBinder : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.scoreText = scoreText;
            ScoreManager.Instance.UpdateScoreUI();
        }
    }
}
