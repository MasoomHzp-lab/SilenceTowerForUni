using UnityEngine;

public class EnemyPatrolHop : MonoBehaviour
{
   [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    [Header("Hop")]
    public float hopForce = 6f;
    public float hopInterval = 1.2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Vector2 target;
    float hopTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("EnemyPatrolHop: pointA/pointB تنظیم نشده");
            enabled = false;
            return;
        }

        target = pointB.position;
        hopTimer = hopInterval;
    }

    void Update()
    {
        PatrolMove();
        HopLogic();
    }

    void PatrolMove()
    {
        Vector2 pos = rb.position;
        Vector2 next = Vector2.MoveTowards(pos, target, moveSpeed * Time.deltaTime);
        rb.MovePosition(next);

        if (Vector2.Distance(next, target) < 0.05f)
        {
            target = (target == (Vector2)pointA.position) ? (Vector2)pointB.position : (Vector2)pointA.position;
        }

        // رو به جهت حرکت بچرخ
        float dir = target.x - pos.x;
        if (Mathf.Abs(dir) > 0.01f)
        {
            Vector3 s = transform.localScale;
            s.x = (dir > 0) ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
            transform.localScale = s;
        }
    }

    void HopLogic()
    {
        hopTimer -= Time.deltaTime;
        if (hopTimer > 0f) return;

        hopTimer = hopInterval;

        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * hopForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return true; // اگه ندادی، گیر نده
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
