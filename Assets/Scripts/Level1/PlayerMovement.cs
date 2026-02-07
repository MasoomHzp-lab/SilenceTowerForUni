using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;         // نقطه‌ای زیر پای بازیکن
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;         // لایه‌ی زمین

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ورودی افقی از بازیکن
        moveInput = Input.GetAxisRaw("Horizontal");

        // پرش
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // حرکت افقی
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // بررسی تماس با زمین
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        // نمایش دایره‌ی بررسی زمین در Scene
        Gizmos.color = Color.yellow;
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
