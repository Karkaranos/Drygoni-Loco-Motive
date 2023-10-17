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
        //Progresses dialogue if not in an interrogation or accusation
        if (DC.interrogating == false)
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
                CC.dc.UpdateScreen(DC.currentDialogue.AllMessages[DC.currentDialogue.currMessage]);
            }

            else
            {
                DC.StopDialogue();
            }
        }

        //Progresses dialogue if in an interrogation
        else if (DC.interrogating == true && DC.accusing == false)
        {
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
                //If hasRead is false, sets hasRead to true and adds 1 to currCounter
                if (DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead == false)
                {
                    DC.currentInterrogation.AllMessages[DC.currentInterrogation.currMessage].hasRead = true;
                    DC.currentInterrogation.currCounter++;
                    //If currCounter is equal to maxCounter, ends interrogation
                    if (DC.currentInterrogation.currCounter == DC.currentInterrogation.maxCounter)
                    {
                        DC.StopDialogue();
                    }
                    //If currCounter isn't equal to maxCounter, goes back to branches of interrogation
                    else
                    {
                        DC.currentInterrogation.currMessage = 0;
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
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                }

                //If correctAnswer is false, 
                else if (DC.currentAccusation.AllMessages[DC.currentAccusation.currMessage].correctAnswer == false)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                }
            }
        }
    }
}
 