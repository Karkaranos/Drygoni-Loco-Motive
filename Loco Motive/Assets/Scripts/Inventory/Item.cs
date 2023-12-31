/*****************************************************************************
// File Name :         Item.cs
// Author :            Cade R. Naylor
// Creation Date :     October 23, 2023
//
// Brief Description :  Creates the Item class for inventory items. Stores name and
image
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite objectImage;
    public Sprite largeObjectImage;
}
