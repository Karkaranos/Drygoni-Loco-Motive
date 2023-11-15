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
    [HideInInspector]
    public int pageCount;

    [HideInInspector]
    public int timelineCount;

    //public Sprite[] image;
    [HideInInspector]
    public bool[,] contentVisible;
    [HideInInspector]
    public bool[] timelineVisible;
    public Sprite empty;

    [SerializeField]
    private GameObject visualUpdateNotification;

    private AudioManager am;

    public string[] timelineContent;
    public Page[] pageContent;

    public int ITEMS_PER_PAGE = 7;
    #endregion

    #region Functions

    /// <summary>
    /// Start initializes the arrays and assigns data to each location
    /// </summary>
    void Start()
    {
        pageCount = pageContent.Length + (int)Mathf.Ceil(timelineContent.Length / 5);
        timelineCount = timelineContent.Length;
        //image = new Sprite[pageCount];
        timelineVisible = new bool[timelineCount];
        contentVisible = new bool[pageCount, ITEMS_PER_PAGE];

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainScene")
        {
            for (int i = 0; i < ITEMS_PER_PAGE; i++)
            {
                for (int j = 5; j < pageCount; j++)
                {
                    contentVisible[j, i] = false;
                }
            }

            for (int i = 0; i < timelineCount-1; i++)
            {
                timelineVisible[i] = false;
            }

            for(int i=1; i<5; i++)
            {
                BasicInformationVisible(i);
            }
            AdvancedInformationVisible(0);


        }


        visualUpdateNotification.SetActive(false);

        am = FindObjectOfType<AudioManager>();

        //RevealEvent(4);
        //RevealEvent(3);
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
        StartCoroutine(NotifyUser());
    }

    /// <summary>
    /// Reveals all information about a page
    /// </summary>
    /// <param name="pageNumber">the page number to reveal info on</param>
    public void AdvancedInformationVisible(int pageNumber)
    {
        for (int i = 0; i < ITEMS_PER_PAGE; i++)
        {
            contentVisible[pageNumber, i] = true;
        }
        StartCoroutine(NotifyUser());
    }

    /// <summary>
    /// Reveals an event
    /// </summary>
    /// <param name="eventNumber">the event to reveal</param>
    public void RevealEvent(int eventNumber)
    {
        timelineVisible[eventNumber] = true;
        StartCoroutine(NotifyUser());
    }

    IEnumerator NotifyUser()
    {
        if(am != null)
        {
            am.Play("NotebookUpdate");
        }
        visualUpdateNotification.SetActive(true);
        yield return new WaitForSeconds(3);
        visualUpdateNotification.SetActive(false);
    }

    #endregion
}
