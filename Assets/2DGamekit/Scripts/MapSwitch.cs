using System.Collections;
using System.Collections.Generic;
using BTAI;
using UnityEngine;

public class MapSwitch : MonoBehaviour
{
    // 把要控制的遊戲物件拖進來
    public GameObject blackmap;
    public GameObject whitemap;
    // public GameObject allmap;
    private bool isShowingObject1 = true;
    void Start()
    {
        blackmap.SetActive(false);
        whitemap.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowOnlyObject();
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
}
