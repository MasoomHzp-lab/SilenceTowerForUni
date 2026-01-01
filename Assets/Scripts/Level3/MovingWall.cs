using UnityEngine;

public class MovingWall : MonoBehaviour
{
  
   [Header("Move Settings")]
    public float moveDistance = 3f;   // چقدر بیاد پایین
    public float moveSpeed = 2f;      // سرعت حرکت

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moveDown = false;

    private void Awake()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.down * moveDistance;
    }

    private void Update()
    {
        if (!moveDown) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }

    public void Open()
    {
        moveDown = true;
    }

}
