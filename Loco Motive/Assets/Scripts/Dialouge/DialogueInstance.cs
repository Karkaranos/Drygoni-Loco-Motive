using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstance : MonoBehaviour
{
    public int currMessage;
    public List<DIalogueMessage> AllMessages;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProgText()
    {
        if (AllMessages[currMessage].Branch.Count == 0)
        {

        }
        else
        {

        }
    }

    public void UpdateDisplay()
    {
        
    }
}
