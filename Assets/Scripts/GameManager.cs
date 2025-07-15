using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DeathScreen;

    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
    }
}
