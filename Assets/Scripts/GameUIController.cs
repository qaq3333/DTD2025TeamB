using Gamekit2D;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour {
    public MenuState currentState;
    public GameObject menuEffect;
    public GameObject gameClearMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public InputActionReference pauseAction;

    [SceneName]
    public string mainMenuSceneName;

    void OnEnable() {
        SetMenuState(currentState);
        pauseAction.action.performed += OnPause;
    }

    void OnDestroy() {
        pauseAction.action.performed -= OnPause;
    }

    public void SetMenuState(MenuState state) {
        switch (state) {
            case MenuState.None:
                Time.timeScale = 1f;
                menuEffect.SetActive(false);
                gameClearMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case MenuState.GameClear:
                Time.timeScale = 0f;
                menuEffect.SetActive(true);
                gameClearMenu.SetActive(true);
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case MenuState.GameOver:
                Time.timeScale = 0f;
                menuEffect.SetActive(true);
                gameClearMenu.SetActive(false);
                gameOverMenu.SetActive(true);
                pauseMenu.SetActive(false);
                break;
            case MenuState.Pause:
                Time.timeScale = 0f;
                menuEffect.SetActive(true);
                gameClearMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(true);
                break;
        }

        currentState = state;
    }

    public void GameClear() {
        if (currentState is MenuState.None) SetMenuState(MenuState.GameClear);
    }

    public void GameOver() {
        if (currentState is MenuState.None) SetMenuState(MenuState.GameOver);
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (currentState is MenuState.None or MenuState.Pause)
            SetMenuState(currentState == MenuState.Pause ? MenuState.None : MenuState.Pause);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1f;
    }

    public enum MenuState {
        None,
        GameClear,
        GameOver,
        Pause
    };
}
