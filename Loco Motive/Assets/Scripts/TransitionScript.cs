using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public void GoToMainScene()
    {
        SceneManager.LoadScene(2);
    }
}
