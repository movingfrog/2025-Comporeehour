using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class bat : enemy
{
    public float Speed = 5;
    public float distance = 0.6f;
    public float direction = 3;
    Rigidbody2D rigid;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x * distance, distance, layer);
        Debug.DrawRay(transform.position, Vector2.right * transform.localScale.x * distance, Color.red);
        if (!PlayerUse.IsLight)
        {
            direction = 5;
        }
        else
        {
            direction = 3;
        }
        isFind = Vector2.Distance(transform.position, PlayerHP.Instance.transform.position) <= direction;
        if(hit.collider != null)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
        if (!isFind && transform.position.y == vec.y)
        {
            rigid.velocity = new Vector2(Speed * transform.localScale.x, rigid.velocity.y);
        }
        else if (!isFind)
        {
            transform.position = Vector2.MoveTowards(transform.position, vec, 0.01f);
        }
        else
        {
            Vector2 Pdistance = (PlayerHP.Instance.transform.position - transform.position).normalized;
            rigid.velocity = Pdistance * (Speed * 2 - 1);
        }
    }
}
