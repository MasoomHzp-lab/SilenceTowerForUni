using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  [Header("Path")]
    public Transform pointA;
    public Transform pointB;

    [Header("Move")]
    public float speed = 2f;

    Rigidbody2D rb;
    Vector2 a, b;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("MovingPlatform: PointA/PointB is not assigned.");
            enabled = false;
            return;
        }

        a = pointA.position;
        b = pointB.position;
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector2 target = Vector2.Lerp(a, b, t);
        rb.MovePosition(target);
    }

    // 🔴 این بخش تغییر کرده
    void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponentInParent<PlayerMovementPhysics>();
        if (player != null)
        {
            player.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var player = col.collider.GetComponentInParent<PlayerMovementPhysics>();
        if (player != null && player.transform.parent == transform)
        {
            player.transform.SetParent(null);
        }
    }
}
