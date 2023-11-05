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
    public float timeBtwnChars;
    public float timeBtwnWords;

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
    public GameObject ExitButton;
    public GameObject BranchOne;
    public GameObject BranchTwo;
    public GameObject BranchThree;
    public GameObject BranchFour;

    public ClickController cc;
    public NotebookManager nm;
    public DialogueInstance openingDialogue;
    public DialogueInstance currentDialogue;
    public InterrogationInstance currentInterrogation;
    public AccusationInstance currentAccusation;
    private InventoryBehavior ib;

    public List<GameObject> ChoiceBranch;

    private AudioManager am;

    //Class used in making typewriter text work
    [SerializeField] TextMeshProUGUI _textMeshPro;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        ib = FindObjectOfType<InventoryBehavior>();
        currentDialogue = openingDialogue;
        DialogueScreen.SetActive(true);
        NotebookButton.SetActive(false);
        InventoryButton.SetActive(false);
        StartDialogue();
        isTalking = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScreen(DialogueMessage x)
    {
        DialogueScreen.SetActive(true);
        SpeakerName.text = getName(x.Names);
        PortraitImage.sprite = x.Portrait;
        _textMeshPro.text = x.Text;
        StartCoroutine(Speaking());
        InterrogateButton.SetActive(currentDialogue.canInterrogate);
        AccusationButton.SetActive(currentDialogue.canAccuse);
        
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
        isTalking = true;
        InventoryButton.SetActive(false);
        NotebookButton.SetActive(false);
        cc.Movement.SetActive(false);
        cc.Map.SetActive(false);
        ExitButton.SetActive(true);
        if (opening == true)
        {
            DialogueScreen.SetActive(true);
            ContinueButton.SetActive(false);
            BranchButtons.SetActive(true);
            InterrogateButton.SetActive(false);
            currentDialogue.StartText();
            InventoryButton.SetActive(false);
            NotebookButton.SetActive(false);
            ExitButton.SetActive(false);
        }
        else
        {
            DialogueScreen.SetActive(true);
            ContinueButton.SetActive(false);
            BranchButtons.SetActive(true);
            InterrogateButton.SetActive(false);
            currentDialogue.StartText();
            InventoryButton.SetActive(false);
            NotebookButton.SetActive(false);
            ExitButton.SetActive(true);
        }
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
        if (am != null)
        {
            am.PlayInterrogationMusic();
        }
        ExitButton.SetActive(false);

    }
    
    public void StartAccusation()
    {
        currentDialogue.canAccuse = false;
        currentAccusation = currentDialogue.thisAccusation;
        accusing = true;
        AccusationButton.SetActive(false);
        cc.Movement.SetActive(false);
        cc.Map.SetActive(false);
        UpdateScreen(currentAccusation.AllMessages[0]);
        ExitButton.SetActive(false);
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

    public void BranchOneClicked()
    {
        BranchOne.GetComponent<Image>().color = Color.cyan;
    }
    public void BranchTwoClicked()
    {
        BranchTwo.GetComponent<Image>().color = Color.cyan;
    }
    public void BranchThreeClicked()
    {
        BranchThree.GetComponent<Image>().color = Color.cyan;
    }
    public void BranchFourClicked()
    {
        BranchFour.GetComponent<Image>().color = Color.cyan;
    }
    public void StopDialogue()
    {
        DialogueScreen.SetActive(false);
        if (ib.iconIsEnabled)
        {
            InventoryButton.SetActive(true);
        }
        cc.Map.SetActive(true);
        cc.Movement.SetActive(true);
        if (nm.iconIsEnabled)
        {
            NotebookButton.SetActive(true);
        }
        cc.Movement.SetActive(true);
        cc.Map.SetActive(true);
        isTalking = false;
        if (interrogating && am != false)
        {
            am.PlayGameMusic();
        }
        interrogating = false;
        accusing = false;
        GameObject lvc = GameObject.Find("LargeViewCanvas");
        if (lvc != null)
        {
            lvc.SetActive(false);
        }
        BranchOne.GetComponent<Image>().color = Color.white;
        BranchTwo.GetComponent<Image>().color = Color.white;
        BranchThree.GetComponent<Image>().color = Color.white;
        BranchFour.GetComponent<Image>().color = Color.white;
        opening = false;
    }

    public IEnumerator Speaking()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    public void EndCheck()
    {

    }
}
