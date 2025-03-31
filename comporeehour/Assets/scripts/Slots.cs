using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    private Item _item;
    public Item item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            if(_item != null)
            {
                image.color = Color.white;
                image.sprite = _item.image;
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
    public Image image;
}
