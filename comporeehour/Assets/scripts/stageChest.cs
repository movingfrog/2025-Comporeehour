using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stageChest : MonoBehaviour
{
    public Item item;

    public int stage;
    public int num;
    public bool isIn;
    public GameObject F;

    private void Start()
    {
        if (chestManager.Instance.isOpen[stage].isOpen[num])
        {
            GetComponent<Animator>().enabled = true;
            Destroy(GetComponent<Collider2D>());
        }
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
        if(isIn && Input.GetKeyDown(KeyCode.F) && Inventory.Instance.maximumSlots(item))
        {
            GetComponent<Animator>().enabled = true;
            Destroy(GetComponent<Collider2D>());
            Inventory.Instance.AddItem(item);
            chestManager.Instance.isOpen[stage].isOpen[num] = true;
        }
    }
}
