using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookContentManager : MonoBehaviour
{
    [SerializeField] private int pageCount;
    public string[,] notebookContent;
    // Start is called before the first frame update
    void Start()
    {
        notebookContent = new string[pageCount, 6];

        notebookContent[0, 0] = "Evidence 1";
        notebookContent[0, 1] = "Item 1 Description";
        notebookContent[0, 2] = "This item was found in a car. More sample text here.";
        notebookContent[0, 3] = "More sample text describing the object, its location. Maybe this reveals after interrogating the right suspect.";
        notebookContent[0, 4] = "Potential Suspect\nAnother Potential Suspect";
        notebookContent[0, 5] = "Suspects for Item";

        notebookContent[1, 0] = "Evidence 2";
        notebookContent[1, 1] = "Item 2 Description";
        notebookContent[1, 2] = "This item was found in the bathroom. More sample text here.";
        notebookContent[1, 3] = "More sample text describing the object, its location. Maybe this reveals after interrogating the right suspect. To differentiate this from the previous sample, here is more text.";
        notebookContent[1, 4] = "Potential Suspect\nAnother Potential Suspect\nAnother Potential Suspect";
        notebookContent[1, 5] = "Suspects for Item";


    }

}
