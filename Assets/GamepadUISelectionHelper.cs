using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GamepadUISelectionHelper : MonoBehaviour {
    public GameObject defaultSelectedGameObject;

    void Update() {
        if (Gamepad.current == null || !Gamepad.current.wasUpdatedThisFrame) return;
        if (EventSystem.current.currentSelectedGameObject != null) return;
        EventSystem.current.SetSelectedGameObject(defaultSelectedGameObject);
    }
}
