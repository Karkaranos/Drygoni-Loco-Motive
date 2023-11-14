/*****************************************************************************
// File Name :         UIButtonManager.cs
// Author :            Cade R. Naylor
// Creation Date :     October 16, 2023
//
// Brief Description :  Handles UI Buttons and Title Screen behavior
*****************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    private AudioManager am;
    private GameObject creditText;
    private GameObject titleCanvas;
    private GameObject creditCanvas;
    private Vector2 creditResetPos;
    private GameObject pauseMenu;
    public bool isPaused;
    private GameObject map;
    private GameObject notebookIcon;
    private GameObject inventoryIcon;
    private DialogueController dc;
    private GameObject movementArrows;
    private GameObject settingsMenu;
    private bool inSettings;
    /// <summary>
    /// Called upon the scene's start; 
    /// </summary>
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            creditCanvas = GameObject.Find("CreditCanvas");
            creditText = GameObject.Find("Credits");
            creditCanvas.SetActive(false);
            settingsMenu = GameObject.Find("Settings");
            settingsMenu.SetActive(false);
    
            titleCanvas = GameObject.Find("TitleCanvas");
        }
        else if(SceneManager.GetActiveScene().name == "MainScene" || 
                SceneManager.GetActiveScene().name == "TutorialScene")
        {
            pauseMenu = GameObject.Find("PauseMenu");
            map = GameObject.Find("Map");
            notebookIcon = GameObject.Find("NotebookIcons");
            inventoryIcon = GameObject.Find("InventoryIcons");
            dc = FindObjectOfType<DialogueController>();
            movementArrows = GameObject.Find("Movement");
            settingsMenu = GameObject.Find("Settings");
            settingsMenu.SetActive(false);
        }

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

    /// <summary>
    /// Moves the player to the tutorial
    /// </summary>
    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
        am.PlayTutorialMusic();
    }

    /// <summary>
    /// Shows the credits
    /// </summary>
    public void Credits()
    {
        titleCanvas.SetActive(false);
        creditCanvas.SetActive(true);
        StartCoroutine(CreditScroll());
    }


    /// <summary>
    /// Scrolls through the credits at a set rate
    /// </summary>
    /// <returns>Time between each position incrmement</returns>
    IEnumerator CreditScroll()
    {
       creditText.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        for (int i=0; i<1500; i += 1)
        {
            Vector2 creditPos = creditText.transform.position;
            creditPos.y += Screen.height/500f;
            creditText.transform.position = creditPos;
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(1f);
        titleCanvas.SetActive(true);
        creditCanvas.SetActive(false);
    }

    /// <summary>
    /// Restarts the current scene
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Resumes the game when paused
    /// </summary>
    public void Resume()
    {
        dc.isTalking = false;
        pauseMenu.SetActive(false);
        if (dc != null && !dc.isTalking && !inSettings)
        {
            isPaused = false;
            movementArrows.SetActive(true);

            for (int i = 0; i < 2; i++)
            {
                if (notebookIcon != null)
                {
                    notebookIcon.SetActive(true);
                }
                else
                {
                    notebookIcon = GameObject.Find("NotebookIcons");
                }

                if (inventoryIcon != null)
                {
                    inventoryIcon.SetActive(true);
                }
                else
                {
                    inventoryIcon = GameObject.Find("InventoryIcons");

                }

                if (map != null)
                {
                    map.SetActive(true);
                }
                else
                {
                    notebookIcon = GameObject.Find("Map");
                }
            }
            am.EndPauseMusic();
        }

    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        if(dc!=null && !dc.isTalking)
        {
            isPaused = true;
            dc.isTalking = true;
            pauseMenu.SetActive(true);
            movementArrows.SetActive(false);

            for (int i = 0; i < 2; i++)
            {
                if (notebookIcon != null)
                {
                    notebookIcon.SetActive(false);
                }
                else
                {
                    notebookIcon = GameObject.Find("NotebookIcons");
                }

                if (inventoryIcon != null)
                {
                    inventoryIcon.SetActive(false);
                }
                else
                {
                    inventoryIcon = GameObject.Find("InventoryIcons");

                }

                if (map != null)
                {
                    map.SetActive(false);
                }
                else
                {
                    notebookIcon = GameObject.Find("Map");
                }
            }
            am.PlayPauseMusic();
        }
    }

    public void OpenSettings()
    {
        inSettings = true;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        inSettings = false;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        settingsMenu.SetActive(false);
    }

}
