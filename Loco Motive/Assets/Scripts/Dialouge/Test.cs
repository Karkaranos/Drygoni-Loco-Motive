using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    private PlayerInput mouseController;

    private InputAction playDialogue;

    DialougeSystem dialouge;
    public DialougeMessage[] MyMessages;

    //string sceneName;
    //public GameObject ChoicePanel;

    //public AudioSource Level;
    //public AudioClip Click;
    public bool InEnding;

    // Start is called before the first frame update
    void Start()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        dialouge = DialougeSystem.instance;
        //sceneName = currentScene.name;

        mouseController = GetComponent<PlayerInput>();
        mouseController.currentActionMap.Enable();

        playDialogue = mouseController.currentActionMap.FindAction("ProgressDialogue");
    }


    int i = 0;
    // Update is called once per frame
    void Update()
    {
        // if (GameController.Gc.CurrentLevel.CurrentState == LevelController.LevelState.dialogue)
    }

    public void PlayDialogue()
    {
        if (i >= MyMessages.Length)
        {
            return;
        }
        Say(MyMessages[i]);
        //Level.PlayOneShot(Click);
        i++;

        void Say(DialougeMessage message)
        {
            dialouge.Say(message);
        }
    }
}


