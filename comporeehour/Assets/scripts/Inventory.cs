using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    public float Speed;

    public List<Item> items;
    public Slots[] slots;
    public GameObject bag;

    public float MaxWeight;
    public float Weight;
    public float Money;

    public Image HPbar;
    public Image OxygenBar;

    public Text Timer;
    public Text WeightText;

    public bool[] invenSize = new bool[2];

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        FreshSlots();
    }

    public void FreshSlots()
    {
        int i = 0;
        Weight = 0;
        for(;i<slots.Length && i< items.Count && Weight + items[i].Weight <= MaxWeight; i++)
        {
            slots[i].item = items[i];
            Weight += items[i].Weight;
        }
        for (; i < slots.Length;i++)
        {
            slots[i].item = null;
        }
        WeightText.text = Weight.ToString() + "/" + MaxWeight.ToString();
    }
    public bool maximumSlots(Item item)
    {
        return slots.Length < items.Count && Weight + item.Weight <= MaxWeight && 
            (slots[3] == null || (invenSize[0] && slots[5] == null) || invenSize[2]);
    }
    public void AddItem(Item item)
    {
        if (maximumSlots(item))
        {
            items.Add(item);
        }
        FreshSlots();
    }
    public void sellItem(bool isbool)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (isbool)
            {
                Money += items[0].sellPrice;
            }
            slots[i].item = null;
            items.RemoveAt(0);
        }
        FreshSlots();
    }
    public void DropItem(int i)
    {
        switch (slots[i].item.UIT)
        {
            case Item.useItemType.HP:
                PlayerHP.Instance.HP += 20;
                break;
            case Item.useItemType.OX:
                PlayerHP.Instance.Oxygen += PlayerHP.Instance.MaxOxygen / 5;
                break;
            case Item.useItemType.TR:
                
                break;
            case Item.useItemType.SSUP:
                StartCoroutine(speedup(3, 3));
                break;
            case Item.useItemType.BSUP:
                StartCoroutine(speedup(6, 5));
                break;
            case Item.useItemType.HIDE:
                StartCoroutine(PlayerHP.Instance.HPdown(3));
                break;
            case Item.useItemType.NULL:
                return;
        }
        EventSystem.current.SetSelectedGameObject(null);
        slots[i].item = null;
        items.RemoveAt(i);
        FreshSlots();
    }
    IEnumerator speedup(float T, float S)
    {
        Speed = S;
        yield return new WaitForSeconds(T);
        Speed = 0;
    }
}
