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
            Debug.LogError("MovingPlatform2D: PointA/PointB is not assigned.");
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

        // با MovePosition حرکت فیزیکی نرم‌تر و پایدارتره
        rb.MovePosition(target);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // اگر پلیر روی سکو نشست، بچسبه به سکو
        if (col.collider.CompareTag("Player"))
        {
            // فقط وقتی از بالا برخورد کرده (ایستادن روی سکو)
            foreach (var c in col.contacts)
            {
                if (c.normal.y > 0.5f)
                {
                    col.collider.transform.SetParent(transform);
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (col.collider.transform.parent == transform)
                col.collider.transform.SetParent(null);
        }
    }
}
