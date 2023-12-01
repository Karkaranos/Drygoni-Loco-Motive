/*****************************************************************************
// File Name :         NotebookBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 18, 2023
//
// Brief Description :  Handles updating the notebook with what the player knows
                        and switching pages
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NotebookManager : MonoBehaviour
{
    #region Variables

    [Header("Notebook General")]
    public int currentPage;
    [SerializeField] private GameObject notebook;
    [SerializeField] private GameObject notebookContentPage;
    [SerializeField] private GameObject notebookTimelinePage;
    public GameObject notebookIcon;
    [SerializeField] private GameObject inventoryManager;
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject lastPage;
    [SerializeField] private GameObject movementArrows;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject inventoryIcon;

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

    [Header("Timeline Content")]
    public TMP_Text[] eventText;

    private static int TEXT_ITEMS_PER_PAGE = 6;

    private NotebookContentManager ncm;
    private DialogueController dc;
    private InventoryBehavior ib;
    private AudioManager am;
    public bool iconIsEnabled;
    #endregion

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        ncm = GetComponent<NotebookContentManager>();
        dc = FindObjectOfType<DialogueController>();
        ib = FindObjectOfType<InventoryBehavior>();
        am = FindObjectOfType<AudioManager>();

        currentPage = 0;

        content = new TMP_Text[TEXT_ITEMS_PER_PAGE];

        content[0] = pageTitle;
        content[1] = imageCaption;
        content[2] = bodyText1;
        content[3] = bodyText2;
        content[4] = subHeader;
        content[5] = bodyText3;

        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            for (int i = 0; i < 5; i++)
            {
                RevealInformation(i);
            }
            RevealComplexInformation(0);
            RevealNewTimelineEvent(0);
            RevealNewTimelineEvent(18);
            RevealNewTimelineEvent(19);
        }

    }

    /// <summary>
    /// Opens the notebook Screen and gets its information
    /// </summary>
    public void OpenNotebook()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        notebook.SetActive(true);
        inventoryManager.GetComponent<InventoryBehavior>().CloseInventory();
        GetPageInformation();
        notebookIcon.SetActive(false);
        map.SetActive(false);
        movementArrows.SetActive(false);
        inventoryIcon.SetActive(false);
        dc.isTalking = true;
    }

    /// <summary>
    /// Closes the notebook Screen
    /// </summary>
    public void CloseNotebook()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        notebook.SetActive(false);
        notebookContentPage.SetActive(false);
        notebookTimelinePage.SetActive(false);
        map.SetActive(true);
        movementArrows.SetActive(true);
        if (iconIsEnabled)
        {
            notebookIcon.SetActive(true);
        }
        if (ib.iconIsEnabled)
        {
            inventoryIcon.SetActive(true);
        }

        dc.isTalking = false;
    }

    /// <summary>
    /// Takes the current page and checks what information is available, then sets
    /// the current information to what the player knows for each item
    /// </summary>
    public void GetPageInformation()
    {
        //Sets the content if the current page is NOT a timeline page
        if (currentPage < ncm.pageCount - ncm.timelineCount/5)
        {
            notebookContentPage.SetActive(true);
            notebookTimelinePage.SetActive(false);
            //Sets the visible content
            for (int i = 0; i < TEXT_ITEMS_PER_PAGE; i++)
            {
                //If the player can see the content, set its text to the provided info
                if (ncm.contentVisible[currentPage, i])
                {
                    string[] temp = new string[ncm.ITEMS_PER_PAGE];
                    temp = ncm.pageContent[currentPage].content;
                    content[i].text = temp[i];
                }
                //Otherwise, indicate the player does not know the information
                else
                {
                    content[i].text = "???";
                }
            }

            GameObject.Find("Photo").SetActive(true);

            //If the image should be visible, set it to the stored image
            if (ncm.contentVisible[currentPage, 6])
            {
                photo.sprite = ncm.pageContent[currentPage].photo;
            }
            //Otherwise set it to a blank image
            else
            {
                photo.sprite = ncm.empty;
            }


        }
        //If is a timeline page
        else
        {
            notebookContentPage.SetActive(false);
            notebookTimelinePage.SetActive(true);
            int index = (currentPage - ncm.pageContent.Length) * 5;
            for (int i = 0; i < eventText.Length; i++)
            {
                if (ncm.timelineVisible[index])
                {
                    //THIS IS NULL FIX IT 
                    eventText[i].text = ncm.timelineContent[index];
                }
                else
                {
                    eventText[i].text = "???";
                }
                index++;
            }


        }

        if (currentPage == 0)
        {
            lastPage.SetActive(false);
        }
        else
        {
            lastPage.SetActive(true);
        }

        if (currentPage + 1 < ncm.pageCount)
        {
            nextPage.SetActive(true);
        }
        else
        {
            nextPage.SetActive(false);
        }

        pageNumber.text = currentPage + 1 + " of " + ncm.pageCount;

    }

    /// <summary>
    /// Moves the player to the next notebook page, if applicable
    /// </summary>
    public void NextPage()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        if (currentPage < ncm.pageCount - 1)
        {
            currentPage++;
            GetPageInformation();
        }
    }

    /// <summary>
    /// Moves the player to the previous notebook page, if applicable
    /// </summary>
    public void PreviousPage()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        if (currentPage > 0)
        {
            currentPage--;
            GetPageInformation();
        }
    }

    /// <summary>
    /// Calls revealing all information for a page
    /// </summary>
    /// <param name="pageNumber"></param>
    public void RevealComplexInformation(int pageNumber)
    {
        ncm.AdvancedInformationVisible(pageNumber);
    }

    /// <summary>
    /// Calls revealing all information for a page
    /// </summary>
    /// <param name="pageNumber"></param>
    public void RevealInformation(int pageNumber)
    {
        ncm.BasicInformationVisible(pageNumber);
    }

    /// <summary>
    /// Calls revealing an event on the timeline
    /// </summary>
    /// <param name="eventNumber">the event to reveal</param>
    public void RevealNewTimelineEvent(int eventNumber)
    {
        ncm.RevealEvent(eventNumber);
    }
    #endregion
}
