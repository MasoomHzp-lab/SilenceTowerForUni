using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SequenceButton : MonoBehaviour
{
    public int buttonID = 1;
    public SequencePuzzleManager manager;

    private bool pressed;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressed) return;
        if (!other.CompareTag("Player")) return;

        pressed = true;
        if (manager != null) manager.Press(buttonID);

        // افکت اختیاری
        transform.localScale *= 0.95f;
    }


}
