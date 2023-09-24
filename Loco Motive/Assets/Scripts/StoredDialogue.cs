using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredDialogue : MonoBehaviour
{
    public string[] sOneDialogue = new string[1];
    public string[] sOneResponse = new string[2];
    public string[] sOneDialogueB1 = new string[3];
    public string[] sOneDialogueB2 = new string[3];

    public string[] sOneQuestionsI1 = new string[4];
    public string[] sOneDialogueI1B1 = new string[2];
    public string[] sOneDialogueI1B2 = new string[2];
    public string[] sOneDialogueI1B3 = new string[2];
    public string[] sOneDialogueI1B4 = new string[2];

    // Start is called before the first frame update
    void Start()
    {
        sOneDialogue[0] = "Hello there, how are you doing today?";

        sOneResponse[0] = "Good";
        sOneResponse[1] = "Not Great";

        sOneDialogueB1[0] = "That's good to hear";
        sOneDialogueB1[1] = "Anyways, it was good to talk with you";
        sOneDialogueB1[2] = "Have a good day";

        sOneDialogueB2[0] = "That's unfortunate";
        sOneDialogueB2[1] = "Well, it was fine talking with you";
        sOneDialogueB2[2] = "Have a day";

        sOneQuestionsI1[0] = "Please work correctly";
        sOneQuestionsI1[1] = "How are you?";
        sOneQuestionsI1[2] = "Words?";
        sOneQuestionsI1[3] = "Variable naming";

        sOneDialogueI1B1[0] = "Glad you asked";
        sOneDialogueI1B1[1] = "New Object Has Appeared!";

        sOneDialogueI1B2[0] = "I don't know";
        sOneDialogueI1B2[1] = "You should ask them instead";

        sOneDialogueI1B3[0] = "Words";
        sOneDialogueI1B3[1] = "Are hard";

        sOneDialogueI1B4[0] = "I1 stands for Interrogation 1";
        sOneDialogueI1B4[1] = "B4 stands for Branch 4";
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
