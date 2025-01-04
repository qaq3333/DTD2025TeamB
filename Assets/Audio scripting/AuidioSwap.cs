using UnityEngine;
using UnityEngine.InputSystem;

public class AudioSwap : MonoBehaviour
{
    public AudioClip newTrack; // 新的音樂片段
    int flag=1;
    bool _interactHeld;
    bool _interactCooldown;

    private void Update()
    {
        if (_interactCooldown) return;
        // 當玩家按下 E 鍵時，切換背景音樂
        if (_interactHeld&&flag==1)
        {
            AudioController.instance.SwapTrack(newTrack);
            flag = 0;
            _interactCooldown = true;
        }
        else if (_interactHeld&&flag==0)
        {
            AudioController.instance.ReturnToDefault();
            flag = 1;
            _interactCooldown = true;
        }
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
