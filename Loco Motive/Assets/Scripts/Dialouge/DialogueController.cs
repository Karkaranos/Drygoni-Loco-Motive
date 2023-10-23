using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    #region Variables
    public bool opening = true;
    public bool numPadDialogue = false;
    public bool needNumInfo = false;
    public bool isTalking = true;
    public int currTalkChar;
    public int strLength;
    public int currSpeaker;
    public bool interrogating = false;
    public bool accusing = false;
    public Image PortraitImage;

    public TMP_Text DialogueBox;
    public TMP_Text DialogueBoxInterrogation;
    public TMP_Text SpeakerName;
    public TMP_Text SpeakerNameInterrogation;
    public TMP_Text ButtonTextOne;
    public TMP_Text ButtonTextTwo;
    public TMP_Text ContinueText;
    public TMP_Text InterrogateQuestionOne;
    public TMP_Text InterrogateQuestionTwo;
    public TMP_Text InterrogateQuestionThree;
    public TMP_Text InterrogateQuestionFour;

    public GameObject DialogueScreen;
    public GameObject InterrogationScreen;
    public GameObject BranchButtons;
    public GameObject BranchButttonsInterrogation;
    public GameObject BranchButtonInterrogation1;
    public GameObject BranchButtonInterrogation2;
    public GameObject BranchButtonInterrogation3;
    public GameObject BranchButtonInterrogation4;
    public GameObject ContinueButton;
    public GameObject ContinueButtonInterrogation;
    public GameObject InterrogateButton;
    public GameObject InventoryButton;
    public GameObject NotebookButton;
    public GameObject AccusationButton;

    public ClickController cc;
    public StoredDialogue sd;
    public NotebookManager nm;
    public DialogueInstance openingDialogue;
    public DialogueInstance currentDialogue;
    public InterrogationInstance currentInterrogation;
    public AccusationInstance currentAccusation;

    public List<GameObject> ChoiceBranch;

    private AudioManager am;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        currentDialogue = openingDialogue;
        DialogueScreen.SetActive(true);
        NotebookButton.SetActive(false);
        InventoryButton.SetActive(false);
        StartDialogue();
        isTalking = true;
        //UpdateScreen(currentDialogue.AllMessages[currentDialogue.currMessage]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScreen(DialogueMessage x)
    {
        DialogueScreen.SetActive(true);
        DialogueBox.text = x.Text;
        SpeakerName.text = getName(x.Names);
        PortraitImage.sprite = x.Portrait;
        InterrogateButton.SetActive(currentDialogue.canInterrogate);
        
        if (x.Branch.Count > 0)
        {
            for (int i = 0; i < x.Branch.Count; i++)
            {
                ChoiceBranch[i].GetComponent<DialogueButton>().UpdateButton(x.Branch[i].nextDialogue, x.Branch[i].choiceText);
                ChoiceBranch[i].SetActive(true);
            }
            ContinueButton.SetActive(false);
        }
        else
        {
            for (int i = 0; i < ChoiceBranch.Count; i++)
            {
                ChoiceBranch[i].SetActive(false);
            }
            ContinueButton.SetActive(true);
        }
    }

    /// <summary>
    /// Starts playing dialogue when a character is clicked
    /// </summary>
    public void StartDialogue()
    {
        //Debug.Log("This works");
        isTalking = true;
        InventoryButton.SetActive(false);
        NotebookButton.SetActive(false);
        cc.Movement.SetActive(false);
        cc.Map.SetActive(false);
        if (opening == true)
        {
            DialogueScreen.SetActive(true);
            ContinueButton.SetActive(false);
            BranchButtons.SetActive(true);
            InterrogateButton.SetActive(false);
            currentDialogue.StartText();
            InventoryButton.SetActive(false);
            NotebookButton.SetActive(false);
            //ButtonTextOne.text = "Yes";
            //ButtonTextTwo.text = "No";
            //DialogueBox.text = "Have you ever been on a train before?";
            //SpeakerName.text = "Hunter";
        }
        //else if (numPadDialogue == true)
        //{
        //    DialogueScreen.SetActive(true);
        //    SpeakerName.text = sd.numPadNames[currDialogue];
        //    DialogueBox.text = sd.numPad[currDialogue];
        //    ContinueButton.SetActive(true);
        //    BranchButtons.SetActive(false);
        //    InterrogateButton.SetActive(false);
        //}
        //else if (currTalkChar == 0)
        //{
        //    DialogueScreen.SetActive(true);
        //    SpeakerName.text = "Hunter";
        //    //ButtonTextOne.text = sd.hResponse[0];
        //    //ButtonTextTwo.text = sd.hResponse[1];
        //    DialogueBox.text = sd.hDialogue[currDialogue];
        //    ContinueButton.SetActive(true);
        //    //BranchButtons.SetActive(true);
        //    if (needNumInfo == true)
        //    {
        //        InterrogateButton.SetActive(true);
        //    }
        //    else
        //    {
        //        InterrogateButton.SetActive(false);
        //    }
        //}
        //else if (currTalkChar == 1)
        //{
        //    DialogueScreen.SetActive(true);
        //    SpeakerName.text = "Alexander";
        //    //ButtonTextOne.text = sd.sOneResponse[0];
        //    //ButtonTextTwo.text = sd.sOneResponse[1];
        //    DialogueBox.text = sd.sOneDialogue[currDialogue];
        //    ContinueButton.SetActive(true);
        //    BranchButtons.SetActive(false);
        //    if (interrogateEvidence >= 1)
        //    {
        //        InterrogateButton.SetActive(true);
        //    }
        //    else
        //    {
        //        InterrogateButton.SetActive(false);
        //    }
        //}
    }

    public void StartInterrogation()
    {
        currentDialogue.canInterrogate = false;
        currentInterrogation = currentDialogue.thisInterrogation;
        interrogating = true;
        InterrogateButton.SetActive(false);
        cc.Movement.SetActive(false);
        cc.Map.SetActive(false);
        UpdateScreen(currentInterrogation.AllMessages[0]);
        am.PlayInterrogationMusic();
    }
    
    public void StartAccusation()
    {
        currentAccusation = currentDialogue.thisAccusation;
        accusing = true;
        AccusationButton.SetActive(false);
        cc.Movement.SetActive(false);
        cc.Map.SetActive(false);
        UpdateScreen(currentAccusation.AllMessages[0]);
    }
        

    public string getName(Constants.Names x)
    {
        switch (x)
        {
            case Constants.Names.Hunter:
                return "Hunter";

            case Constants.Names.Casey:
                return "Casey";

            case Constants.Names.Alex:
                return "Alex";

            case Constants.Names.Hana:
                return "Hana";
            
            case Constants.Names.Brady:
                return "Brady";

            case Constants.Names.Dominic:
                return "Dominic";

            case Constants.Names.Josephine:
                return "Josephine";

            default:
                return "???";
        }
    }
    public void StopDialogue()
    {
        DialogueScreen.SetActive(false);
        InventoryButton.SetActive(true);
        NotebookButton.SetActive(true);
        cc.Movement.SetActive(true);
        cc.Map.SetActive(true);
        isTalking = false;
        if (interrogating)
        {
            am.PlayGameMusic();
        }
        interrogating = false;
        accusing = false;
    }

    //public void ProgDialogue()
    //{
    //    if (opening == true && strLength != currDialogue)
    //    {
    //        if (branchNum == 1)
    //        {
    //            currDialogue++;
    //            DialogueBox.text = sd.openingB1[currDialogue];
    //            SpeakerName.text = sd.oB1Names[currDialogue];
    //        }

    //        else if (branchNum == 2)
    //        {
    //            currDialogue++;
    //            DialogueBox.text = sd.openingB2[currDialogue];
    //            SpeakerName.text = sd.oB2Names[currDialogue];
    //        }
    //    }

    //    else if (numPadDialogue == true && strLength != currDialogue)
    //    {
    //        currDialogue++;
    //        DialogueBox.text = sd.numPad[currDialogue];
    //        SpeakerName.text = sd.numPadNames[currDialogue];
    //    }

    //    else if (currTalkChar == 0 && strLength != currDialogue && interrogating == false)
    //    {
    //        if (branchNum == 1)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.hDialogueB1[currDialogue];
    //        }

    //        else if (branchNum == 2)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.hDialogueB2[currDialogue];
    //        }

    //        else if (branchNum == 3)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB3[currDialogue];
    //        }

    //        else if (branchNum == 4)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB4[currDialogue];
    //        }
    //    }

    //    else if (currTalkChar == 0 && strLength != currDialogue && interrogating == true)
    //    {
    //        if (branchNum == 1)
    //        {
    //            interrogateEvidence += 1;
    //            currDialogue++;
    //            DialogueBoxInterrogation.text = sd.hDialogueIB1[currDialogue];
    //            SpeakerNameInterrogation.text = sd.hIB1Names[currDialogue];
    //        }

    //        else if (branchNum == 2)
    //        {
    //            currDialogue++;
    //            DialogueBoxInterrogation.text = sd.hDialogueIB2[currDialogue];
    //            SpeakerNameInterrogation.text = sd.hIB2Names[currDialogue];
    //        }

    //        else if (branchNum == 3)
    //        {
    //            currDialogue++;
    //            DialogueBoxInterrogation.text = sd.hDialogueIB3[currDialogue];
    //            SpeakerNameInterrogation.text = sd.hIB3Names[currDialogue];
    //        }
    //    }


    //    else if (currTalkChar == 1 && strLength != currDialogue && interrogating == false)
    //    {
    //        if (branchNum == 1)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB1[currDialogue];
    //        }

    //        else if (branchNum == 2)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB2[currDialogue];
    //        }

    //        else if (branchNum == 3)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB3[currDialogue];
    //        }

    //        else if (branchNum == 4)
    //        {
    //            currDialogue++;
    //            //DialogueBox.text = sd.sOneDialogueB4[currDialogue];
    //        }
    //    }

    //    else if (currTalkChar == 1 && strLength != currDialogue && interrogating == true)
    //    {
    //        if (branchNum == 1)
    //        {
    //            currDialogue++;
    //            DialogueBoxInterrogation.text = sd.sOneDialogueI1B1[currDialogue];
    //            SpeakerNameInterrogation.text = sd.sOneI1B1Names[currDialogue];
    //            nm.RevealComplexInformation(0);

    //        }

    //        else if (branchNum == 2)
    //        {
    //            currDialogue++;
    //            DialogueBoxInterrogation.text = sd.sOneDialogueI1B2[currDialogue];
    //            SpeakerNameInterrogation.text = sd.sOneI1B2Names[currDialogue];
    //        }

    //        else if (branchNum == 3)
    //        {
    //            currDialogue++;
    //            //DialogueBoxI.text = sd.sOneDialogueI1B3[currDialogue];
    //        }

    //        else if (branchNum == 4)
    //        {
    //            currDialogue++;
    //            //DialogueBoxI.text = sd.sOneDialogueI1B4[currDialogue];
    //        }
    //    }

    //    else
    //    {
    //        currDialogue = 0;
    //        strLength = 0;
    //        isTalking = false;
    //        opening = false;
    //        numPadDialogue = false;

    //        if (interrogating == false)
    //        {
    //            DialogueScreen.SetActive(false);
    //            InventoryButton.SetActive(true);
    //            NotebookButton.SetActive(true);
    //            cc.Movement.SetActive(true);
    //            cc.Map.SetActive(true);
    //        }

    //        else if (interrogating == true && interrogateCount > 0)
    //        {
    //            interrogateCount -= 1;
    //            DialogueBoxInterrogation.text = "";
    //            BranchButttonsInterrogation.SetActive(true);
    //            ContinueButtonInterrogation.SetActive(false);

    //            if (askedOne == true)
    //            {
    //                BranchButtonInterrogation1.SetActive(false);
    //            }

    //            if (askedTwo == true)
    //            {
    //                BranchButtonInterrogation2.SetActive(false);
    //            }

    //            if (askedThree == true)
    //            {
    //                BranchButtonInterrogation3.SetActive(false);
    //            }

    //            if (askedFour == true)
    //            {
    //                BranchButtonInterrogation4.SetActive(false);
    //            }
    //        }

    //        else if (interrogating == true && interrogateCount <= 0)
    //        {
    //            interrogating = false;
    //            InterrogationScreen.SetActive(false);
    //            askedOne = false;
    //            askedTwo = false;
    //            askedThree = false;
    //            askedFour = false;
    //            InventoryButton.SetActive(true);
    //            NotebookButton.SetActive(true);
    //            cc.Movement.SetActive(true);
    //            cc.Map.SetActive(true);
    //        }
    //    }
    //}





    //public void BranchOne()
    //{
    //    branchNum = 1;
    //    ContinueButton.SetActive(true);

    //    if (currTalkChar == 0)
    //    {
    //        if (interrogating == false && opening == false)
    //        {
    //            strLength = 0;
    //            ContinueButton.SetActive(true);
    //        }

    //        else if (interrogating == false && opening == true)
    //        {
    //            DialogueBox.text = sd.openingB1[0];
    //            SpeakerName.text = sd.oB1Names[0];
    //            strLength = 5;
    //            BranchButtons.SetActive(false);
    //            ContinueButton.SetActive(true);
    //        }

    //        else if (interrogating == true)
    //        {
    //            DialogueBoxInterrogation.text = sd.hDialogueIB1[0];
    //            SpeakerNameInterrogation.text = sd.hIB1Names[0];
    //            strLength = 6;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedOne = true;
    //            //nm.RevealComplexInformation(0);
    //        }
    //    }

    //    else if (currTalkChar == 1)
    //    {
    //        if (interrogating == false)
    //        {
    //            //DialogueBox.text = sd.sOneDialogueB1[0];
    //            strLength = 2;
    //            BranchButtons.SetActive(false);
    //            ContinueButton.SetActive(true);
    //        }

    //        else if (interrogating == true)
    //        {
    //            DialogueBoxInterrogation.text = sd.sOneDialogueI1B1[0];
    //            SpeakerNameInterrogation.text = sd.sOneI1B1Names[0];
    //            strLength = 3;
    //            currDialogue = 0;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedOne = true;
    //        }
    //    }
    //}


    //public void BranchTwo()
    //{
    //    branchNum = 2;
    //    ContinueButton.SetActive(true);

    //    if (currTalkChar == 0)
    //    {
    //        if (interrogating == false && opening == false)
    //        {

    //        }

    //        else if (interrogating == false && opening == true)
    //        {
    //            DialogueBox.text = sd.openingB2[0];
    //            SpeakerName.text = sd.oB2Names[0];
    //            strLength = 6;
    //            BranchButtons.SetActive(false);
    //            ContinueButton.SetActive(true);
    //        }

    //        if (interrogating == true)
    //        {
    //            DialogueBoxInterrogation.text = sd.hDialogueIB2[0];
    //            SpeakerNameInterrogation.text = sd.hIB2Names[0];
    //            strLength = 5;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedTwo = true;

    //        }
    //    }

    //    else if (currTalkChar == 1)
    //    {
    //        if (interrogating == false)
    //        {
    //            //DialogueBox.text = sd.sOneDialogueB2[0];
    //            strLength = 2;
    //            BranchButtons.SetActive(false);
    //            ContinueButton.SetActive(true);
    //        }

    //        else if (interrogating == true)
    //        {
    //            DialogueBoxInterrogation.text = sd.sOneDialogueI1B2[0];
    //            SpeakerNameInterrogation.text = sd.sOneI1B2Names[0];
    //            strLength = 4;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedTwo = true;
    //        }
    //    }
    //}


    //public void BranchThree()
    //{
    //    branchNum = 3;
    //    ContinueButton.SetActive(true);

    //    if (currTalkChar == 0)
    //    {
    //        if (interrogating == false)
    //        {

    //        }

    //        if (interrogating == true)
    //        {
    //            DialogueBoxInterrogation.text = sd.hDialogueIB3[0];
    //            SpeakerNameInterrogation.text = sd.hIB3Names[0];
    //            strLength = 1;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedThree = true;
    //        }
    //    }
    //    else if (currTalkChar == 1)
    //    {
    //        if (interrogating == false)
    //        {

    //        }

    //        if (interrogating == true)
    //        {
    //            //DialogueBoxI.text = sd.sOneDialogueI1B3[0];
    //            strLength = 1;
    //            BranchButttonsInterrogation.SetActive(false);
    //            ContinueButtonInterrogation.SetActive(true);
    //            askedThree = true;
    //        }
    //    }
    //}


    //public void BranchFour()
    //{
    //    branchNum = 4;
    //    ContinueButton.SetActive(true);

    //    if (interrogating == false)
    //    {

    //    }

    //    if (interrogating == true)
    //    {
    //        //DialogueBoxI.text = sd.sOneDialogueI1B4[0];
    //        strLength = 1;
    //        BranchButttonsInterrogation.SetActive(false);
    //        ContinueButtonInterrogation.SetActive(true);
    //        askedFour = true;
    //    }
    //}


}
