using UnityEngine;

public class DoorOpener : MonoBehaviour
{
  public GameObject closedDoor;
    public GameObject openDoor;

    private bool opened = false;

    private void Start()
    {
        // فقط اگر هنوز باز نشده، حالت اولیه رو ست کن
        if (!opened)
        {
            if (closedDoor != null) closedDoor.SetActive(true);
            if (openDoor != null) openDoor.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        Debug.Log("[DoorSwitcher] OpenDoor() called");

        if (opened) return;
        opened = true;

        if (closedDoor == null || openDoor == null)
        {
            Debug.LogError("[DoorSwitcher] closedDoor یا openDoor وصل نیست!");
            return;
        }

        closedDoor.SetActive(false);
        openDoor.SetActive(true);

        Debug.Log("[DoorSwitcher] Door switched: CLOSED off, OPEN on");
    }
}
