using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traps : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            for(int i = 0; i < 2; i++)
            {
                PlayerHP.Instance.getHeart();
            }
            Destroy(gameObject);
        }
    }
}
