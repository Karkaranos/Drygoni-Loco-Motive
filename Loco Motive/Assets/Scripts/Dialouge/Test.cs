using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    DialougeSystem dialouge;
    public DialougeMessage[] MyMessages;

    //string sceneName;
    //public GameObject ChoicePanel;

    public AudioSource Level;
    public AudioClip Click;
    public bool InEnding;

    // Start is called before the first frame update
    void Start()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        dialouge = DialougeSystem.instance;
        //sceneName = currentScene.name;
        
    }


    int i = 0;
    // Update is called once per frame
    void Update()
    {
        // if (GameController.Gc.CurrentLevel.CurrentState == LevelController.LevelState.dialogue)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (i >= MyMessages.Length)
                {

                    //Idk what we need this for yet
                    return;
                }
                Say(MyMessages[i]);
                Level.PlayOneShot(Click);
                i++;

            }
        }
        void Say(DialougeMessage message)
        {
            dialouge.Say(message);
        }
    }
}


