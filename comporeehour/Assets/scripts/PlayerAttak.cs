using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttak : MonoBehaviour
{
    public Vector2 attackRange;
    public bool isAttack;
    public float attacckCool;
    float AttackTime;

    Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Collider2D enemy = Physics2D.OverlapBox(transform.position + (Vector3)attackRange * transform.localScale.x, Vector2.one, 0);
        if(AttackTime >= attacckCool)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ani.SetTrigger("isAttack");
                isAttack = true;
                if(enemy != null && enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<enemy>().getHeart();
                }
                AttackTime = 0;
            }
            else
            {
                AttackTime += Time.deltaTime;
            }
        }
        else
        {
            AttackTime += Time.deltaTime;
            isAttack = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + (Vector3)attackRange * transform.localScale.x, Vector2.one);
    }
}
