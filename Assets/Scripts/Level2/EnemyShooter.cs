using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
   public GameObject projectilePrefab;
    public Transform firePoint;

    public float shootInterval = 2f;
    public float projectileSpeed = 10f;
    public float projectileDistance = 6f;

    public LayerMask solidLayers; // زمین/دیوار برای نابودی چاقو

    float timer;

    void Start()
    {
        timer = shootInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = shootInterval;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // جهت شلیک: بر اساس نگاه دشمن
        Vector2 dir = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        var move = proj.GetComponent<Projectile>();
        if (move != null)
        {
            move.solidLayers = solidLayers;
            move.Init(dir, projectileSpeed, projectileDistance);
        }
        else
        {
            // اگر ProjectileMove روش نبود، حداقل نابودش کن که تو صحنه جمع نشه
            Destroy(proj, 3f);
        }
    }
}
