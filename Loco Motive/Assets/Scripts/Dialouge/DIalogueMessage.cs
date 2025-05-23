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
    public Sprite[] Portrait;
    public bool hasRead = false;
    public bool correctAnswer = false;
    public List<int> messagesToMarkHasRead;
    public List<GameObject> CharacterOn;
    public List<GameObject> CharacterOff;
    public bool AddInventoryItem;
    public string InventoryItem;
    public bool conditionForTutorialProgression;
    public bool updatesTimeline;
    public int timelineEventNum;
    public bool revealInfoNotebook;
    public int pageInfoToReveal;
    public bool mainBranching;
    public bool knifeDialogue = false;
    public bool changesScene;
    public string scene;
}
