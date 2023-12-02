using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class ClickController : MonoBehaviour
{
    private int arrLength = 0;
    private PlayerInput mouseController;

    private InputAction mPos;
    private InputAction interact;
    private InputAction revealInteractables;
    private InputAction restart;
    private InputAction pause;
    private InputAction revealBC;
    public int currentRoom;

    private Vector2 currPos;

    public ComboLockController clc;
    public DialogueController dc;
    public InventoryBehavior ib;
    public NumberPadBehavior npb;
    public NumberPadBehavior npb2;

    public GameObject NumPadCollider;
    public GameObject Movement;
    public GameObject Map;
    public GameObject Lock;
    public GameObject OpenLockBox;
    private NotebookManager nm;
    public DialogueInstance OpeningDialogue;
    public DialogueInstance D1;

    public DialogueInstance D2;

    public DialogueInstance D3;

    //Update this to dialogue being used
    [SerializeField] private GameObject[] MapRooms;

    [SerializeField] private GameObject[] outlines;

    private AudioManager am;
    private UIButtonManager uibm;


    // Start is called before the first frame update
    void Start()
    {
        uibm = FindObjectOfType<UIButtonManager>();

        mouseController = GetComponent<PlayerInput>();
        mouseController.currentActionMap.Enable();

        nm = GameObject.Find("NotebookManager").GetComponent<NotebookManager>();
        am = GameObject.FindObjectOfType<AudioManager>();
        npb = GameObject.FindObjectOfType<NumberPadBehavior>();

        clc = FindObjectOfType<ComboLockController>();

        mPos = mouseController.currentActionMap.FindAction("MousePosition");
        interact = mouseController.currentActionMap.FindAction("Interact");
        restart = mouseController.currentActionMap.FindAction("Restart");
        pause = mouseController.currentActionMap.FindAction("Pause");
        revealInteractables = mouseController.currentActionMap.FindAction("RevealInteractables");
        revealBC = mouseController.currentActionMap.FindAction("RevealBoneCounter");

        interact.performed += Interact_performed;
        restart.performed += Restart_performed;
        pause.performed += Pause_performed;
        revealInteractables.performed += Reveal_performed;
        revealInteractables.canceled += Reveal_canceled;
        revealBC.performed += RevealBC_performed;


        dc.ContinueText.text = "Continue";

        dc.InterrogationScreen.SetActive(false);
        OpenLockBox.SetActive(false);

        dc.opening = true;

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            currentRoom = 10;

        }
        else
        {
            currentRoom = 8;            
        }

        MapRooms[currentRoom].GetComponent<SpriteRenderer>().color = Color.blue;

        foreach (GameObject i in outlines)
        {
            i.SetActive(false);
        }

        uibm.map.SetActive(false);

        MapRooms[arrLength].GetComponent<SpriteRenderer>().color = Color.clear;
    }

    private void RevealBC_performed(InputAction.CallbackContext obj)
    {
        dc.BoneCounter.SetActive(true);
        dc.BoneCounterText.text = "Bones = " + dc.boneCounterValue;
    }

    private void OnDestroy()
    {
        interact.performed -= Interact_performed;
        restart.performed -= Restart_performed;
        pause.performed -= Pause_performed;
        revealInteractables.performed -= Reveal_performed;
        revealInteractables.canceled -= Reveal_canceled;
    }

    private void Restart_performed(InputAction.CallbackContext obj)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if (am != null)
        {
            am.StopAllSounds();
            am.PlayMenuMusic();
        }
    }

    private void Reveal_performed(InputAction.CallbackContext obj)
    {
        foreach (GameObject i in outlines)
        {
            if(i != null)
            {
                i.SetActive(true);
            }

        }
    }

    private void Reveal_canceled(InputAction.CallbackContext obj)
    {
        foreach (GameObject i in outlines)
        {
            if (i != null)
            {
                i.SetActive(false);
            }

        }
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if (uibm.isPaused)
        {
            uibm.Resume();
        }
        else
        {
            uibm.Pause();
        }
    }

    void Update()
    {
        currPos = mPos.ReadValue<Vector2>();
    }


    /// <summary>
    /// When the player clicks, uses raycasting to hit the gameObject they clicked,
    /// then checks what they hit if they hit something
    /// </summary>
    /// <param name="obj"></param>
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(currPos), Vector2.zero);

        if (hit.collider != null && dc.isTalking == false)
        {

            if (hit.transform.GetComponent<ObjectHandler>())
            {
                hit.transform.GetComponent<ObjectHandler>().Interact();
            }

            if (hit.transform.GetComponent<NumberPadButtonBehavior>())
            {
                hit.transform.GetComponent<NumberPadButtonBehavior>().OpenPad();
            }
            if (hit.transform.GetComponent<RoomMove>() && 
                hit.transform.GetComponent<RoomMove>().canBeAccessed)
            {
                if (am != null)
                {
                    am.PlayFootsteps();
                    am.PlayDoor();
                }
                transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                Map.transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                Vector2 pos = transform.position;
                //pos.x += -15.45f;
                //pos.y += 2.5f;
                Map.transform.position = pos;
                arrLength = 0;
                while (arrLength < MapRooms.Length)
                {
                    MapRooms[arrLength].GetComponent<SpriteRenderer>().color = Color.clear;
                    arrLength++;
                }
                int roomNum = hit.collider.gameObject.GetComponent<RoomMove>().connectedRoom.roomNum;
                MapRooms[roomNum].GetComponent<SpriteRenderer>().color = Color.blue;
                currentRoom = roomNum;

                if(currentRoom == 9&&am!=null)
                {
                    am.PlayCorporateLadderRoomMusic();
                }
                else
                {
                    if (am != null)
                    {
                        am.StopCorporateLadderRoomMusic();
                    }

                }
            }

            if (hit.collider.GetComponent<DialogueInstance>())
            {
                dc.currentDialogue = hit.collider.GetComponent<DialogueInstance>();
                dc.StartDialogue();
            }

            if (hit.collider.CompareTag("Lock"))
            {
                if (ib.keyCollected == true)
                {
                    OpenLockBox.SetActive(true);
                    Lock.SetActive(false);
                    //Replace ITEM2 with the correct enum of the key
                    ib.RemoveItemFromInventory("Key");
                }
            }

            else if (hit.collider.CompareTag("ComboLock"))
            {
                clc.OpenLock();
            }

            else if (hit.collider.CompareTag("Bed"))
            {
                TutorialManager tm = FindObjectOfType<TutorialManager>();
                tm.StartGame();
            }

            if (hit.collider.CompareTag("OneShotText"))
            {
                DialogueInstance di = hit.transform.GetComponent<DialogueInstance>();
                Destroy(di);
            }
        }
    }



    
}