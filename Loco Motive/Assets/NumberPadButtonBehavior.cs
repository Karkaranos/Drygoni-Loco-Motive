/*****************************************************************************
// File Name :         NumberPadButtonBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     September 22, 2023
//
// Brief Description :  Passes an integer to Number Pad Behavior
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPadButtonBehavior : MonoBehaviour
{
    public int number;
    [SerializeField] GameObject npbObject;
    private NumberPadBehavior npb;
    
    /// <summary>
    /// Start is called before the first frame update. It initializes a variable.
    /// </summary>
    void Start()
    {
        npb = npbObject.GetComponent<NumberPadBehavior>();
    }

    /// <summary>
    /// Passes a value to the NumberPressed function of number pad behavior when 
    /// the object with this script is clicked
    /// </summary>
    public void OnClick()
    {
        npb.NumberPressed(number);
    }
}
