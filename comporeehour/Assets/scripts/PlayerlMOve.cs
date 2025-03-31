using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerlMOve : MonoBehaviour
{
    public float tSpeed = 5;
    public float JumpPower = 10;
    public float distance = 0.1f;
    public float down = 0.6f;
    public LayerMask layer;

    public bool isSit;

    Rigidbody2D rigid;
    Animator ani;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));
        Jump(Input.GetAxisRaw("Vertical"));
        isSit = sit(Input.GetAxisRaw("Vertical"));
        ani.SetBool("isSit", isSit);
    }

    void Move(float H)
    {
        if (H != 0 && !isSit && !GetComponent<PlayerAttak>().isAttack)
        {
            ani.SetBool("isRun", true);
            rigid.velocity = new Vector2(H * (tSpeed + Inventory.Instance.Speed), rigid.velocity.y);
            if(rigid.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if(rigid.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    void Jump(float V)
    {
        RaycastHit2D Hit = Physics2D.Raycast(rigid.position + Vector2.down * down, Vector2.down * distance, distance, layer);
        Debug.DrawRay(rigid.position + Vector2.down * down, Vector2.down * distance, Color.red);
        if(Hit.collider != null && Input.GetKeyDown(KeyCode.Space))
        {
            if(V < 0 && !Hit.collider.CompareTag("isNotDown"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
                return;
            }
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);

        }
        ani.SetBool("isJump", rigid.velocity.y >= 0.1f);
        ani.SetBool("isFall", rigid.velocity.y <= -0.1f);
    }

    bool sit(float V)
    {
        if(V < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
