using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ClickController : MonoBehaviour
{
    private int arrLength = 0;
    private bool isHighlighted = false;
    private PlayerInput mouseController;

    private InputAction mPos;
    private InputAction interact;

    private Vector2 currPos;

    public ComboLockController clc;
    private StoredDialogue sd;
    public DialogueController dc;
    public InventoryBehavior ib;

    private Color originalColor;
    private GameObject HighlightedObject;

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


    // Start is called before the first frame update
    void Start()
    {
        mouseController = GetComponent<PlayerInput>();
        mouseController.currentActionMap.Enable();

        nm = GameObject.Find("NotebookManager").GetComponent<NotebookManager>();

        mPos = mouseController.currentActionMap.FindAction("MousePosition");
        interact = mouseController.currentActionMap.FindAction("Interact");

        interact.performed += Interact_performed;

        dc.ContinueText.text = "Continue";

        dc.DialogueScreen.SetActive(false);
        dc.InterrogationScreen.SetActive(false);
        OpenLockBox.SetActive(false);

        dc.opening = true;
        

        MapRooms[4].GetComponent<SpriteRenderer>().color = Color.blue;
    }



    void Update()
    {
        currPos = mPos.ReadValue<Vector2>();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(currPos), Vector2.zero);

        if (hit.collider != null && dc.isTalking == false)
        {
            if (isHighlighted == false)
            {
                HighlightedObject = hit.collider.gameObject;
                originalColor = HighlightedObject.GetComponent<SpriteRenderer>().color;
                HighlightedObject.GetComponent<SpriteRenderer>().color = Color.white;
                isHighlighted = true;
            }
        }
        
        else if (isHighlighted == true)
        {
            HighlightedObject.GetComponent<SpriteRenderer>().color = originalColor;
            isHighlighted = false;

        }
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
            if (hit.transform.GetComponent<RoomMove>())
            {
                transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                Map.transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                Vector2 pos = transform.position;
                pos.x += -15.45f;
                pos.y = 2.5f;
                Map.transform.position = pos;
                arrLength = 0;
                while (arrLength < MapRooms.Length)
                {
                    MapRooms[arrLength].GetComponent<SpriteRenderer>().color = Color.white;
                    arrLength++;
                }
                int roomNum = hit.collider.gameObject.GetComponent<RoomMove>().connectedRoom.roomNum;
                MapRooms[roomNum].GetComponent<SpriteRenderer>().color = Color.blue;
            }

            else if (hit.collider.GetComponent<DialogueInstance>())
            {
                dc.currentDialogue = hit.collider.GetComponent<DialogueInstance>();
                dc.StartDialogue();
            }
            else if (hit.collider.CompareTag("SuspectOne"))
            {
                dc.currTalkChar = 1;
                dc.strLength = 0;
                dc.StartDialogue();
            }

            else if (hit.collider.CompareTag("DigitOne"))
            {
                if (clc.digitOne == 9)
                {
                    clc.digitOne = 0;
                }
                else
                {
                    clc.digitOne += 1;
                }
            }

            else if (hit.collider.CompareTag("DigitTwo"))
            {
                if (clc.digitTwo == 9)
                {
                    clc.digitTwo = 0;
                }
                else
                {
                    clc.digitTwo += 1;
                }
            }

            else if (hit.collider.CompareTag("DigitThree"))
            {
                if (clc.digitThree == 9)
                {
                    clc.digitThree = 0;
                }
                else
                {
                    clc.digitThree += 1;
                }
            }

            else if (hit.collider.CompareTag("NumPadCollider"))
            {
                dc.numPadDialogue = true;
                dc.needNumInfo = true;
                dc.StartDialogue();
                NumPadCollider.SetActive(false);
                //dc.strLength = 1;
            }

            else if (hit.collider.CompareTag("Lock"))
            {
                if (ib.keyCollected == true)
                {
                    OpenLockBox.SetActive(true);
                    Lock.SetActive(false);
                    //Replace ITEM2 with the correct enum of the key
                    ib.RemoveItemFromInventory(InventoryBehavior.Items.ITEM2);
                }
            }
        }
    }



    
}