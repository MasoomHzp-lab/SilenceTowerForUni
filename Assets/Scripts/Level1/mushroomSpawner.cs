using UnityEngine;

public class mushroomSpawner : MonoBehaviour
{
 
[Header("Prefab")]
    public GameObject mushroomPrefab;

    [Header("Settings")]
    public int spawnPoints = 30;         // چند نقطه بررسی بشه
    public float distanceBetween = 6f;   // فاصله بین نقاط بررسی
    [Range(0f, 1f)]
    public float spawnChance = 0.5f;     // احتمال ظاهر شدن قارچ
    public float raycastHeight = 10f;    // از چه ارتفاعی Ray پایین بره
    public LayerMask groundLayer;        // لایه‌ی زمین (Ground)

    void Start()
    {
        for (int i = 0; i < spawnPoints; i++)
        {
            if (Random.value <= spawnChance)
            {
                // موقعیت فرضی در X
                float xPos = i * distanceBetween;
                Vector2 rayOrigin = new Vector2(xPos, raycastHeight);

                // ری‌کست از بالا به پایین برای پیدا کردن زمین
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, Mathf.Infinity, groundLayer);

                if (hit.collider != null)
                {
                    Vector3 spawnPos = hit.point;
                    Instantiate(mushroomPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    // فقط برای تست، مسیر ری‌کست‌ها رو در صحنه نشون بده
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < spawnPoints; i++)
        {
            Vector3 origin = new Vector3(i * distanceBetween, raycastHeight, 0);
            Gizmos.DrawLine(origin, origin + Vector3.down * raycastHeight * 2);
        }
    }
}
