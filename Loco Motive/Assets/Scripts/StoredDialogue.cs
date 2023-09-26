using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredDialogue : MonoBehaviour
{
    public string[] openingDialogue = new string[1];
    public string[] openingResponses = new string[2];
    public string[] openingNames = new string[1];
    public string[] openingB1 = new string[6];
    public string[] oB1Names = new string[6];
    public string[] openingB2 = new string[7];
    public string[] oB2Names = new string[7];

    public string[] numPad = new string[2];
    public string[] numPadNames = new string[2];

    public string[] hDialogue = new string[1];

    public string[] hDialogueIQuestion = new string[3];
    public string[] hDialogueIB1 = new string[7];
    public string[] hDialogueIB2 = new string[6];
    public string[] hDialogueIB3 = new string[2];
    public string[] hIB1Names = new string[7];
    public string[] hIB2Names = new string[6];
    public string[] hIB3Names = new string[2];

    public string[] sOneDialogue = new string[1];
    //public string[] sOneResponse = new string[2];
    //public string[] sOneDialogueB1 = new string[3];
    //public string[] sOneDialogueB2 = new string[3];

    public string[] sOneQuestionsI1 = new string[2];
    public string[] sOneDialogueI1B1 = new string[3];
    public string[] sOneDialogueI1B2 = new string[5];
    public string[] sOneI1B1Names = new string[3];
    public string[] sOneI1B2Names = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        openingDialogue[0] = "Have you been on a train before?";

        openingB1[0] = "Yes, but it wasn't this fancy. It was just a standard passenger train I would commute on.";
        openingB1[1] = "Whoa, you went on a train every day? That’s not fair!";
        openingB1[2] = "It wasn’t as nice as you’re thinking.";
        openingB1[3] = "We should get started right away with our investigation, right?";
        openingB1[4] = "Yeah, let’s start ";
        openingB1[5] = "Alex is in the other room if you need to interrogate him about anything you’ve found";

        openingResponses[0] = "Yes";
        openingResponses[1] = "No";

        oB1Names[0] = "Casey";
        oB1Names[1] = "Hunter";
        oB1Names[2] = "Casey";
        oB1Names[3] = "Hunter";
        oB1Names[4] = "Casey";
        oB1Names[5] = "Hunter";

        openingB2[0] = "No, I don’t think I ever have been.";
        openingB2[1] = "What? Never? Are you scared?";
        openingB2[2] = "What should I be scared of? The train monster?";
        openingB2[3] = "Well I was thinking the train could get derailed but if there’s a train monster you should definitely be afraid of that instead. There isn’t a train monster, right?";
        openingB2[4] = "We should get started right away with our investigation, right?";
        openingB2[5] = "Yeah, let’s start ";
        openingB2[6] = "Alex is in the other room if you need to interrogate him about anything you’ve found";

        oB2Names[0] = "Casey";
        oB2Names[1] = "Hunter";
        oB2Names[2] = "Casey";
        oB2Names[3] = "Hunter";
        oB2Names[4] = "Hunter";
        oB2Names[5] = "Casey";
        oB2Names[6] = "Hunter";

        numPad[0] = "Hey, check it out";
        numPad[1] = "What do you think the combination is?";

        numPadNames[0] = "Casey";
        numPadNames[1] = "Hunter";

        hDialogue[0] = "How are you doing?";

        hDialogueIQuestion[0] = "Number Pad";
        hDialogueIQuestion[1] = "Behavior";
        hDialogueIQuestion[2] = "Luggage";

        hDialogueIB1[0] = "Do you have any idea what the combination for the number pad is?";
        hDialogueIB1[1] = "Well, if it was me I would do my birthday!";
        hDialogueIB1[2] = "Your birthday? I don’t think it will be your birthday but it could be Alexander’s or his kids. When is your birthday Hunter?";
        hDialogueIB1[3] = "What?";
        hDialogueIB1[4] = "I was just curious when your birthday was.";
        hDialogueIB1[5] = "You mean you don’t remember? It was yesterday :(";
        hDialogueIB1[6] = "Oh was it? Happy late birthday then.";

        hIB1Names[0] = "Casey";
        hIB1Names[1] = "Hunter";
        hIB1Names[2] = "Casey";
        hIB1Names[3] = "Hunter";
        hIB1Names[4] = "Casey";
        hIB1Names[5] = "Hunter";
        hIB1Names[6] = "Casey";

        hDialogueIB2[0] = "You’ve been on your best behavior, right?";
        hDialogueIB2[1] = "Of course!";
        hDialogueIB2[2] = "...";
        hDialogueIB2[3] = "...";
        hDialogueIB2[4] = "Ok, well, I did steal a croissant earlier…";
        hDialogueIB2[5] = "I knew it.";

        hIB2Names[0] = "Casey";
        hIB2Names[1] = "Hunter";
        hIB2Names[2] = "Casey";
        hIB2Names[3] = "Hunter";
        hIB2Names[4] = "Hunter";
        hIB2Names[5] = "Casey";

        hDialogueIB3[0] = "Why did you pack so much? I told you to pack light.";
        hDialogueIB3[1] = "This is light! I only brought my weekend dumbbells! Don’t tell me, 150 is heavy to you?";

        hIB3Names[0] = "Casey";
        hIB3Names[1] = "Hunter";


        sOneDialogue[0] = "Hello there, how are you doing today?";

        sOneQuestionsI1[0] = "Kid";
        sOneQuestionsI1[1] = "Destination";
        
        sOneDialogueI1B1[0] = "Is this your kid in the photo?";
        sOneDialogueI1B1[1] = "Yes! I’m so glad you asked! That’s my son Joseph and he is the sweetest little boy in the entire world! I can still remember the first time I held him. He was born on September 9, 2003. He was so small back then, I could hold up above my head but he’s a bit too big for that now. Still, that's probably the happiest day of my life!";
        sOneDialogueI1B1[2] = "Right, I’ve heard enough. (He seems to really love his kid, 9/9/03? That’s probably an important date to remember.)";

        sOneDialogueI1B2[0] = "So, um, where are we going?";
        sOneDialogueI1B2[1] = "You got onto a train without knowing where it was headed? That doesn’t seem safe.";
        sOneDialogueI1B2[2] = "Well I was just doing what my boss asked me, the consequences of that were an after thought.";
        sOneDialogueI1B2[3] = "Well, we’re going to France!";
        sOneDialogueI1B2[4] = "No, not France. Can’t we go to Italy instead?";

        sOneI1B1Names[0] = "Casey";
        sOneI1B1Names[1] = "Alexander";
        sOneI1B1Names[2] = "Casey";

        sOneI1B2Names[0] = "Casey";
        sOneI1B2Names[1] = "Alexander";
        sOneI1B2Names[2] = "Casey";
        sOneI1B2Names[3] = "Alexander";
        sOneI1B2Names[4] = "Casey";
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
