using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Detection")]
    public Transform groundCheck;     // Asignar en el Inspector
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;     // Seleccionar capa del suelo (p.ej. "Ground")

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool jumpPressed;         // Captura el input y salta en FixedUpdate

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (groundCheck == null)
        {
            Debug.LogWarning("GroundCheck no asignado en el Inspector.");
            return;
        }

        // --- Detección de suelo ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // --- Movimiento horizontal con teclado ---
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Animación de correr
        animator?.SetBool("isRunning", move != 0);

        // Voltear sprite según dirección
        if (move > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0) transform.localScale = new Vector3(-1, 1, 1);

        // --- Salto con teclado ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpPressed)
        {
            jumpPressed = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator?.SetTrigger("jump");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
