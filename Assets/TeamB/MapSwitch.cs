using System.Collections;
using System.Collections.Generic;
using BTAI;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapSwitch : MonoBehaviour
{
    // 把要控制的遊戲物件拖進來
    public GameObject blackmap;
    public GameObject whitemap;
    // public GameObject allmap;
    private bool isShowingObject1 = true;
    bool _interactHeld;
    bool _interactCooldown;
    void Start()
    {
        ShowOnlyObject();
    }

    void Update()
    {
        if (_interactCooldown) return;
        if (_interactHeld)
        {
            ShowOnlyObject();
            _interactCooldown = true;
        }
    }

    // 顯示特定遊戲物件，隱藏其他
    void ShowOnlyObject()
    {
        if (isShowingObject1)
        {
            blackmap.SetActive(true);
            whitemap.SetActive(false);
        }
        else
        {
            blackmap.SetActive(false);
            whitemap.SetActive(true);
        }
        // 切換狀態
        isShowingObject1 = !isShowingObject1;
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.started) {
            _interactHeld = true;
        } else if (context.canceled) {
            _interactHeld = false;
            _interactCooldown = false;
        }
    }
}
