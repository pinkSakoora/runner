using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject PauseScreen;

    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            PauseScreen.SetActive(!PauseScreen.activeSelf);
            if (!PauseScreen.activeSelf)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
}
