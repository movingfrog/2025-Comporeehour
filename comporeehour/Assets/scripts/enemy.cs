using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public LayerMask layer;
    public Vector2 vec;
    public bool isFind;
    
    protected virtual void Awake()
    {
        vec = transform.position;
    }

    protected void Attack()
    {
        PlayerHP.Instance.getHeart();
        Destroy(gameObject);
    }

    public void getHeart()
    {
        if (!isFind)
        {
            Destroy(gameObject);
        }
    }
}
