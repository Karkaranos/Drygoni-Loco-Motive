using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboLockController : MonoBehaviour
{
    public int digitOne = 0;
    public int digitTwo = 0;
    public int digitThree = 0;
    public int correctDigitOne;
    public int correctDigitTwo;
    public int correctDigitThree;
    public GameObject OpenSafe;
    // Start is called before the first frame update
    void Start()
    {
        //OpenSafe
    }

    // Update is called once per frame
    void Update()
    {
        if (digitOne == correctDigitOne && digitTwo == correctDigitTwo && digitThree == correctDigitThree)
        {
            OpenLock();
        }
    }

    void OpenLock()
    {

    }
}
