using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueButton : MonoBehaviour
{
    public int NextDialogue;
    public TMP_Text ButtonText;
    public ClickController CC;
    public DialogueController DC;

    public void Start()
    {
        CC = FindObjectOfType<ClickController>();
        DC = FindObjectOfType<DialogueController>();
    }
    public void UpdateButton(int i, string s)
    {
        ButtonText.text = s;
        NextDialogue = i;
    }

    public void ProgText()
    {
        if (CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage].EndDialogue == false)
        {
            CC.CurrentDialogue.currMessage = NextDialogue;
            CC.dc.UpdateScreen(CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage]);
        }
        else
        {
            DC.StopDialogue();
        }
    }

    public void ContinueProgress()
    {
        if (CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage].EndDialogue == false)
        {
            if (CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage].NextTextOverride != 0)
            {
                CC.CurrentDialogue.currMessage = CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage].NextTextOverride;
            }
            else
            {
                CC.CurrentDialogue.currMessage++;
            }
            CC.dc.UpdateScreen(CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage]);
        }
        else
        {
            DC.StopDialogue();
        }
    }
}
