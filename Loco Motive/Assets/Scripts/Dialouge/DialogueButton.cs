using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueButton : MonoBehaviour
{
    public int NextDialogue;
    public TMP_Text ButtonText;
    public ClickController CC;
    
    public void UpdateButton(int i, string s)
    {
        ButtonText.text = s;
        NextDialogue = i;
    }

    public void ProgText()
    {
        CC.CurrentDialogue.currMessage = NextDialogue;
        CC.dc.UpdateScreen(CC.CurrentDialogue.AllMessages[CC.CurrentDialogue.currMessage]);
    }
}
