using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{

    public float speed = 0.1f;
    Rigidbody2D rigid2D;
    public float jumpForce = 0.3f; // 跳躍的力量
    public int jumpCount = 0; // 當前跳躍次數
    public int maxJumpCount = 2; // 最大允許跳躍次數
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
            // 執行跳躍
            rigid2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            // 增加跳躍次數
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
    // 確保在角色接觸地面時可以重置跳躍次數
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查碰撞的對象是否是地面
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 重置跳躍次數
            jumpCount = 0;
        }
    }

}