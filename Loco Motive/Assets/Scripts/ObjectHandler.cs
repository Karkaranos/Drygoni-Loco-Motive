/*****************************************************************************
// File Name :         ObjectHandler.cs
// Author :            Cade R. Naylor
// Creation Date :     September 20, 2023
//
// Brief Description :  Handles object interactions on click
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public bool canBePickedUp;
    public bool updatesNotebook;
    private InventoryBehavior ib;
    private NotebookContentManager ncm;
    private NotebookManager nm;
    public int itemID;
    [SerializeField] private int pageNumber;
    // Start is called before the first frame update
    void Start()
    {
        ib = GameObject.Find("InventoryManager").GetComponent<InventoryBehavior>();
        ncm = GameObject.Find("NotebookManager").GetComponent<NotebookContentManager>();
        nm = GameObject.Find("NotebookManager").GetComponent<NotebookManager>();

    }

    public void Interact()
    {
        if (canBePickedUp)
        {
            ib.AddItemToInventory(itemID);
        }
        if (updatesNotebook)
        {
            ncm.BasicInformationVisible(pageNumber);
            nm.GetPageInformation();
        }
        Destroy(gameObject);
    }
}
