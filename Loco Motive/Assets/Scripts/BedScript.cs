using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    [SerializeField] DialogueInstance di;
    [SerializeField] InterrogationInstance ii;
    // Update is called once per frame

    private void Start()
    {
        //StartCoroutine(ResetValues());
    }

    IEnumerator ResetValues()
    {
        while (true)
        {
            if (ii.currCounter > 0&&GameObject.FindObjectOfType<DialogueController>().isTalking == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    ii.AllMessages[i].hasRead = false;
                    Debug.Log("GA");
                }
                ii.currCounter = 0;
                di.canInterrogate = true;
            }
            yield return new WaitForSeconds(3);
        }
    }
}
