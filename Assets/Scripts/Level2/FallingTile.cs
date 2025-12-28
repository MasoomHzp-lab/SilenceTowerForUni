using UnityEngine;

public class FallingTile : MonoBehaviour
{
     [Header("Fall Settings")]
    public float delayBeforeFall = 1.5f;  // چند ثانیه بعد از لمس بیفته
    public float destroyAfter = 3f;       // چند ثانیه بعد از افتادن نابود شه (اختیاری)

    Rigidbody2D rb;
    bool triggered = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // اولش ثابت باشه
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered) return;

        if (collision.collider.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(Fall), delayBeforeFall);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
