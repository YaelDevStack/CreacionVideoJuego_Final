using UnityEngine;

public class HUDHeartsBinder : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void Start()
    {
        if (LifeManager.Instance != null)
        {
            LifeManager.Instance.SetHUD(heart1, heart2, heart3);
        }
    }
}
