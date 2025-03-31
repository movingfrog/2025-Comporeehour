using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopsystem : MonoBehaviour
{
    [Serializable]
    public struct buyButton
    {
        public Button button;
        public Text priceText;
        public GameObject soldOut;
        public float Price;
    }

    public bool isIn;
    public GameObject F;
    public GameObject panel;

    public buyButton[] item;
    public float[] price;
    public Text Money;

    private void Awake()
    {
        price = new float[item.Length];
        for(int i = 0; i < price.Length; i++)
        {
            price[i] = item[i].Price;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
            F.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
            F.SetActive(false);
            panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            for(int i = 0; i < item.Length; i++)
            {
                item[i].Price = 0;
            }
        }
        if(isIn && Input.GetKeyDown(KeyCode.F))
        {
            panel.SetActive(!panel.activeSelf);
        }
        for(int i = 0; i < item.Length; i++)
        {
            item[i].priceText.text = item[i].Price.ToString() + "¾ï";
        }
        Money.text = Inventory.Instance.Money.ToString("0.0") + "¾ï";
        for(int i = 0; i < item.Length; i++)
        {
            if ((i < Inventory.Instance.invenSize.Length&& Inventory.Instance.invenSize[i]))
            {
                item[i].soldOut.SetActive(true);
                item[i].button.enabled = false;
            }
            if((i >= Inventory.Instance.invenSize.Length && PlayerHP.Instance.OxygenSize[i - Inventory.Instance.invenSize.Length]))
            {
                item[i].soldOut.SetActive(true);
                item[i].button.enabled = false;
            }
        }
    }

    public void buyItem(int i)
    {
        if(Inventory.Instance.Money >= item[i].Price)
        {
            if(i == 0)
            {
                Inventory.Instance.invenSize[0] = true;
                Inventory.Instance.FreshSlots();
            }
            else if(i == 1)
            {
                Inventory.Instance.invenSize[0] = true;
                Inventory.Instance.invenSize[1] = true;
                Inventory.Instance.FreshSlots();
            }
            else if(i == 2)
            {
                PlayerHP.Instance.OxygenSize[0] = true;
            }
            else if(i == 3)
            {
                PlayerHP.Instance.OxygenSize[0] = true;
                PlayerHP.Instance.OxygenSize[1] = true;
            }
            else if(i == 4)
            {
                PlayerHP.Instance.OxygenSize[0] = true;
                PlayerHP.Instance.OxygenSize[1] = true;
                PlayerHP.Instance.OxygenSize[2] = true;
            }
            Inventory.Instance.Money -= item[i].Price;
        }
        for(int j = 0; j < item.Length; j++)
        {
            item[j].Price = price[j];
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
