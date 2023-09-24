using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBehavior : MonoBehaviour
{
    public int spaceID;
    private InventoryBehavior ib;
    [SerializeField] private GameObject ibObject;

    public void Start()
    {
        ib = ibObject.GetComponent<InventoryBehavior>();
    }

    public void OnClick()
    {
        ib.OpenLargeView(spaceID);
    }
}
