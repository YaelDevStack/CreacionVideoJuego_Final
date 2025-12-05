using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // El jugador
    public Vector3 offset;        // Desfase opcional
    public float smoothSpeed = 5f; // Velocidad de suavizado

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
