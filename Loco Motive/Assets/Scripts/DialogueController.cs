using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    private int interrogateEvidence = 0;
    private int interrogateCount = 0;
    private bool askedOne = false;
    private bool askedTwo = false;
    private bool askedThree = false;
    private bool askedFour = false;
    public bool opening = true;
    public bool numPadDialogue = false;
    public bool needNumInfo = false;
    public bool isTalking = false;
    private int currDialogue = 0;
    public int currTalkChar;
    public int strLength;
    private int branchNum;
    public int currSpeaker;
    private bool interrogating = false;

    public TMP_Text DialogueBox;
    public TMP_Text DialogueBoxI;
    public TMP_Text SpeakerName;
    public TMP_Text SpeakerNameI;
    public TMP_Text ButtonTextOne;
    public TMP_Text ButtonTextTwo;
    public TMP_Text ContinueText;
    public TMP_Text QuestionOne;
    public TMP_Text QuestionTwo;
    public TMP_Text QuestionThree;
    public TMP_Text QuestionFour;

    public GameObject DialogueScreen;
    public GameObject InterrogationScreen;
    public GameObject BranchButtons;
    public GameObject BranchButtonsI;
    public GameObject BranchButtonI1;
    public GameObject BranchButtonI2;
    public GameObject BranchButtonI3;
    public GameObject BranchButtonI4;
    public GameObject ContinueButton;
    public GameObject ContinueButtonI;
    public GameObject InterrogateButton;
    public GameObject InventoryButton;
    public GameObject NotebookButton;

    public ClickController cc;
    public StoredDialogue sd;
    public NotebookManager nm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Starts playing dialogue when a character is clicked
    /// </summary>
    public void StartDialogue()
    {
        isTalking = true;
        InventoryButton.SetActive(false);
        NotebookButton.SetActive(false);
        cc.Movement.SetActive(false);
        if (opening == true)
        {
            DialogueScreen.SetActive(true);
            ContinueButton.SetActive(false);
            BranchButtons.SetActive(true);
            InterrogateButton.SetActive(false);
            ButtonTextOne.text = "Yes";
            ButtonTextTwo.text = "No";
            DialogueBox.text = "Have you ever been on a train before?";
            SpeakerName.text = "Hunter";
        }
        else if (numPadDialogue == true)
        {
            DialogueScreen.SetActive(true);
            SpeakerName.text = sd.numPadNames[currDialogue];
            DialogueBox.text = sd.numPad[currDialogue];
            ContinueButton.SetActive(true);
            BranchButtons.SetActive(false);
            InterrogateButton.SetActive(false);
        }
        else if (currTalkChar == 0)
        {
            DialogueScreen.SetActive(true);
            SpeakerName.text = "Hunter";
            //ButtonTextOne.text = sd.hResponse[0];
            //ButtonTextTwo.text = sd.hResponse[1];
            DialogueBox.text = sd.hDialogue[currDialogue];
            ContinueButton.SetActive(true);
            //BranchButtons.SetActive(true);
            if (needNumInfo == true)
            {
                InterrogateButton.SetActive(true);
            }
            else
            {
                InterrogateButton.SetActive(false);
            }
        }
        else if (currTalkChar == 1)
        {
            DialogueScreen.SetActive(true);
            SpeakerName.text = "Alexander";
            //ButtonTextOne.text = sd.sOneResponse[0];
            //ButtonTextTwo.text = sd.sOneResponse[1];
            DialogueBox.text = sd.sOneDialogue[currDialogue];
            ContinueButton.SetActive(true);
            BranchButtons.SetActive(false);
            if (interrogateEvidence >= 1)
            {
                InterrogateButton.SetActive(true);
            }
            else
            {
                InterrogateButton.SetActive(false);
            }
        }
    }

    public void StartInterrogation()
    {
        interrogateCount += interrogateEvidence - 1;
        interrogateEvidence = 0;
        interrogating = true;
        DialogueScreen.SetActive(false);
        InterrogateButton.SetActive(false);
        InterrogationScreen.SetActive(true);
        ContinueButton.SetActive(false);
        BranchButtonsI.SetActive(true);
        BranchButtonI1.SetActive(true);
        BranchButtonI2.SetActive(true);
        BranchButtonI3.SetActive(true);
        BranchButtonI4.SetActive(true);
        cc.Movement.SetActive(false);
        if (currTalkChar == 0)
        {
            needNumInfo = false;
            interrogateCount = 2;
            SpeakerNameI.text = "Hunter";
            QuestionOne.text = sd.hDialogueIQuestion[0];
            QuestionTwo.text = sd.hDialogueIQuestion[1];
            QuestionThree.text = sd.hDialogueIQuestion[2];
            DialogueBoxI.text = "";
            BranchButtonI4.SetActive(false);
        }
        else if (currTalkChar == 1)
        {
            interrogateCount = 1;
            SpeakerNameI.text = "Alexander";
            QuestionOne.text = sd.sOneQuestionsI1[0];
            QuestionTwo.text = sd.sOneQuestionsI1[1];
            //QuestionThree.text = sd.sOneQuestionsI1[2];
            //QuestionFour.text = sd.sOneQuestionsI1[3];
            DialogueBoxI.text = "";
            BranchButtonI3.SetActive(false);
            BranchButtonI4.SetActive(false);
        }
    }

    public void ProgDialogue()
    {
        if (opening == true && strLength != currDialogue)
        {
            if (branchNum == 1)
            {
                currDialogue++;
                DialogueBox.text = sd.openingB1[currDialogue];
                SpeakerName.text = sd.oB1Names[currDialogue];
            }

            else if (branchNum == 2)
            {
                currDialogue++;
                DialogueBox.text = sd.openingB2[currDialogue];
                SpeakerName.text = sd.oB2Names[currDialogue];
            }
        }

        else if (numPadDialogue == true && strLength != currDialogue)
        {
            currDialogue++;
            DialogueBox.text = sd.numPad[currDialogue];
            SpeakerName.text = sd.numPadNames[currDialogue];
        }

        else if (currTalkChar == 0 && strLength != currDialogue && interrogating == false)
        {
            if (branchNum == 1)
            {
                currDialogue++;
                //DialogueBox.text = sd.hDialogueB1[currDialogue];
            }

            else if (branchNum == 2)
            {
                currDialogue++;
                //DialogueBox.text = sd.hDialogueB2[currDialogue];
            }

            else if (branchNum == 3)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB3[currDialogue];
            }

            else if (branchNum == 4)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB4[currDialogue];
            }
        }

        else if (currTalkChar == 0 && strLength != currDialogue && interrogating == true)
        {
            if (branchNum == 1)
            {
                interrogateEvidence += 1;
                currDialogue++;
                DialogueBoxI.text = sd.hDialogueIB1[currDialogue];
                SpeakerNameI.text = sd.hIB1Names[currDialogue];
            }

            else if (branchNum == 2)
            {
                currDialogue++;
                DialogueBoxI.text = sd.hDialogueIB2[currDialogue];
                SpeakerNameI.text = sd.hIB2Names[currDialogue];
            }

            else if (branchNum == 3)
            {
                currDialogue++;
                DialogueBoxI.text = sd.hDialogueIB3[currDialogue];
                SpeakerNameI.text = sd.hIB3Names[currDialogue];
            }
        }


        else if (currTalkChar == 1 && strLength != currDialogue && interrogating == false)
        {
            if (branchNum == 1)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB1[currDialogue];
            }

            else if (branchNum == 2)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB2[currDialogue];
            }

            else if (branchNum == 3)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB3[currDialogue];
            }

            else if (branchNum == 4)
            {
                currDialogue++;
                //DialogueBox.text = sd.sOneDialogueB4[currDialogue];
            }
        }

        else if (currTalkChar == 1 && strLength != currDialogue && interrogating == true)
        {
            if (branchNum == 1)
            {
                currDialogue++;
                DialogueBoxI.text = sd.sOneDialogueI1B1[currDialogue];
                SpeakerNameI.text = sd.sOneI1B1Names[currDialogue];

            }

            else if (branchNum == 2)
            {
                currDialogue++;
                DialogueBoxI.text = sd.sOneDialogueI1B2[currDialogue];
                SpeakerNameI.text = sd.sOneI1B2Names[currDialogue];
            }

            else if (branchNum == 3)
            {
                currDialogue++;
                //DialogueBoxI.text = sd.sOneDialogueI1B3[currDialogue];
            }

            else if (branchNum == 4)
            {
                currDialogue++;
                //DialogueBoxI.text = sd.sOneDialogueI1B4[currDialogue];
            }
        }

        else
        {
            currDialogue = 0;
            strLength = 0;
            isTalking = false;
            opening = false;
            numPadDialogue = false;

            if (interrogating == false)
            {
                DialogueScreen.SetActive(false);
                InventoryButton.SetActive(true);
                NotebookButton.SetActive(true);
                cc.Movement.SetActive(true);
            }

            else if (interrogating == true && interrogateCount > 0)
            {
                interrogateCount -= 1;
                DialogueBoxI.text = "";
                BranchButtonsI.SetActive(true);
                ContinueButtonI.SetActive(false);

                if (askedOne == true)
                {
                    BranchButtonI1.SetActive(false);
                }

                if (askedTwo == true)
                {
                    BranchButtonI2.SetActive(false);
                }

                if (askedThree == true)
                {
                    BranchButtonI3.SetActive(false);
                }

                if (askedFour == true)
                {
                    BranchButtonI4.SetActive(false);
                }
            }

            else if (interrogating == true && interrogateCount <= 0)
            {
                interrogating = false;
                InterrogationScreen.SetActive(false);
                askedOne = false;
                askedTwo = false;
                askedThree = false;
                askedFour = false;
                InventoryButton.SetActive(true);
                NotebookButton.SetActive(true);
                cc.Movement.SetActive(true);
            }
        }
    }





    public void BranchOne()
    {
        branchNum = 1;
        ContinueButton.SetActive(true);

        if (currTalkChar == 0)
        {
            if (interrogating == false && opening == false)
            {
                strLength = 0;
                ContinueButton.SetActive(true);
            }

            else if (interrogating == false && opening == true)
            {
                DialogueBox.text = sd.openingB1[0];
                SpeakerName.text = sd.oB1Names[0];
                strLength = 5;
                BranchButtons.SetActive(false);
                ContinueButton.SetActive(true);
            }

            else if (interrogating == true)
            {
                DialogueBoxI.text = sd.hDialogueIB1[0];
                SpeakerNameI.text = sd.hIB1Names[0];
                strLength = 6;
                BranchButtonsI.SetActive(false);
                ContinueButtonI.SetActive(true);
                askedOne = true;
                nm.RevealComplexInformation(0);
            }
        }

        else if (currTalkChar == 1)
        {
            if (interrogating == false)
            {
                //DialogueBox.text = sd.sOneDialogueB1[0];
                strLength = 2;
                BranchButtons.SetActive(false);
                ContinueButton.SetActive(true);
            }

            else if (interrogating == true)
            {
                DialogueBoxI.text = sd.sOneDialogueI1B1[0];
                SpeakerNameI.text = sd.sOneI1B1Names[0];
                strLength = 3;
                currDialogue = 0;
                BranchButtonsI.SetActive(false);
                ContinueButton.SetActive(true);
                askedOne = true;
            }
        }
    }


    public void BranchTwo()
    {
        branchNum = 2;
        ContinueButton.SetActive(true);

        if (currTalkChar == 0)
        {
            if (interrogating == false && opening == false)
            {

            }

            else if (interrogating == false && opening == true)
            {
                DialogueBox.text = sd.openingB2[0];
                SpeakerName.text = sd.oB2Names[0];
                strLength = 6;
                BranchButtons.SetActive(false);
                ContinueButton.SetActive(true);
            }

            if (interrogating == true)
            {
                DialogueBoxI.text = sd.hDialogueIB2[0];
                SpeakerNameI.text = sd.hIB2Names[0];
                strLength = 5;
                BranchButtonsI.SetActive(false);
                ContinueButtonI.SetActive(true);
                askedTwo = true;

            }
        }

        else if (currTalkChar == 1)
        {
            if (interrogating == false)
            {
                //DialogueBox.text = sd.sOneDialogueB2[0];
                strLength = 2;
                BranchButtons.SetActive(false);
                ContinueButton.SetActive(true);
            }

            else if (interrogating == true)
            {
                DialogueBoxI.text = sd.sOneDialogueI1B2[0];
                SpeakerNameI.text = sd.sOneI1B2Names[0];
                strLength = 4;
                BranchButtonsI.SetActive(false);
                ContinueButtonI.SetActive(true);
                askedTwo = true;
            }
        }
    }


    public void BranchThree()
    {
        branchNum = 3;
        ContinueButton.SetActive(true);
        
        if (currTalkChar == 0)
        {
            if (interrogating == false)
            {

            }

            if (interrogating == true)
            {
                DialogueBoxI.text = sd.hDialogueIB3[0];
                SpeakerNameI.text = sd.hIB3Names[0];
                strLength = 1;
                BranchButtonsI.SetActive(false);
                ContinueButtonI.SetActive(true);
                askedThree = true;
            }
        }
        else if (currTalkChar == 1)
        {
            if (interrogating == false)
            {

            }

            if (interrogating == true)
            {
                //DialogueBoxI.text = sd.sOneDialogueI1B3[0];
                strLength = 1;
                BranchButtonsI.SetActive(false);
                ContinueButtonI.SetActive(true);
                askedThree = true;
            }
        }
    }


    public void BranchFour()
    {
        branchNum = 4;
        ContinueButton.SetActive(true);

        if (interrogating == false)
        {

        }

        if (interrogating == true)
        {
            //DialogueBoxI.text = sd.sOneDialogueI1B4[0];
            strLength = 1;
            BranchButtonsI.SetActive(false);
            ContinueButtonI.SetActive(true);
            askedFour = true;
        }
    }
}
