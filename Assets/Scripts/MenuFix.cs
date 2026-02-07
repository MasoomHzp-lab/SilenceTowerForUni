using UnityEngine;

public class MenuFix : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;   // ✅ هر بار منو لود شد، از فریز دربیاد
    }
}
