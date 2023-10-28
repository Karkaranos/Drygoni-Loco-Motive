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

    /// <summary>
    /// Called upon the scene's start; 
    /// </summary>
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        creditCanvas = GameObject.Find("CreditCanvas");
        creditText = GameObject.Find("Credits");
        creditCanvas.SetActive(false);
        creditResetPos = new Vector2(450, 250);

        titleCanvas = GameObject.Find("TitleCanvas");
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
       creditText.transform.position = creditResetPos;
        for (int i=0; i<1350; i += 1)
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

}
