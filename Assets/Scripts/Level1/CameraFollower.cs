using UnityEngine;

public class CameraFollower : MonoBehaviour
{
 [Header("Target")]
    public Transform target;          // کرکتری که باید دنبال بشه

    [Header("Settings")]
    public float smoothSpeed = 5f;    // نرمی حرکت دوربین
    public Vector3 offset;            // فاصله از کاراکتر
    public bool followY = false;      // آیا در جهت Y هم دنبال کنه؟

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // اگه فقط محور X رو بخوای دنبال کنه:
        if (!followY)
            desiredPosition.y = transform.position.y;

        // حرکت نرم با Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
