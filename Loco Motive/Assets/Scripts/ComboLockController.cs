using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboLockController : MonoBehaviour
{
    public int digitOne = 0;
    public int digitTwo = 0;
    public int digitThree = 0;
    public int correctDigitOne;
    public int correctDigitTwo;
    public int correctDigitThree;
    public bool solved = false;
    public GameObject OpenSafe;
    public GameObject ClosedSafe;
    public TMP_Text ButtonOne;
    public TMP_Text ButtonTwo;
    public TMP_Text ButtonThree;
    public DialogueController dc;
    public GameObject ComboLock;

    // Start is called before the first frame update
    void Start()
    {
        ComboLock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (solved == false)
        {
            if (digitOne == correctDigitOne && digitTwo == correctDigitTwo && digitThree == correctDigitThree)
            {
                Unlock();
            }
        }
    }

    public void UpdateDigitOne()
    {
        if (digitOne == 9)
        {
            digitOne = 0;
        }
        else
        {
            digitOne += 1;
        }
        ButtonOne.text = "" + digitOne;
    }

    public void UpdateDigitTwo()
    {
        if (digitTwo == 9)
        {
            digitTwo = 0;
        }
        else
        {
            digitTwo += 1;
        }
        ButtonTwo.text = "" + digitTwo;
    }

    public void UpdateDigitThree()
    {
        if (digitThree == 9)
        {
            digitThree = 0;
        }
        else
        {
            digitThree += 1;
        }
        ButtonThree.text = "" + digitThree;
    }

    //This runs when the combination is correct
    void Unlock()
    {
        solved = true;
        OpenSafe.SetActive(true);
        ClosedSafe.SetActive(false);
        CloseLock();
    }

    //This opens the combination lock
    public void OpenLock()
    {
        dc.isTalking = true;
        dc.InventoryButton.SetActive(false);
        dc.NotebookButton.SetActive(false);
        dc.cc.Movement.SetActive(false);
        dc.cc.Map.SetActive(false);
        ComboLock.SetActive(true);
    }

    //This closes the combination lock
    public void CloseLock()
    {
        dc.isTalking = false;
        dc.InventoryButton.SetActive(true);
        dc.NotebookButton.SetActive(true);
        dc.cc.Movement.SetActive(true);
        dc.cc.Map.SetActive(true);
        ComboLock.SetActive(false);
    }
}
