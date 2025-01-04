using UnityEngine;
using UnityEngine.InputSystem;

public class GameUIController : MonoBehaviour {
    public InputActionReference pauseAction;
    public GameObject menuEffect;
    public GameObject gameClearMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public MenuState currentState;

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

    public void OnPause(InputAction.CallbackContext context) {
        if (currentState is MenuState.None or MenuState.Pause)
            SetMenuState(currentState == MenuState.Pause ? MenuState.None : MenuState.Pause);
    }

    public void GameClear() {
        if (currentState is MenuState.None) SetMenuState(MenuState.GameClear);
    }

    public void GameOver() {
        if (currentState is MenuState.None) SetMenuState(MenuState.GameOver);
    }

    public enum MenuState {
        None,
        GameClear,
        GameOver,
        Pause
    };
}
