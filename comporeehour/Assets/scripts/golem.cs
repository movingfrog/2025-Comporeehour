using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem : enemy
{
    public Vector2 attackRange;
    public Vector2 attackSize;
    public float attacktime;
    public float attackCool;

    Rigidbody2D rigid;
    Animator ani;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        isFind = true;
        attacktime = attackCool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    public void Update()
    {
        Collider2D collidor = Physics2D.OverlapBox(transform.position + (Vector3)attackRange * transform.localScale.x, attackSize, 0, layer);

        if (PlayerUse.IsLight)
        {
            if(collidor != null)
            {
                if(attacktime >= attackCool)
                {
                    ani.SetTrigger("isAttack");
                    PlayerHP.Instance.getHeart();
                    attacktime = 0;
                }
                else
                {
                    attacktime += Time.deltaTime;
                    ani.SetBool("isRun", false);
                }
            }
            else
            {
                attacktime += Time.deltaTime;
                Vector2 Pdistance = (PlayerHP.Instance.transform.position - transform.position).normalized;
                rigid.velocity = Vector2.right * (Pdistance.x >= 0 ? 1 : -1);
                transform.localScale = new Vector3(rigid.velocity.x >= 0 ? 1 : -1, 1, 1);
                ani.SetBool("isRun", true);
            }
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position + (Vector3)attackRange * transform.localScale.x, attackSize);
    }
}
