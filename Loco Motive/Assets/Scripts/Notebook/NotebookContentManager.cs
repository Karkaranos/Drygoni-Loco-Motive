using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookContentManager : MonoBehaviour
{
    [SerializeField] private int pageCount;
    public string[,] notebookContent;
    public Sprite[] image;
    [SerializeField] private Sprite item1;
    [SerializeField] private Sprite item2;


    // Start is called before the first frame update
    void Start()
    {
        notebookContent = new string[pageCount, 6];
        image = new Sprite[pageCount];

        notebookContent[0, 0] = "Evidence 1";
        notebookContent[0, 1] = "A leaf of a toxic plant";
        notebookContent[0, 2] = "This item was found in Suspect 1's car behind a locked box.";
        notebookContent[0, 3] = "After talking to Suspect 1, we learned they found this item in the kitchen. I should investigate the kitchen to see if there is more information about this item.";
        notebookContent[0, 4] = "Potential Suspect 1";
        notebookContent[0, 5] = "Suspects for Item";

        notebookContent[1, 0] = "Evidence 2";
        notebookContent[1, 1] = "A very scary monster";
        notebookContent[1, 2] = "This item was found in the kitchen where Suspect 1 said they found the leaf.";
        notebookContent[1, 3] = "More sample text describing the object, its location. Maybe this reveals after interrogating the right suspect. To differentiate this from the previous sample, here is more text.";
        notebookContent[1, 4] = "Potential Suspect\nAnother Potential Suspect\nAnother Potential Suspect";
        notebookContent[1, 5] = "Suspects for Item";

        image[0] = item1;
        image[1] = item2;


    }

}
