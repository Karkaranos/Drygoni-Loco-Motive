using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeSystem : MonoBehaviour
{
    public static DialougeSystem instance;

    public GameObject SpeechPanel;
    public TMP_Text SpeakerNameText;
    public TMP_Text SpeechText;

    public Image Player;
    public Image Maiden;

    public bool IsWaitingForInput = false;
    Coroutine Speaking = null;

    public Sprite PlayerM;
    public Sprite PlayerF;

   
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var playerCharcater = PlayerPrefs.GetInt("SelectedCharacter");
        if (playerCharcater == 0)
        {
            Player.sprite = PlayerM; // check player pref
        }
        else if (playerCharcater == 1)
        {
            Player.sprite = PlayerF;
        }
    }
    public void Say(DialougeMessage message)
    {
        var playerCharcater = PlayerPrefs.GetInt("SelectedCharacter");
        if (playerCharcater == 0)
        {
            Player.sprite = message.HeroM; // check player pref
        }
        else if (playerCharcater == 1)
        {
            Player.sprite = message.HeroF;
        }
        Maiden.sprite = message.Maiden;
        StopSpeaking();
        Speaking = StartCoroutine(speaking(message.Message, message.Speaker));
    }
    public void StopSpeaking()
    {
        if (Speaking != null)
        {
            SpeakerNameText.text = "";
            SpeechText.text = "";
            StopCoroutine(Speaking);

            Speaking = null;
        }
    }
    /*if(Speaking != null)
        {
        IsSpeaking = true;
        }*/


    IEnumerator speaking(string targetSpeech, string speaker)
    {
        SpeechPanel.SetActive(true);
        SpeechText.text = "";
        SpeakerNameText.text = speaker;
        IsWaitingForInput = false;
        
        while (SpeechText.text != targetSpeech)
        {
            SpeechText.text += targetSpeech[SpeechText.text.Length];
            yield return new WaitForSeconds(.01f);
        }
        IsWaitingForInput = true;
        while (IsWaitingForInput)
        {
            yield return new WaitForEndOfFrame();
        }
        StopSpeaking();
    }
}
//  public class Elements





