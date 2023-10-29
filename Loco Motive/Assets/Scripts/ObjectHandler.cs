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
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    public bool canBePickedUp;
    public bool updatesNotebook;
    private InventoryBehavior ib;
    private NotebookContentManager ncm;
    private NotebookManager nm;
    public string itemID;
    [SerializeField] private int pageNumber;
    public bool updatesUI;
    public string scriptToUpdate;
    public Image largeView;
    public GameObject largeViewObj;
    public bool isTrain;
    public Sprite largeViewSprite;
    // Start is called before the first frame update
    void Start()
    {
        ib = GameObject.Find("InventoryManager").GetComponent<InventoryBehavior>();
        ncm = GameObject.Find("NotebookManager").GetComponent<NotebookContentManager>();
        nm = GameObject.Find("NotebookManager").GetComponent<NotebookManager>();

    }

    /// <summary>
    /// Adds objects to the inventory or notebook if they can be picked up
    /// </summary>
    public void Interact()
    {
        if (canBePickedUp)
        {
            ib.AddItemToInventory(itemID);
            LargeObjectView();
        }
        if (updatesNotebook)
        {
            ncm.BasicInformationVisible(pageNumber);
            nm.GetPageInformation();
            ncm.AdvancedInformationVisible(pageNumber);
            LargeObjectView();
        }
        if (updatesUI)
        {
            if(scriptToUpdate == "ib")
            {
                ib.iconIsEnabled = true;
                ib.inventoryIcon.SetActive(true);
            }
            if(scriptToUpdate == "nm")
            {
                nm.iconIsEnabled = true;
                nm.notebookIcon.SetActive(true);
            }
        }
        if (!isTrain)
        {
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ILikeTrains");
        }
    }

    public void LargeObjectView()
    {
        largeViewObj.SetActive(true);
        largeView.sprite = largeViewSprite;
    }

}
