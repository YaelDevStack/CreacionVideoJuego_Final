using UnityEngine;
using UnityEngine.UI;

public class PlayerMobileController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;
    public bl_Joystick joystick;   // referencia al joystick en la UI

    [Header("Jump Button")]
    public Button jumpButton;      // botón de salto en la UI

    [Header("Ground Detection")]
    public Transform groundCheck;     // Asignar en el Inspector
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;     // Seleccionar capa del suelo (p.ej. "Ground")

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Conectar el botón de salto
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnJumpButtonPressed);
        }
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

        // --- Movimiento horizontal con joystick ---
        float move = joystick != null ? joystick.Horizontal : 0f;

        // Aplicar movimiento horizontal
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Animación de correr
        animator?.SetBool("isRunning", move != 0);

        // Voltear sprite según dirección
        if (move > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Debug para verificar valores del joystick
        Debug.Log("Joystick Horizontal: " + move);
    }

    void FixedUpdate()
    {
        if (jumpPressed && isGrounded)
        {
            jumpPressed = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator?.SetTrigger("jump");
        }
    }

    // Método para el botón de salto
    private void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            jumpPressed = true;
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
