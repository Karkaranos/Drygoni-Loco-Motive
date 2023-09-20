using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private PlayerInput mouseController;

    private InputAction mPos;

    private InputAction interact;

    private Vector2 currPos;

    public ComboLockController clc;

    // Start is called before the first frame update
    void Start()
    {
        mouseController = GetComponent<PlayerInput>();
        mouseController.currentActionMap.Enable();

        mPos = mouseController.currentActionMap.FindAction("MousePosition");
        interact = mouseController.currentActionMap.FindAction("Interact");

        interact.performed += Interact_performed;


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

        if (hit.collider != null)
        {
            //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            if (hit.transform.GetComponent<RoomMove>())
            {
                transform.position = hit.transform.GetComponent<RoomMove>().connectedRoom.roomPos.position;
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
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

            if (hit.collider.CompareTag("DigitThree"))
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

        }
    }





}
