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
    public int pageCount = 7;
    
    public Sprite[] image;
    public bool[,] contentVisible;
    public Sprite empty;
    [SerializeField] private Sprite item1;
    [SerializeField] private Sprite item2;

    public string[] page1notebookContent = new string[6];
    public string[] page2notebookContent = new string[6];
    public string[] page3notebookContent = new string[6];
    public string[] page4notebookContent = new string[6];
    public string[] page5notebookContent = new string[6];
    public string[] page6notebookContent = new string[6];
    public string[] page7notebookContent = new string[6];
    public string[] page8notebookContent = new string[6];

    public List<string[]> pages = new List<string[]>();

    public int ITEMS_PER_PAGE = 7;
    #endregion

    #region Functions
    
    /// <summary>
    /// Start initializes the arrays and assigns data to each location
    /// </summary>
    void Start()
    {
        print(pageCount);
        image = new Sprite[pageCount];
        contentVisible = new bool[pageCount,ITEMS_PER_PAGE];

        for(int i=0; i<ITEMS_PER_PAGE; i++)
        {
            for(int j=0; j<pageCount; j++)
            {
                contentVisible[j, i] = false;
            }
        }

        pages.Add(page1notebookContent);
        pages.Add(page2notebookContent);
        pages.Add(page3notebookContent);
        pages.Add(page4notebookContent);
        pages.Add(page5notebookContent);
        pages.Add(page6notebookContent);
        pages.Add(page7notebookContent);
        pages.Add(page8notebookContent);

        
        //Assigning visuals
        image[0] = empty;
        image[1] = item1;
        image[2] = item2;

        BasicInformationVisible(0);
        BasicInformationVisible(1);
        BasicInformationVisible(2);
        BasicInformationVisible(3);
        BasicInformationVisible(4);
    }

    /// <summary>
    /// Reveals all information about an object, save its Body Text 2
    /// </summary>
    /// <param name="pageNumber"></param>
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

    /// <summary>
    /// Reveals all information about a page
    /// </summary>
    /// <param name="pageNumber"></param>
    public void AdvancedInformationVisible(int pageNumber)
    {
        for (int i = 0; i < ITEMS_PER_PAGE; i++)
        {
            contentVisible[pageNumber, i] = true;
        }
    }

    #endregion
}
