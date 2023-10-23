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
    private AudioManager am;
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    /// <summary>
    /// Moves the player to the main scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        am.PlayGameMusic();
    }

    /// <summary>
    /// Moves the player to the title screen
    /// </summary>
    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
        am.PlayMenuMusic();
    }

    /// <summary>
    /// Moves player to a win Screen
    /// </summary>
    public void WinScreen()
    {
        SceneManager.LoadScene("WinScene");
        am.PlayMenuMusic();
    }




}
