using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public RoomMove[] roomReference;
    public Arrow[] movementArrows;
    public ClickController CC;
    public int requirementsToUnlock;
    [SerializeField] private GameObject HCDoorFlavor;
    [SerializeField] private GameObject HCDoorEnter;
    private NotebookContentManager ncm;



    private void Start()
    {
        CC = FindObjectOfType<ClickController>();
        ncm = FindObjectOfType<NotebookContentManager>();
        for(int i=0; i<movementArrows.Length; i++)
        {
            movementArrows[i].relatedObject.SetActive(false);
        }
        EnableArrows(1);
        EnableArrows(0);
        EnableArrows(2);
        EnableArrows(4);
        HCDoorEnter.SetActive(false);

        for(int i=0; i<3; i++)
        {
            ncm.AdvancedInformationVisible(i);
        }

        for(int i=0; i<5; i++)
        {
            ncm.RevealEvent(i);
        }


    }

    public void EnableRoom(int index)
    {
        roomReference[index].canBeAccessed = true;
    }

    public void EnableArrows(int index)
    {
        for(int i=0; i<movementArrows.Length; i++)
        {
            if(movementArrows[i].containedInRoom == index)
            {
                movementArrows[i].relatedObject.SetActive(true);
            }
        }
    }

    public void IncreaseRequirement()
    {
        if(CC.currentRoom==8 && requirementsToUnlock == 2)
        {
            requirementsToUnlock = 0;
            EnableArrows(8);
            EnableRoom(0);
        }
        else if ((CC.currentRoom == 3 || CC.currentRoom==4) && requirementsToUnlock == 5)
        {
            requirementsToUnlock = 0;
            EnableArrows(3);
            EnableRoom(1);
        }
        else if (CC.currentRoom == 3)
        {
            EnableRoom(2);
        }
        else if (CC.currentRoom == 2)
        {
            EnableRoom(3);
        }
        else if (CC.currentRoom == 0)
        {
            EnableRoom(4);
        }
        else if (CC.currentRoom == 1)
        {
            EnableRoom(5);
        }
        if (HCDoorFlavor == null)
        {
            HCDoorEnter.SetActive(true);
        }
    }

    public void StartGame()
    {
        AudioManager am = FindObjectOfType<AudioManager>();
        am.PlayGameMusic();
        SceneManager.LoadScene(2);
    }
}
