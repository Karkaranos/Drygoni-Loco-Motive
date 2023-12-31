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
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject map;
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
            //pauseMenu.SetActive(false);
    
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
            pauseMenu.SetActive(false);
        }

    }

    /// <summary>
    /// Moves the player to the main scene
    /// </summary>
    public void StartGame()
    {
        if (am != null)
        {
            am.Play("Click");
            am.PlayGameMusic();
        }
        SceneManager.LoadScene("MainScene");

    }

    /// <summary>
    /// Moves the player to the title screen
    /// </summary>
    public void TitleScreen()
    {
        if (am != null)
        {
            am.Play("Click");
            am.PlayMenuMusic();
        }
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Moves player to a win Screen
    /// </summary>
    public void WinScreen()
    {
        if (am != null)
        {
            am.Play("Click");
            am.PlayMenuMusic();
        }
        SceneManager.LoadScene("WinScene");
    }

    /// <summary>
    /// Moves the player to the tutorial
    /// </summary>
    public void Tutorial()
    {
        if (am != null)
        {
            am.Play("Click");
            am.PlayTutorialMusic();
        }
        SceneManager.LoadScene("TutorialScene");
    }

    /// <summary>
    /// Shows the credits
    /// </summary>
    public void Credits()
    {
        if (am != null)
        {
            am.Play("Click");
        }
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
        for (int i=0; i<1400; i += 1)
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
        if (am != null)
        {
            am.Play("Click");
            am.EndPauseMusic();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        Application.Quit();
    }

    /// <summary>
    /// Resumes the game when paused
    /// </summary>
    public void Resume()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        if (dc.opening == false)
        {
            dc.isTalking = false;
        }
        else
        {
            dc.isTalking = true;
        }
        
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
            if (am != null)
            {
                am.EndPauseMusic();
            }
        }

    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        if (dc!=null && !dc.isTalking)
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
            if (am != null)
            {
                am.PlayPauseMusic();
            }
        }
    }

    public void OpenSettings()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        inSettings = true;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (titleCanvas != null)
        {
            titleCanvas.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        if (am != null)
        {
            am.Play("Click");
        }
        inSettings = false;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        if (titleCanvas != null)
        {
            titleCanvas.SetActive(true);
        }
        settingsMenu.SetActive(false);
    }

}
