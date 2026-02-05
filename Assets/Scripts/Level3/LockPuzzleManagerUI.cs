using UnityEngine;
using TMPro;

public class LockPuzzleManagerUI : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject lockPanel;
    public TMP_Text infoText;

    [Header("Snap Points (0..8)")]
    public RectTransform[] snapPoints; // size=9

    [Header("Tokens")]
    public DraggableTokenUI blueToken;
    public DraggableTokenUI redToken;
    public DraggableTokenUI purpleToken;

    [Header("Correct Code (must exist in UI)")]
    public int blueNumber = 4;
    public int redNumber = 6;
    public int purpleNumber = 8; // چون اسلات 9 نداری

    [Header("Door Switch")]
    public DoorOpener door;

    private bool solved = false;

    void Start()
    {
        if (lockPanel != null) lockPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        if (solved) return;
        if (lockPanel != null) lockPanel.SetActive(true);

        if (infoText != null)
            infoText.text = "کد رنگ‌ها را بچین.";
    }

    public void ClosePanel()
    {
        if (lockPanel != null) lockPanel.SetActive(false);
    }

    public void CloseFromButton()
    {
        ClosePanel();
    }

    public void CheckSolved()
    {
        if (solved) return;

        if (blueToken == null || redToken == null || purpleToken == null)
        {
            Debug.LogError("[LockPuzzle] Token refs missing!");
            if (infoText != null) infoText.text = "توکن‌ها وصل نیستن!";
            return;
        }

        int b = blueToken.CurrentNumber;
        int r = redToken.CurrentNumber;
        int p = purpleToken.CurrentNumber;

        if (infoText != null)
            infoText.text = $"آبی={b}  قرمز={r}  بنفش={p}";

        bool ok = (b == blueNumber && r == redNumber && p == purpleNumber);

        if (ok)
        {
            solved = true;

            if (infoText != null) infoText.text = "در باز شد!";

            if (door != null) door.OpenDoor();
            else Debug.LogError("[LockPuzzle] DoorSwitcher ref missing!");

            ClosePanel();
        }
    }
}
