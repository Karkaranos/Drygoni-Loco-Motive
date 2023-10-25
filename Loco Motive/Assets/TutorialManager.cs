using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public RoomMove[] roomReference;
    public Arrow[] movementArrows;
    public ClickController CC;
    public int requirementsToUnlock;



    private void Start()
    {
        CC = FindObjectOfType<ClickController>();
        for(int i=0; i<movementArrows.Length; i++)
        {
            movementArrows[i].relatedObject.SetActive(false);
        }
        EnableArrows(1);
        EnableArrows(0);
        EnableArrows(2);
        EnableArrows(4);
    }

    public void EnableRoom(int index)
    {
        //roomReference[index].canBeAccessed = true;
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
        else if (CC.currentRoom == 3 && requirementsToUnlock == 4)
        {
            requirementsToUnlock = 0;
            EnableArrows(3);
            EnableRoom(1);
        }
    }
}
