using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Controller : MonoBehaviour
{
    private PlayerInput mouseController;

    private InputAction mPos;
    private InputAction interact;

    private Vector2 currPos;

    public ComboLockController clc;
    public StoredDialogue sd;

    public bool isTalking = false;
    public int currDialogue = 0;
    public int currTalkChar;
    public int strLength;
    public int branchNum;
    public int currSpeaker;
    public bool interrogating = false;


    //CHANGE THIS TO 0 AFTER MERGE, IS CURRENTLY 4 FOR TESTING
    public int interrogateEvidence = 0;
    public int interrogateCount = 0;
    public bool askedOne = false;
    public bool askedTwo = false;
    public bool askedThree = false;
    public bool askedFour = false;
    public bool opening = true;
    public bool numPadDialogue = false;
    public bool needNumInfo = false;


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
    public GameObject NumPadCollider;
    public GameObject InventoryButton;
    public GameObject NotebookButton;
    public GameObject Movement;
    private NotebookManager nm;


    // Start is called before the first frame update
    void Start()
    {
        mouseController = GetComponent<PlayerInput>();
        mouseController.currentActionMap.Enable();

        nm = GameObject.Find("NotebookManager").GetComponent<NotebookManager>();

        mPos = mouseController.currentActionMap.FindAction("MousePosition");
        interact = mouseController.currentActionMap.FindAction("Interact");

        interact.performed += Interact_performed;

        ContinueText.text = "Continue";

        DialogueScreen.SetActive(false);
        InterrogationScreen.SetActive(false);

        opening = true;
        StartDialogue();
    }



    void Update()
    {
        currPos = mPos.ReadValue<Vector2>();
    }


    /// <summary>
    /// When the player clicks, uses raycasting to hit the gameObject they clicked,
    /// then checks what they hit if they hit something
    /// </summary>
    /// <param name="obj"></param>
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(currPos), Vector2.zero);

        if (hit.collider != null && isTalking == false)
        {

            if (hit.transform.GetComponent<ObjectHandler>())
            {
                hit.transform.GetComponent<ObjectHandler>().Interact();
            }

            if (hit.transform.GetComponent<NumberPadButtonBehavior>())
            {
                hit.transform.GetComponent<NumberPadButtonBehavior>().OpenPad();
            }
            if (hit.transform.GetComponent<RoomMove>())
            {
                transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }

            else if (hit.collider.CompareTag("Hunter"))
            {
                currTalkChar = 0;
                strLength = 0;
                StartDialogue();
            }
            else if (hit.collider.CompareTag("SuspectOne"))
            {
                currTalkChar = 1;
                strLength = 0;
                StartDialogue();
            }

            else if (hit.collider.CompareTag("DigitOne"))
            {
                if (clc.digitOne == 9)
                {
                    clc.digitOne = 0;
                }
                else
                {
                    clc.digitOne += 1;
                }
            }

            else if (hit.collider.CompareTag("DigitTwo"))
            {
                if (clc.digitTwo == 9)
                {
                    clc.digitTwo = 0;
                }
                else
                {
                    clc.digitTwo += 1;
                }
            }

            else if (hit.collider.CompareTag("DigitThree"))
            {
                if (clc.digitThree == 9)
                {
                    clc.digitThree = 0;
                }
                else
                {
                    clc.digitThree += 1;
                }
            }

            else if (hit.collider.CompareTag("NumPadCollider"))
            {
                numPadDialogue = true;
                needNumInfo = true;
                StartDialogue();
                NumPadCollider.SetActive(false);
                strLength = 1;
            }
        }
    }



    /// <summary>
    /// Starts playing dialogue when a character is clicked
    /// </summary>
    public void StartDialogue()
    {
        isTalking = true;
        InventoryButton.SetActive(false);
        NotebookButton.SetActive(false);
        Movement.SetActive(false);
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
        Movement.SetActive(false);
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
                Movement.SetActive(true);
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
                Movement.SetActive(true);
            }
        }
    }





    public void BranchOne()
    {
        branchNum = 1;

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