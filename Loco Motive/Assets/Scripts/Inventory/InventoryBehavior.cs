/*****************************************************************************
// File Name :         InventoryBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 18, 2023
//
// Brief Description :  Handles adding objects to the inventory and removing them,
                        as well as updating the inventory
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InventoryBehavior : MonoBehaviour
{
    #region Variables

    #region Visual Aspects
    [Header("Inventory General")]
    [SerializeField] private int inventorySize;
    [SerializeField] private Sprite placeholder;
    [SerializeField] private GameObject inventory;
    public GameObject inventoryIcon;
    [SerializeField] private GameObject notebookManager;
    [SerializeField] private GameObject inventoryLarge;
    [SerializeField] private Image largeObject;    
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject movementArrows;
    [SerializeField] private GameObject notebookIcon;
    [SerializeField] private GameObject itemText;
    [SerializeField] private int maxItems;
    [SerializeField] private GameObject visualUpdateNotification;
    public int itemsCollected;

    [Header("Inventory Slot References")]
    public Image[] inventorySpaces;

    #endregion
    public int itemAdded;
    public int itemRemoved;
    public int pieceCounter = 0;
    public bool keyCollected = false;

    private string[] inventoryName;
    public Item[] itemObjects;
    public bool iconIsEnabled;


    private DialogueController dc;
    private AudioManager am;
    private NotebookManager nb;
    private GameObject accusation;


    #endregion


    #region Functions

    /// <summary>
    /// Start is called on the first frame update. It creates the arrays and calls
    /// their population
    /// </summary>
    void Start()
    {
        inventoryName = new String[inventorySize];
        accusation = GameObject.Find("Hunter(AccusationDl)");
        if (accusation != null)
        {
            accusation.SetActive(false);
        }
        

        PopulateArrays();

        itemText.SetActive(false);

        dc = FindObjectOfType<DialogueController>();
        am = FindObjectOfType<AudioManager>();
        nb = FindObjectOfType<NotebookManager>();

        visualUpdateNotification.SetActive(false);

        if (!iconIsEnabled)
        {
            inventoryIcon.SetActive(false);
        }
        if (!nb.iconIsEnabled)
        {
            notebookIcon.SetActive(false);
        }
    }

    /// <summary>
    /// Opens the inventory Screen
    /// </summary>
    public void OpenInventory()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        notebookManager.GetComponent<NotebookManager>().CloseNotebook();
        inventory.SetActive(true);
        UpdateInventory();
        inventoryIcon.SetActive(false);
        map.SetActive(false);
        movementArrows.SetActive(false);
        notebookIcon.SetActive(false);
        dc.isTalking = true;
    }

    /// <summary>
    /// Closes the inventory Screen
    /// </summary>
    public void CloseInventory()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        inventory.SetActive(false);
        if (iconIsEnabled)
        {
            inventoryIcon.SetActive(true);
        }
        map.SetActive(true);
        movementArrows.SetActive(true);
        if (nb.iconIsEnabled)
        {
            notebookIcon.SetActive(true);
        }
        dc.isTalking = false;
    }

    /// <summary>
    /// Opens the zoomed in sprite view
    /// </summary>
    /// <param name="i">the object to zoom in on</param>
    public void OpenLargeView(int i)
    {
        if (am != null)
        {
            am.Play("Click");
        }
        inventoryLarge.SetActive(true);
        largeObject.sprite = inventorySpaces[i-1].sprite;
    }

    /// <summary>
    /// Closes the zoomed in sprite view
    /// </summary>
    public void CloseLargeView()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        inventoryLarge.SetActive(false);
        largeObject.sprite = placeholder;
    }

    /// <summary>
    /// Fills all items in the arrays. Fills the visual array with placeholders
    /// and fills the Image array with the corresponding slots
    /// </summary>
    private void PopulateArrays()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySpaces[i].sprite = placeholder;
        }

        for (int i = 0; i < inventorySize; i++)
        {
            inventoryName[i] = "";
        }
    }

    /// <summary>
    /// Takes an object being added to the inventory and adds it to the inventory,
    /// then calls updating it
    /// </summary>
    /// <param name="itemIndex">The item number to be added</param>
    public void AddItemToInventory(string name)
    {
        //Temporary variables
        int currIndex = 0;
        bool emptySlotFound = false;
        //Sprite newItem;
        //Items newName;

        Item itemToAdd = Array.Find(itemObjects, itemToAdd => itemToAdd.itemName == name);

        //Runs until it finds an empty slot or knows the inventory is full
        while (!emptySlotFound && currIndex < inventorySize)
        {
            //If the current index is empty, empty slot found
           
            if(inventorySpaces[currIndex].sprite == placeholder)
            {
                emptySlotFound = true;
            }
            //Otherwise check the next spot
            else
            {
                currIndex++;

            }
        }

        if (emptySlotFound)
        {
            //Add the new image to the visual inventory
            inventorySpaces[currIndex].sprite = itemToAdd.objectImage;
            inventoryName[currIndex] = itemToAdd.itemName;
            StartCoroutine(NotifyUser());
            itemsCollected++;

            if(name == "Key")
            {
                keyCollected = true;
            }
        }

        if (name == "Piece1")
        {
            pieceCounter++;
        }

        else if (name == "Piece2")
        {
            pieceCounter++;
        }
        //Checks if both pieces of CombineObject puzzle are collected
        if (pieceCounter == 2)
        {
            RemoveItemFromInventory("Piece1");
            RemoveItemFromInventory("Piece2");
            pieceCounter = 0;
            //Assign Full Note's ItemIndex when it is added
            AddItemToInventory("FullPaper");
        }
        //Update the Inventory to match its current state
        UpdateInventory();
        if (itemsCollected >= maxItems)
        {
            StartCoroutine(AllItemsCollected());
            accusation.SetActive(true);
        }
    }

    /// <summary>
    /// Removes an object from the inventory given a numeric representation of its
    /// name, then calls updating it
    /// </summary>
    /// <param name="name"></param>
    public void RemoveItemFromInventory(String name)
    {
        //Temporary variables
        int currIndex = 0;
        bool itemFound = false;

        //Runs until index would be out of bounds or a match is found
        while(!itemFound && currIndex < inventorySize)
        {
            //If a match is found
            if(inventoryName[currIndex] == name)
            {
                //Stop the search and remove the item
                itemFound = true;
                inventorySpaces[currIndex].sprite = placeholder;
            }
            //Otherwise check the next spot
            else
            {
                currIndex++;
            }
        }

        //Update the Inventory to match its current state
        UpdateInventory();

    }


    /// <summary>
    /// Updates the images in the inventory to match the current inventory's status
    /// </summary>
    private void UpdateInventory()
    {
        for(int i=0; i<inventorySize; i++)
        {
            //inventorySpaces[i].sprite = inventoryVi[i];
        }
    }

    /// <summary>
    /// Notifies the player when all physical items have been collected
    /// </summary>
    /// <returns>how long it waits for</returns>
    IEnumerator AllItemsCollected()
    {
        itemText.SetActive(true);
        yield return new WaitForSeconds(3f);
        itemText.SetActive(false);
    }

    IEnumerator NotifyUser()
    {
        if (am != null)
        {
            am.Play("InventoryUpdate");
        }
        visualUpdateNotification.SetActive(true);
        yield return new WaitForSeconds(3);
        visualUpdateNotification.SetActive(false);
    }

    #endregion
}
