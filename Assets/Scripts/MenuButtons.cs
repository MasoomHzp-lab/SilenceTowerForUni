using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        // 1) اول سعی کن Instance رو بگیری
        GameManageer gm = GameManageer.Instance;

        // 2) اگر نبود، توی صحنه دنبالش بگرد
        if (gm == null)
            gm = FindObjectOfType<GameManageer>();

        // 3) اگر باز هم نبود، خودمون بسازیمش
        if (gm == null)
        {
            GameObject go = new GameObject("GameManageer");
            gm = go.AddComponent<GameManageer>();
            DontDestroyOnLoad(go);
        }

        // حالا مطمئنیم داریمش
        gm.StartGame();
    }
    public void QuitGame()
    {
        if (GameManageer.Instance != null)
            GameManageer.Instance.QuitGame();
        else
            Application.Quit();
    }
}
