using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject trapObject;
    public bool isRain;
    public float Height;
    public bool isBall;
    public float power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isRain && !isBall)
            {
                StartCoroutine(poisen());
            }
            else if (isRain)
            {
                GameObject clone = Instantiate(trapObject);
                clone.transform.position = transform.position + Vector3.up * Height;
                clone.GetComponent<Rigidbody2D>().AddForce(Vector2.down * Height, ForceMode2D.Impulse);
            }
            else
            {
                trapObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * power * (trapObject.transform.localScale.x >= 0 ? 1 : -1), ForceMode2D.Impulse);
            }
            Destroy(GetComponent<Collider2D>());

        }
    }

    IEnumerator poisen()
    {
        for(int i = 0; i < 10; i++)
        {
            PlayerHP.Instance.HP -= 2;
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(PlayerHP.Instance.HPdown(1.5f));
        Destroy(gameObject);
    }
}
