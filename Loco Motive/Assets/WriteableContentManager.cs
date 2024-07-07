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

    }

    public string[] LoadPageInformation(int startingPage)
    {
        string[] results = new string[4];
        int index = 0;
        for(int i=0; i<2; i++)
        {
            results[index] = pageTitles[startingPage];
            index++;
            results[index] = pageContent[startingPage];
            index++;
            startingPage++;

        }

        return results;
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
