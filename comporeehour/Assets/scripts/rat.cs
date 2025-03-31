using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rat : enemy
{
    public float Distance;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        for(int i = 0; i < 60; i++)
        {
            transform.position = Vector2.Lerp(transform.position, vec + Vector2.right * Distance * transform.localScale.x, 1 / 60f);
            yield return new WaitForSeconds(1 / 60f);
        }
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);

        StartCoroutine(move());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack();
        }
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1, layer);
        Debug.DrawRay(transform.position, Vector2.right * transform.localScale.x, Color.cyan);
        isFind = hit.collider != null;
    }
}
