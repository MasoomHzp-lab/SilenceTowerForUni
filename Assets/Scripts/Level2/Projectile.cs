using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 6f; // چند متر جلوتر بره بعد ناپدید شه
    public LayerMask solidLayers;  // زمین/دیوار

    Vector2 startPos;
    Vector2 dir = Vector2.right;

    public void Init(Vector2 direction, float spd, float distance)
    {
        dir = direction.normalized;
        speed = spd;
        maxDistance = distance;
        startPos = transform.position;
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // خوردن به پلیر
        if (other.CompareTag("Player"))
        {
            // PlayerHealth خودش TakeDamage رو انجام میده (از OnTriggerEnter2D)
            Destroy(gameObject);
            return;
        }

        // خوردن به زمین/دیوار
        if (((1 << other.gameObject.layer) & solidLayers) != 0)
        {
            Destroy(gameObject);
        }
    }
}
