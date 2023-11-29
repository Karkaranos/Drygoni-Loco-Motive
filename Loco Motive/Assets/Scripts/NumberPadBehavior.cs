/*****************************************************************************
// File Name :         NumberPadBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 22, 2023
//
// Brief Description :  Handles Number Pad- takes input, checks the code, then, if
                           true, removes the lock
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberPadBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject numberPad;
    [SerializeField] private string code;
    private string playerGuess = "----";
    private int digitsEntered=0;
    [SerializeField] private TMP_Text codeText;    
    [SerializeField] private Image comboImage;
    [SerializeField] private GameObject numberPadObject;
    [SerializeField] private GameObject numPadEvidence;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject movementArrows;
    [SerializeField] private GameObject inventoryIcon;
    [SerializeField] private GameObject notebookIcon;
    [SerializeField] private GameObject OpenLockbox;
    [SerializeField] private GameObject ClosedLockbox;

    [Header("Put Sprites Here")]
    [SerializeField]
    private Sprite standardNumberPad;
    [SerializeField]
    private Sprite wrongCombo;
    [SerializeField]
    private Sprite rightCombo;

    private DialogueController dc;


    public bool unlocked = false;
    #endregion

    #region Functions

    private void Start()
    {
        numberPad.SetActive(false);
        numPadEvidence.SetActive(false);
        dc = FindObjectOfType<DialogueController>();
    }

    /// <summary>
    /// Takes in a number and adds it to the player's current guess. 
    /// </summary>
    /// <param name="number"></param>
    public void NumberPressed(int number)
    {
        //Set the code to be blank if starting a new guess
        if(digitsEntered == 0)
        {
            playerGuess = "";
        }

        //If there are fewer digits entered than digits in the code, add to the code
        if (digitsEntered < 4)
        {
            playerGuess += number;

            //Increase the number of digits guessed
            digitsEntered++;
            codeText.text = playerGuess;
        }
        //Otherwise if the lock is about to overflow, reset it
        else
        {
            playerGuess = "----";
            digitsEntered = 0;
            codeText.text = "OVERFLOW";
            StartCoroutine("WrongCode");
        }
    }

    /// <summary>
    /// Checks if the player inputted code is correct
    /// </summary>
    public void CheckCode()
    {
        //Check the code
        if (code.Equals(playerGuess))
        {
            unlocked = true;
            StartCoroutine(RightCode());
        }
        else
        {
            playerGuess = "----";
            digitsEntered = 0;
            codeText.text = "WRONG";
            StartCoroutine("WrongCode");
        }
    }

    /// <summary>
    /// Closes the lock window
    /// </summary>
    public void CloseLock()
    {
        numberPad.SetActive(false);
        playerGuess = "----";
        digitsEntered = 0;
        numberPadObject.SetActive(true);
        map.SetActive(true);
        movementArrows.SetActive(true);
        notebookIcon.SetActive(true);
        inventoryIcon.SetActive(true);
        dc.isTalking = false;
    }

    /// <summary>
    /// Opens the lock window
    /// </summary>
    public void OpenLock()
    {
        numberPad.SetActive(true);
        //numberPadObject.SetActive(false);
        codeText.text = playerGuess.ToString();
        map.SetActive(false);
        movementArrows.SetActive(false);
        notebookIcon.SetActive(false);
        inventoryIcon.SetActive(false);
        dc.isTalking = true;
    }

    IEnumerator RightCode()
    {
        comboImage.sprite = rightCombo;
        yield return new WaitForSeconds(1f);
        comboImage.sprite = standardNumberPad;
        CloseLock();
        OpenLockbox.SetActive(true);
        ClosedLockbox.SetActive(false);
        numPadEvidence.SetActive(true);
        Destroy(numberPad);
        Destroy(numberPadObject);
    }

    IEnumerator WrongCode()
    {
        comboImage.sprite = wrongCombo;
        yield return new WaitForSeconds(1f);
        comboImage.sprite = standardNumberPad;
        codeText.text = "----";
    }
    #endregion
}
