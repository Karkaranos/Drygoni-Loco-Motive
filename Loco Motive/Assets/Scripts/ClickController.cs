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

            if (hit.transform.GetComponent<ObjectHandler>())
            {
                hit.transform.GetComponent<ObjectHandler>().Interact();
            }
        }
    }





}
