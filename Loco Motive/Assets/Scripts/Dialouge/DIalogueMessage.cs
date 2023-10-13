using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogueMessage
{
    public string Text;
    public Constants.Names Names;
    public List<Choice> Branch;
    public int NextTextOverride;
    public bool EndDialogue;
    public Sprite Portrait;
    public bool hasRead = false;
}
