using UnityEngine;

public class AudioSwap : MonoBehaviour
{
    public AudioClip newTrack; // 新的音樂片段
    int flag=1;
    private void Update()
    {
        // 當玩家按下 E 鍵時，切換背景音樂
        if (Input.GetKeyDown(KeyCode.E)&&flag==1)
        {
            Audiocontroller.instance.SwapTrack(newTrack);
            flag = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E)&&flag==0)
        {   
            Audiocontroller.instance.ReturnToDefault();
            flag = 1;
        }
    }
}