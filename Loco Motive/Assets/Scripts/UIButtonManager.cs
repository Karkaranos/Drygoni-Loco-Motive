/*****************************************************************************
// File Name :         UIButtonManager.cs
// Author :            Cade R. Naylor
// Creation Date :     October 16, 2023
//
// Brief Description :  Handles UI Buttons
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    /// <summary>
    /// Moves the player to the main scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Moves the player to the title screen
    /// </summary>
    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }




}
