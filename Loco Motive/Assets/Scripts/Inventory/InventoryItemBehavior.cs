/*****************************************************************************
// File Name :         InventoryItemBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 22, 2023
//
// Brief Description :  Calls functions from InventoryBehavior to zoom in image
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBehavior : MonoBehaviour
{
    public int spaceID;
    private InventoryBehavior ib;
    [SerializeField] private GameObject ibObject;

    /// <summary>
    /// Start is called before the first frame update. It gets a reference to 
    /// Inventory Behavior
    /// </summary>
    public void Start()
    {
        ib = ibObject.GetComponent<InventoryBehavior>();
    }

    /// <summary>
    /// Calls OpenLargeView of the current inventory space
    /// </summary>
    public void OnClick()
    {
        ib.OpenLargeView(spaceID);
    }
}
