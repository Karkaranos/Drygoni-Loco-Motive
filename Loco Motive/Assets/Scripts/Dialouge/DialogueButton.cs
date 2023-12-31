using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueButton : MonoBehaviour
{
    public int NextDialogue;
    public TMP_Text ButtonText;
    public ClickController CC;
    public DialogueController DC;
    public InventoryBehavior IB;
    public TutorialManager TM;
    public NotebookContentManager NCM;

    public void Start()
    {
        CC = FindObjectOfType<ClickController>();
        DC = FindObjectOfType<DialogueController>();
        IB = FindObjectOfType<InventoryBehavior>();
        TM = FindObjectOfType<TutorialManager>();
        NCM = FindObjectOfType<NotebookContentManager>();
    }
    public void UpdateButton(int i, string s)
    {
        ButtonText.text = s;
        NextDialogue = i;
    }

    public void ProgText()
    {
        if (DC.interrogating == false && DC.accusing == false)
        {
            if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].EndDialogue == false)
            {
                DC.currentDialogue.currMessage = NextDialogue;
                DC.UpdateScreen(DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage]);
            }
            else
            {
                DC.StopDialogue();
            }
        }
        
        else if (DC.interrogating == true && DC.accusing == false)
        {
            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn.Count != 0)
            {
                for (int i = 0; i < DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn.Count; i++)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn[i].SetActive(true);
                }
            }

            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff.Count != 0)
            {
                for (int i = 0; i < DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff.Count; i++)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff[i].SetActive(false);
                }
            }

            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].EndDialogue == false)
            {
                DC.currentInterrogation.currMessage = NextDialogue;
                DC.UpdateScreen(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage]);
            }
            else
            {
                DC.StopDialogue();
            }
        }

        else
        {
            if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].EndDialogue == false)
            {
                DC.currentAccusation.currMessage = NextDialogue;
                DC.UpdateScreen(DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage]);
            }
            else
            {
                DC.StopDialogue();
            }
        }
    }

    //Progress dialogue when hitting continue button
    public void ContinueProgress()
    {
        /*if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].changesScene == true)
        {
            TutorialManager tm = FindObjectOfType<TutorialManager>();
            tm.StartGame();
        }*/
        //Progresses dialogue if not in an interrogation or accusation
        if (DC.interrogating == false && DC.accusing == false)
        {
            if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].EndDialogue == false)
            {
                //Goes to specific line of dialogue if NextTextOverride isn't 0
                if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].NextTextOverride != 0)
                {
                    DC.currentDialogue.currMessage = DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].NextTextOverride;
                }
                else
                {
                    DC.currentDialogue.currMessage++;
                }
                if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOn.Count != 0)
                {
                    for (int i = 0; i < DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOn.Count; i++)
                    {
                        DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOn[i].SetActive(true);
                    }
                }

                if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOff.Count != 0)
                {
                    for (int i = 0; i < DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOff.Count; i++)
                    {
                        DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].CharacterOff[i].SetActive(false);
                        DC.UpdateBoneCounter();
                    }
                }
                CC.dc.UpdateScreen(DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage]);

                if(TM!=null && DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].conditionForTutorialProgression && !DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].hasRead)
                {
                    TM.requirementsToUnlock++;
                    TM.IncreaseRequirement();
                }

                if (TM != null)
                {
                    DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].hasRead = true;
                }

                if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].updatesTimeline == true)
                {
                    NCM.RevealEvent(DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].timelineEventNum);
                }
            }

            else
            {
                DC.StopDialogue();
            }
        }

        //Progresses dialogue if in an interrogation
        else if (DC.interrogating == true && DC.accusing == false)
        {
            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].updatesTimeline == true)
            {
                NCM.RevealEvent(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].timelineEventNum);
            }
            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn.Count != 0)
            {
                for (int i = 0; i < DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn.Count; i++)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOn[i].SetActive(true);
                }
            }

            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff.Count != 0)
            {
                for (int i = 0; i < DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff.Count; i++)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].CharacterOff[i].SetActive(false);
                }
            }

            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].updatesTimeline == true)
            {
                NCM.RevealEvent(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].timelineEventNum);
            }

            //Progresses dialogue if EndDialogue is false
            if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].EndDialogue == false)
            {
                //Goes to specific line of dialogue if NextTextOverride isn't 0
                if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].NextTextOverride != 0)
                {
                    DC.currentInterrogation.currMessage = DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].NextTextOverride;
                }
                else
                {
                    DC.currentInterrogation.currMessage++;
                }
                CC.dc.UpdateScreen(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage]);
            }

            //Runs if EndDialogue is true
            else
            {
                if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].knifeDialogue == true && DC.currentInterrogation.itemCollected == true)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead = true;
                }
                if (DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage].changesScene == true)
                {
                    //SceneManager.LoadScene(2);
                }
                //If hasRead is false, sets hasRead to true and adds 1 to currCounter
                if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead == false)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead = true;
              
                        //for (int i = 0; i < DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].messagesToMarkHasRead.Count; i++)
                        //{
                        //    DC.currentInterrogation.AllMessages[DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].messagesToMarkHasRead[i]].hasRead = true;
                        //}
                    
                    DC.currentInterrogation.currCounter++;

                    if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].revealInfoNotebook)
                    {
                        NotebookManager nm = FindObjectOfType<NotebookManager>();
                        nm.RevealComplexInformation(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].pageInfoToReveal);
                    }

                    if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].AddInventoryItem && DC.currentInterrogation.itemCollected == false)
                    {
                        string s = DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].InventoryItem.ToString();
                        IB.AddItemToInventory(s);
                        DC.currentInterrogation.itemCollected = true;
                    }
                    //If currCounter is equal to maxCounter, ends interrogation
                    if (DC.currentInterrogation.currCounter == DC.currentInterrogation.maxCounter)
                    {
                        DC.interrogating = false;
                        DC.StopDialogue();
                    }
                    //If currCounter isn't equal to maxCounter, goes back to branches of interrogation
                    else
                    {
                        if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].NextTextOverride != 0)
                        {
                            DC.currentInterrogation.currMessage = DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].NextTextOverride;
                        }
                        else
                        {
                            DC.currentInterrogation.currMessage = 0;
                        }
                        CC.dc.UpdateScreen(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage]);
                    }
                }

                //If hasRead is true, returns to branches of interrogation
                else if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead == true)
                {
                    DC.currentInterrogation.currMessage = 0;
                    CC.dc.UpdateScreen(DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage]);
                }
            }
        }

        //Progresses dialogue if in an accusation
        else
        {
            //Progresses dialogue if EndDialogue is false
            if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].EndDialogue == false)
            {
                //Goes to specific line of dialogue if NextTextOverride isn't 0
                if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].NextTextOverride != 0)
                {
                    DC.currentAccusation.currMessage = DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].NextTextOverride;
                }
                else
                {
                    DC.currentAccusation.currMessage++;
                }
                CC.dc.UpdateScreen(DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage]);
            }

            //Runs if EndDialogue is true
            else
            {
                //If correctAnswer is true, 
                if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].correctAnswer == true)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                }

                //If correctAnswer is false, 
                else if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].correctAnswer == false)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(4);
                }
            }
        }
    }
}
 