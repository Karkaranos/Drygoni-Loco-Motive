/*****************************************************************************
// File Name :         WriteableContentManager.cs
// Author :            Cade R. Naylor
// Creation Date :     June 13, 2024
//
// Brief Description :  Handles reading, storing, and saving user input for 
                        writeable notebook pages
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteableContentManager : MonoBehaviour
{
    public string[] pageTitles;
    public string[] pageContent;
    [SerializeField] string contentFilePath;
    [SerializeField] string titleFilePath;
    NotebookManager nm;
    NotebookContentManager ncm;

    [HideInInspector]
    public char[] seperators = { '~' };
    // Start is called before the first frame update
    void Start()
    {
        contentFilePath = Application.streamingAssetsPath + "/" + contentFilePath + ".txt";
        titleFilePath = Application.streamingAssetsPath + "/" + titleFilePath + ".txt";
        pageTitles = File.ReadAllLines(titleFilePath)[0].Split(seperators);
        pageContent = File.ReadAllLines(contentFilePath)[0].Split(seperators);
        //TryReadingData(pageContent, contentFilePath);
        //TryReadingData(pageTitles, titleFilePath);
        nm = GetComponent<NotebookManager>();
        ncm = GetComponent<NotebookContentManager>();

    }

    public string[] LoadPageInformation(int startingPage)
    {
        string[] results = new string[4];
        results[0] = pageTitles[2 * startingPage];
        results[1] = pageContent[2 * startingPage];
        results[2] = pageTitles[2 * startingPage + 1];
        results[3] = pageContent[2 * startingPage + 1];
        /*for(int i=0; i<2; i++)
        {
            results[index] = pageTitles[startingPage];
            index++;
            results[index] = pageContent[startingPage];
            index++;
            startingPage++;

        }*/

        return results;
    }

    public void UpdateFirstTitle(string s)
    {
        pageTitles[2 * (nm.currentPage - ncm.nonwriteablePageCount)] = s;
    }
    public void UpdateSecondTitle(string s)
    {
        pageTitles[2 * (nm.currentPage - ncm.nonwriteablePageCount)+1] = s;
    }

    public void UpdateFirstContent(string s)
    {
        pageContent[2 * (nm.currentPage - ncm.nonwriteablePageCount)] = s;
    }
    public void UpdateSecondContent(string s)
    {
        pageContent[2 * (nm.currentPage - ncm.nonwriteablePageCount) + 1] = s;
    }

    public void UpdateSavedData()
    {

        File.WriteAllText(titleFilePath, pageTitles[0] + "~");
        for(int i=1; i<pageTitles.Length; i++)
        {
            File.AppendAllText(titleFilePath, pageTitles[i] + "~");
        }
        File.WriteAllText(contentFilePath, pageContent[0] + "~");
        for (int i = 1; i < pageContent.Length; i++)
        {
            File.AppendAllText(contentFilePath, pageContent[i] + "~");
        }
    }

    private void TryReadingData(string[] sArr, string potentialFile)
    {
        try
        {
            sArr = File.ReadAllLines(potentialFile)[0].Split(seperators);
        }
        catch
        {
            throw new System.Exception(potentialFile + "Not Found");
        }
    }

}
