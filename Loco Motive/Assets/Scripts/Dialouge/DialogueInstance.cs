using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstance : MonoBehaviour
{
    public int currMessage;
    public List<DialogueMessage> AllMessages;
    public DialogueController dc;
    public bool canInterrogate = false;
    public bool canAccuse = false;
    public InterrogationInstance thisInterrogation;
    public AccusationInstance thisAccusation;
    // Start is called before the first frame update
    void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Starts dialogue
    public void StartText(int startMessage = 0)
    {
        dc = FindObjectOfType<DialogueController>();
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
