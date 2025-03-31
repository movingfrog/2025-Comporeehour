using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomalChest : MonoBehaviour
{
    public Item[] items;
    Item item;
    public bool isIn;
    public GameObject F;

    private void Start()
    {
        item = items[Random.Range(0, items.Length)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
            F.SetActive(isIn);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
            F.SetActive(isIn);
        }
    }

    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.F) && Inventory.Instance.maximumSlots(item))
        {
            GetComponent<Animator>().enabled = true;
            Destroy(GetComponent<Collider2D>());
            Inventory.Instance.AddItem(item);
        }
    }
}
