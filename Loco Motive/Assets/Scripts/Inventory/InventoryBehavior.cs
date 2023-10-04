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


public class InventoryBehavior : MonoBehaviour
{
    #region Variables

    #region Visual Aspects
    [Header("Inventory General")]
    [SerializeField] private int inventorySize;
    [SerializeField] private Sprite placeholder;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject inventoryIcon;
    [SerializeField] private GameObject notebookManager;
    [SerializeField] private GameObject inventoryLarge;
    [SerializeField] private Image largeObject;


    [Header("Inventory Potential Contents")]
    [SerializeField] private Sprite item1;
    [SerializeField] private Sprite item2;
    [SerializeField] private Sprite item3;
    [SerializeField] private Sprite item4;
    [SerializeField] private Sprite item5;

    [Header("Inventory Slot References")]
    [SerializeField] private Image slot1;
    [SerializeField] private Image slot2;
    [SerializeField] private Image slot3;
    [SerializeField] private Image slot4;
    [SerializeField] private Image slot5;

    #endregion

    public int itemAdded;
    public int itemRemoved;
    public int pieceCounter = 0;
    public bool keyCollected = false;

    private Sprite[] inventoryVisual;
    private Image[] inventorySpaces;
    private Items[] inventoryName;
    public enum Items { EMPTY, ITEM1, ITEM2, ITEM3, ITEM4, ITEM5 }

    #endregion


    #region Functions



    /// <summary>
    /// Start is called on the first frame update. It creates the arrays and calls
    /// their population
    /// </summary>
    void Start()
    {
        inventoryVisual = new Sprite[inventorySize];
        inventorySpaces = new Image[inventorySize];
        inventoryName = new Items[inventorySize];

        PopulateArrays();

        CloseInventory();

    }

    /// <summary>
    /// Opens the inventory Screen
    /// </summary>
    public void OpenInventory()
    {
        notebookManager.GetComponent<NotebookManager>().CloseNotebook();
        inventory.SetActive(true);
        UpdateInventory();
        inventoryIcon.SetActive(false);
    }

    /// <summary>
    /// Closes the inventory Screen
    /// </summary>
    public void CloseInventory()
    {
        inventory.SetActive(false);
        inventoryIcon.SetActive(true);
    }

    public void OpenLargeView(int i)
    {
        inventoryLarge.SetActive(true);
        largeObject.sprite = inventoryVisual[i-1];
    }

    public void CloseLargeView()
    {
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
            inventoryVisual[i] = placeholder;
        }

        //See if can be simplified?
        inventorySpaces[0] = slot1;
        inventorySpaces[1] = slot2;
        inventorySpaces[2] = slot3;
        inventorySpaces[3] = slot4;
        inventorySpaces[4] = slot5;


        for (int i = 0; i < inventorySize; i++)
        {
            inventoryName[i] = Items.EMPTY;
        }
    }

    /// <summary>
    /// Takes an object being added to the inventory and adds it to the inventory,
    /// then calls updating it
    /// </summary>
    /// <param name="itemIndex">The item number to be added</param>
    public void AddItemToInventory(int itemIndex)
    {
        //Temporary variables
        int currIndex = 0;
        bool emptySlotFound = false;
        Sprite newItem;
        Items newName;

        //Runs until it finds an empty slot or knows the inventory is full
        while (!emptySlotFound && currIndex < inventorySize)
        {
            //If the current index is empty, empty slot found
            if(inventoryVisual[currIndex] == placeholder)
            {
                emptySlotFound = true;
            }
            //Otherwise check the next spot
            else
            {
                currIndex++;
            }
        }

        //See if can simplify later
        switch (itemIndex)
        {
            case 1:
                newItem = item1;
                newName = Items.ITEM1;
                break;
            case 2:
                newItem = item2;
                newName = Items.ITEM2;
                keyCollected = true;
                break;
            case 3:
                newItem = item3;
                newName = Items.ITEM3;
                //Change where pieceCounter++ is to where torn paper pieces are
                pieceCounter++;
                break;
            case 4:
                newItem = item4;
                newName = Items.ITEM4;
                //Change where pieceCounter++ is to where torn paper pieces are
                pieceCounter++;
                break;
            default:
                newItem = item5;
                newName = Items.ITEM5;
                break;
        }

        //Add the new image to the visual inventory
        inventoryVisual[currIndex] = newItem;
        inventoryName[currIndex] = newName;

        //Checks if both pieces of CombineObject puzzle are collected
        if (pieceCounter == 2)
        {
            RemoveItemFromInventory(Items.ITEM3);
            RemoveItemFromInventory(Items.ITEM4);
            pieceCounter = 0;
            //Assign Full Note's ItemIndex when it is added
            AddItemToInventory(5);
        }
        //Update the Inventory to match its current state
        UpdateInventory();
    }

    /// <summary>
    /// Removes an object from the inventory given a numeric representation of its
    /// name, then calls updating it
    /// </summary>
    /// <param name="name"></param>
    public void RemoveItemFromInventory(Items name)
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
                inventoryVisual[currIndex] = placeholder;
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
            inventorySpaces[i].sprite = inventoryVisual[i];
        }
    }

    #endregion
}
