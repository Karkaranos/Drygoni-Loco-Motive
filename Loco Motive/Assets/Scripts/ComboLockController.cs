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
    public GameObject OpenSafe;
    public TMP_Text SafeText;
    public GameObject DigitOne;
    public GameObject DigitTwo;
    public GameObject DigitThree;
    [SerializeField] GameObject endText;
    // Start is called before the first frame update
    void Start()
    {
        OpenSafe.SetActive(false);
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (digitOne == correctDigitOne && digitTwo == correctDigitTwo && digitThree == correctDigitThree)
        {
            OpenLock();
        }
        SafeText.text = digitOne + "  " + digitTwo + "  " + digitThree;
    }

    void OpenLock()
    {
        OpenSafe.SetActive(true);
        endText.SetActive(true);
        Destroy(DigitOne);
        Destroy(DigitTwo);
        Destroy(DigitThree);
        Destroy(SafeText);
        Destroy(gameObject);
    }
}
