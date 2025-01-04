using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class player : MonoBehaviour
{

    public float speed = 0.1f;
    Rigidbody2D rigid2D;
    public float jumpForce = 0.3f; // ���D���O�q
    public int jumpCount = 0; // ��e���D����
    public int maxJumpCount = 2; // �̤j���\���D����
    public float speed_x_constraint;
    public Animator playerani;
    public SpriteRenderer playerSr;
    public Animator playjump;
    public AudioSource footStepAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;

    float _moveDirection;
    bool _jumpHeld;

    void Start()
    {
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

  
    void FixedUpdate()
    {
        bool IsWalking = false;
        bool IsJump = false;
        if (playjump.GetInteger("jump") == 0 && _jumpHeld && jumpCount < maxJumpCount)
        {
            IsJump = true;
            // ������D
            rigid2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            // �W�[���D����
            jumpCount++;
            jumpAudioSource.Play();
        }

        if (_moveDirection > 0f)
        {
            if(playerSr.flipX == true)
            {
                playerSr.flipX= false;
            }
            IsWalking = true;
            rigid2D.AddForce(new Vector2(500 * speed, 0), ForceMode2D.Force);
            //this.gameObject.transform.position += new Vector3(speed, 0, 0);
        }
        if (_moveDirection < 0f)
        {
            if (playerSr.flipX == false)
            {
                playerSr.flipX = true;
            }
            IsWalking = true;
            rigid2D.AddForce(new Vector2(-500 * speed, 0), ForceMode2D.Force);
            if (!footStepAudioSource.isPlaying) footStepAudioSource.Play();
        }

        if (_moveDirection == 0f)
            if (footStepAudioSource.isPlaying)
                footStepAudioSource.Stop();
        if (IsWalking)
        {
            if (playerani.GetInteger("status") == 0)
                playerani.SetInteger("status", 1);
        }

        else
        {
            if (playerani.GetInteger("status") == 1)
                playerani.SetInteger("status", 0);
        }

        if (IsJump)
        {
            if (playjump.GetInteger("jump") == 0)
                playjump.SetInteger("jump", 1);
        }

        else
        {
            if (playjump.GetInteger("jump") == 1)
                playjump.SetInteger("jump", 0);
        }
       
        //constraint
        if (rigid2D.linearVelocity.x > speed_x_constraint)
        {
            rigid2D.linearVelocity = new Vector2(speed_x_constraint, rigid2D.linearVelocity.y);
        }
        if (rigid2D.linearVelocity.x < -speed_x_constraint)
        {
            rigid2D.linearVelocity = new Vector2(-speed_x_constraint, rigid2D.linearVelocity.y);
        }
    }
    // �T�O�b���ⱵĲ�a���ɥi�H���m���D����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameClear")) FindAnyObjectByType<GameUIController>()?.GameClear();
        // �ˬd�I������H�O�_�O�a��
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ���m���D����
            jumpCount = 0;
            landAudioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("GameOver")) FindAnyObjectByType<GameUIController>()?.GameOver();
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveDirection = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started) {
            _jumpHeld = true;
        } else if (context.canceled) {
            _jumpHeld = false;
        }
    }
}
