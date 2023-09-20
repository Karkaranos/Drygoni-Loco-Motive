/*****************************************************************************
// File Name :         NotebookBehavior.cs
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
using TMPro;

public class NotebookManager : MonoBehaviour
{
    #region Variables

    [Header("Notebook General")]
    public int currentPage;

    [Header("Notebook Content")]
    [SerializeField] private TMP_Text pageTitle;
    [SerializeField] private TMP_Text imageCaption;
    [SerializeField] private TMP_Text bodyText1;
    [SerializeField] private TMP_Text bodyText2;
    [SerializeField] private TMP_Text bodyText3;
    [SerializeField] private TMP_Text subHeader;
    [SerializeField] private Image photo;

    private NotebookContentManager ncm;
    #endregion

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        ncm = GetComponent<NotebookContentManager>();

        currentPage = 0;

    }


    private void GetPageInformation()
    {
        pageTitle.text = ncm.notebookContent[currentPage, 0];
        imageCaption.text = ncm.notebookContent[currentPage, 1];
        bodyText1.text = ncm.notebookContent[currentPage, 2];
        bodyText2.text = ncm.notebookContent[currentPage, 3];
        bodyText3.text = ncm.notebookContent[currentPage, 4];
        subHeader.text = ncm.notebookContent[currentPage, 5];
        photo.sprite = ncm.image[currentPage];
    }


    // temp function- remove later
    void Update()
    {
        GetPageInformation();
    }

    #endregion
}
