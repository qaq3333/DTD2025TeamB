using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class NewMonoBehaviourScript : MonoBehaviour
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
    void Start()
    {
        print("Start");
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        print("jump");
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

  
    void Update()
        
    {
        bool IsWalking = false;
        bool IsJump = false;
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            IsJump = true;
            // ������D
            rigid2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            // �W�[���D����
            jumpCount++;
            print("Jump");
        }

        if (Input.GetKey(KeyCode.D))
        {
            if(playerSr.flipX == true)
            {
                playerSr.flipX= false;
            }
            IsWalking = true;
            rigid2D.AddForce(new Vector2(500 * speed, 0), ForceMode2D.Force);
            //this.gameObject.transform.position += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (playerSr.flipX == false)
            {
                playerSr.flipX = true;
            }
            IsWalking = true;
            rigid2D.AddForce(new Vector2(-500 * speed, 0), ForceMode2D.Force);
        }

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
        print(rigid2D.angularVelocity);
       
    }
    // �T�O�b���ⱵĲ�a���ɥi�H���m���D����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ˬd�I������H�O�_�O�a��
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ���m���D����
            jumpCount = 0;
        }
    }

}