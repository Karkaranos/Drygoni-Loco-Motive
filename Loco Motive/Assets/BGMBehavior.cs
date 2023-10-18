using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numULSC = FindObjectsOfType<BGMBehavior>().Length;
        if (numULSC != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
