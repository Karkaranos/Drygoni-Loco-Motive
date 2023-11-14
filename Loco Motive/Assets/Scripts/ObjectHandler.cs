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
    public bool updatesTimeline;
    public int timelineEventNum;
    public List<GameObject> CharacterOn;
    public List<GameObject> CharacterOff;
    public bool noDestroy;
    public bool fullyRevealsNotebookPage;
    private int teaCount;
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
        if(gameObject.tag == "Bed")
        {
            TutorialManager tm = FindObjectOfType<TutorialManager>();
            tm.StartGame();
        }
        if(gameObject.tag == "TeaCupRequirement")
        {
            teaCount++;
            if(teaCount == 2)
            {
                ncm.AdvancedInformationVisible(6);
            }
        }
        if (canBePickedUp)
        {
            ib.AddItemToInventory(itemID);
            LargeObjectView();
        }

        if (CharacterOn.Count != 0)
        {
            for (int i = 0; i < CharacterOn.Count; i++)
            {
                CharacterOn[i].SetActive(true);
            }
        }

        if (CharacterOff.Count != 0)
        {
            for (int i = 0; i < CharacterOff.Count; i++)
            {
                CharacterOff[i].SetActive(true);
            }
        }

        if (updatesNotebook)
        {
            ncm.BasicInformationVisible(pageNumber);
            LargeObjectView();
        }
        if (fullyRevealsNotebookPage)
        {
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
            LargeObjectView();
        }
        if (updatesTimeline == true)
        {
            ncm.RevealEvent(timelineEventNum);
        }
        if(tag == "body")
        {
            updatesNotebook = false;
        }
        if (!isTrain)
        {
            if (!noDestroy)
            {
                Destroy(gameObject);
            }

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
