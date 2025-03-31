using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public enum useItemType
    {
        HP,
        OX,
        TR,
        SSUP,
        BSUP,
        HIDE,
        NULL,
    }
    public useItemType UIT;
    public float Weight;
    public float sellPrice;
}
