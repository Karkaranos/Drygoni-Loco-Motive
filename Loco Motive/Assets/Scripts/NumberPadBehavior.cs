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
    [SerializeField] private int code;
    [SerializeField] private bool startsWith0;
    [SerializeField] private int digitsInCode;
    private int playerGuess = 0;
    private bool playerGuessStartWith0 = false;
    private int digitsEntered=0;
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private GameObject numberPadObject;
    [SerializeField] private GameObject numPadEvidence; 


    public bool unlocked = false;
    #endregion

    #region Functions

    private void Start()
    {
        numPadEvidence.SetActive(false);
    }

    /// <summary>
    /// Takes in a number and adds it to the player's current guess. 
    /// </summary>
    /// <param name="number"></param>
    public void NumberPressed(int number)
    {
        //If there are fewer digits entered than digits in the code, add to the code
        if (digitsEntered < digitsInCode)
        {
            //If the current guess is 0, run these checks
            if (playerGuess == 0)
            {
                //If the current guess is 0, player inputted 0,  and the guess
                //doesn't start with 0, signal the guess starts with a 0
                if (!playerGuessStartWith0 && number == 0)
                {
                    playerGuessStartWith0 = true;
                }
                //Otherwise if the player didn't input 0 or the number already has
                //a zero, run these checks
                else
                {
                    //If there are many digits and input is 0, multiply it by 10
                    if(digitsEntered > 0 && number == 0)
                    {
                        playerGuess *= 10;
                    }
                    //otherwise just add the inputted number
                    else
                    {
                        playerGuess += number;
                    }
                }
            }
            //If the current guess isn't 0, multiply it by 10 and add the input
            else
            {
                playerGuess *= 10;
                playerGuess += number;
            }
            //Increase the number of digits guessed
            digitsEntered++;
            if (playerGuessStartWith0 && playerGuess!=0)
            {
                codeText.text = "0" + playerGuess;
            }
            else
            {
                codeText.text = playerGuess.ToString();
            }
        }
        //Otherwise if there are the same number of digits in both, check the code
        else
        {
            CheckCode();
        }
    }

    /// <summary>
    /// Checks if the player inputted code is correct
    /// </summary>
    public void CheckCode()
    {
        //If both codes start or don't start with 0, check the number
        if (startsWith0 == playerGuessStartWith0)
        {
            //If the codes are equal, break the lock
            if(code == playerGuess)
            {
                unlocked = true;
                print("Lock Broken!");
                numberPad.SetActive(false);
                numberPadObject.SetActive(false);
                numPadEvidence.SetActive(true);
            }
            //Otherwise reset the lock
            else
            {
                playerGuess = 0;
                digitsEntered = 0;
                print("fail");
                playerGuessStartWith0 = false;
                codeText.text = "WRONG";
            }
        }
        else
        {
            playerGuess = 0;
            digitsEntered = 0;
            print("fail");
            playerGuessStartWith0 = false;
            codeText.text = "WRONG";
        }
    }

    /// <summary>
    /// Closes the lock window
    /// </summary>
    public void CloseLock()
    {
        numberPad.SetActive(false);
        playerGuess = 0;
        digitsEntered = 0;
        numberPadObject.SetActive(true);
        playerGuessStartWith0 = false;
    }

    /// <summary>
    /// Opens the lock window
    /// </summary>
    public void OpenLock()
    {
        numberPad.SetActive(true);
        numberPadObject.SetActive(false);
        codeText.text = playerGuess.ToString();
    }
    #endregion
}