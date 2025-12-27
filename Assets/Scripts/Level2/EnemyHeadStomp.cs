using UnityEngine;

public class EnemyHeadStomp : MonoBehaviour
{
   [Header("Stomp Settings")]
    public int hitsToKill = 4;          // چند بار لازم است روی سر بپره تا دشمن بمیره
    public float bounceForce = 9f;      // قدرت پرش بعد از استامپ
    public bool requireFalling = true;  // فقط وقتی پلیر در حال سقوطه حساب کن

    int currentHits;

    void Awake()
    {
        currentHits = hitsToKill;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // فقط پلیر
        if (!other.CompareTag("Player"))
            return;

        // اگر خواستی فقط وقتی پلیر از بالا میاد حساب بشه:
        if (requireFalling)
        {
            var prb = other.attachedRigidbody;
            if (prb != null && prb.linearVelocity.y >= 0f)
            {
                // پلیر در حال بالا رفتن/ایستاده → استامپ حساب نکن
                return;
            }
        }

        // 1) ضربه به دشمن
        currentHits--;

        // 2) بونس دادن به پلیر
        BouncePlayer(other);

        // 3) اگر تموم شد → دشمن بمیره
        if (currentHits <= 0)
        {
            KillEnemy();
        }
        else
        {
            // اگر بخوای می‌تونی اینجا صدا/افکت بزاری
            // Debug.Log("Enemy stomped! Remaining hits: " + currentHits);
        }
    }

    void BouncePlayer(Collider2D player)
    {
        var rb = player.attachedRigidbody;
        if (rb == null) return;

        // سرعت عمودی رو ریست کن که بونس همیشه یکدست باشه
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    void KillEnemy()
    {
        // بهتره Destroy روی روت دشمن انجام بشه نه فقط سر
        Transform root = transform.root;

        // اگر انیمیشن مرگ داری اینجا تریگرش کن
        // var anim = root.GetComponent<Animator>();
        // if (anim) anim.SetTrigger("Die");

        Destroy(root.gameObject);
    }
}
