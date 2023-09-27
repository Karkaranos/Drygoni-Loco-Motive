/*****************************************************************************
// File Name :         NotebookBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 18, 2023
//
// Brief Description :  Handles updating the notebook with what the player knows
                        and switching pages
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotebookManager : MonoBehaviour
{
    #region Variables

    [Header("Notebook General")]
    public int currentPage;
    [SerializeField] private GameObject notebook;
    [SerializeField] private GameObject notebookIcon;
    [SerializeField] private GameObject inventoryManager;
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject lastPage;

    [Header("Notebook Content")]
    [SerializeField] private TMP_Text pageTitle;
    [SerializeField] private TMP_Text imageCaption;
    [SerializeField] private TMP_Text bodyText1;
    [SerializeField] private TMP_Text bodyText2;
    [SerializeField] private TMP_Text bodyText3;
    [SerializeField] private TMP_Text subHeader;
    [SerializeField] private Image photo;
    [SerializeField] private TMP_Text pageNumber;
    private TMP_Text[] content;


    private static int TEXT_ITEMS_PER_PAGE = 6;

    private NotebookContentManager ncm;
    #endregion

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        ncm = GetComponent<NotebookContentManager>();

        currentPage = 0;

        content = new TMP_Text[TEXT_ITEMS_PER_PAGE];

        content[0] = pageTitle;
        content[1] = imageCaption;
        content[2] = bodyText1;
        content[3] = bodyText2;
        content[4] = bodyText3;
        content[5] = subHeader;

        notebook.SetActive(false);

    }

    /// <summary>
    /// Opens the notebook Screen and gets its information
    /// </summary>
    public void OpenNotebook()
    {
        notebook.SetActive(true);
        inventoryManager.GetComponent<InventoryBehavior>().CloseInventory();
        GetPageInformation();
        notebookIcon.SetActive(false);
    }

    /// <summary>
    /// Closes the notebook Screen
    /// </summary>
    public void CloseNotebook()
    {
        notebook.SetActive(false);
        notebookIcon.SetActive(true);
    }

    /// <summary>
    /// Takes the current page and checks what information is available, then sets
    /// the current information to what the player knows for each item
    /// </summary>
    public void GetPageInformation()
    {
        //Sets the visible content
        for(int i=0; i<TEXT_ITEMS_PER_PAGE; i++)
        {
            //If the player can see the content, set its text to the provided info
            if (ncm.contentVisible[currentPage, i])
            {
                content[i].text = ncm.notebookContent[currentPage, i];
            }
            //Otherwise, indicate the player does not know the information
            else
            {
                content[i].text = "???";
            }
        }

        if(ncm.contentVisible[currentPage, 6])
        {
            photo.sprite = ncm.image[currentPage];
        }
        else
        {
            photo.sprite = ncm.empty;
        }

        pageNumber.text = currentPage + 1 + " of " + ncm.pageCount;

        if(currentPage == 0)
        {
            lastPage.SetActive(false);
        }
        else
        {
            lastPage.SetActive(true);
        }

        if(currentPage+1 < ncm.pageCount)
        {
            nextPage.SetActive(true);
        }
        else
        {
            nextPage.SetActive(false);
        }

    }

    /// <summary>
    /// Moves the player to the next notebook page, if applicable
    /// </summary>
    public void NextPage()
    {
        if(currentPage < ncm.pageCount-1)
        {
            currentPage++;
            GetPageInformation();
        }
    }

    public void PreviousPage()
    {
        if(currentPage > 0)
        {
            currentPage--;
            GetPageInformation();
        }
    }


    public void RevealComplexInformation(int pageNumber)
    {
        ncm.AdvancedInformationVisible(pageNumber);
    }
    #endregion
}
