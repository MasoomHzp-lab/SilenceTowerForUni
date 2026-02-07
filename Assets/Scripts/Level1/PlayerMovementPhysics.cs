using UnityEngine;

public class PlayerMovementPhysics : MonoBehaviour
{
     public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // بررسی برخورد با زمین
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // پرش فقط وقتی روی زمینه
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        // فقط برای دیدن ناحیه بررسی زمین در Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
