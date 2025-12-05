using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool jumpPressed;
    private float moveDirection = 0f; // dirección actual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (groundCheck == null) return;

        // Detección de suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

#if UNITY_EDITOR
        // Simulación con mouse
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;

            if (pos.x < Screen.width / 2)
            {
                // Mitad izquierda ? deslizar controla dirección
                moveDirection = (pos.x < Screen.width / 4) ? -1f : 1f;
            }
            else
            {
                if (isGrounded) jumpPressed = true;
            }
        }
        else
        {
            moveDirection = 0f;
        }
#else
        // En móvil real
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
                {
                    if (t.position.x < Screen.width / 2)
                        moveDirection = (t.position.x < Screen.width / 4) ? -1f : 1f;
                    else if (isGrounded)
                        jumpPressed = true;
                }

                if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                    moveDirection = 0f;
            }
        }
        else
        {
            moveDirection = 0f;
        }
#endif

        // Animación de correr
        animator?.SetBool("isRunning", moveDirection != 0);

        // Voltear sprite
        if (moveDirection > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveDirection < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        // Aplicar movimiento en físicas
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        if (jumpPressed)
        {
            jumpPressed = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator?.SetTrigger("jump");
        }
    }
}
