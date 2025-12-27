using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [Header("Health Settings")]
    public int maxHits = 4;

    int currentHits;
    bool isDead = false;

    void Awake()
    {
        currentHits = maxHits;
    }

    // این متد از EnemyHeadStomp صدا زده میشه
    public void TakeStompHit()
    {
        if (isDead) return;

        currentHits--;
        Debug.Log("Enemy hit! Remaining: " + currentHits);

        if (currentHits <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // اگر انیمیشن مرگ داری
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        // اگر پرتاب‌کننده داره، خاموشش کن
        var shooter = GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            shooter.enabled = false;
        }

        // نابودی دشمن (یه تاخیر کوچیک بهتره)
        Destroy(gameObject, 0.2f);
    }
}
