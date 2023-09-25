/*****************************************************************************
// File Name :         NotebookContentManager.cs
// Author :            Cade R. Naylor
// Creation Date :     September 18, 2023
//
// Brief Description :  Handles storing all content for the notebook
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookContentManager : MonoBehaviour
{
    #region Variables
    public int pageCount;
    public string[,] notebookContent;
    public Sprite[] image;
    public bool[,] contentVisible;
    public Sprite empty;
    [SerializeField] private Sprite item1;
    [SerializeField] private Sprite item2;

    public int ITEMS_PER_PAGE = 7;
    #endregion

    #region Functions
    
    /// <summary>
    /// Start initializes the arrays and assigns data to each location
    /// </summary>
    void Start()
    {
        notebookContent = new string[pageCount, ITEMS_PER_PAGE-1];
        image = new Sprite[pageCount];
        contentVisible = new bool[pageCount,ITEMS_PER_PAGE];

        for(int i=0; i<ITEMS_PER_PAGE; i++)
        {
            for(int j=0; j<pageCount; j++)
            {
                contentVisible[j, i] = false;
            }
        }

        //Page 1 content
        notebookContent[0, 0] = "Evidence 1";
        notebookContent[0, 1] = "A leaf of a toxic plant";
        notebookContent[0, 2] = "This item was found in Suspect 1's car behind a locked box.";
        notebookContent[0, 3] = "After talking to Suspect 1, we learned they found this item in the kitchen. I should investigate the kitchen to see if there is more information about this item.";
        notebookContent[0, 4] = "Potential Suspect 1";
        notebookContent[0, 5] = "Suspects for Item";

        //Page 2 content
        notebookContent[1, 0] = "Evidence 2";
        notebookContent[1, 1] = "A very scary monster";
        notebookContent[1, 2] = "This item was found in the kitchen where Suspect 1 said they found the leaf.";
        notebookContent[1, 3] = "More sample text describing the object, its location. Maybe this reveals after interrogating the right suspect. To differentiate this from the previous sample, here is more text.";
        notebookContent[1, 4] = "Potential Suspect\nAnother Potential Suspect\nAnother Potential Suspect";
        notebookContent[1, 5] = "Suspects for Item";

        //Assigning visuals
        image[0] = item1;
        image[1] = item2;


    }

    public void BasicInformationVisible(int pageNumber)
    {
        for (int i = 0; i < ITEMS_PER_PAGE; i++)
        {
            if (i != 3)
            {
                contentVisible[pageNumber, i] = true;
            }
        }
    }

    #endregion
}
