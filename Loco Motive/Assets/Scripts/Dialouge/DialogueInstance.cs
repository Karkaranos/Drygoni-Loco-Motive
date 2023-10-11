using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstance : MonoBehaviour
{
    public int currMessage;
    public List<DIalogueMessage> AllMessages;
    public DialogueController dc;
    // Start is called before the first frame update
    void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProgText(int startMessage = 0)
    {
        currMessage = startMessage;
        if (AllMessages[currMessage].Branch.Count == 0)
        {
            dc.UpdateScreen(AllMessages[currMessage]);
        }
        else
        {
            dc.UpdateScreen(AllMessages[currMessage]);
        }
    }

    public void UpdateDisplay()
    {
        
    }
}
