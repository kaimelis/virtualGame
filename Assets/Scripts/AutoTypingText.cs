using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoTypingText : MonoBehaviour
{
    private string theText = "";
    private float minPause = 0.01f; //The higher the values the slower...
    private float maxPause = 0.1f; //...the typing will be
    private float fullStopPauseTime = 1f; //How long should we pause after a full stop?
    private float startDelay = 1f; //Add a start delay?
    private float minPitch = 1f; //Pitch of the...
    private float maxPitch = 1f; //...audiosource
    [SerializeField] private bool playSound = true; //Play a sound or silent?
    [SerializeField] private AudioSource letterSound;
    private float _pauseTime;

    [Header("Introduction Panel")]
    [SerializeField] private GameObject introduction;
    [SerializeField] private bool showCanvasStart = true;
    [SerializeField] private Text textToDisplay; //Display the text


    [SerializeField] private Animator elevator;
    [SerializeField] private PlayerControls2 player;
    [SerializeField] private Crosshair cross;
    public AudioSource background;
    void Start()
    {
        background.enabled = false;
        elevator.enabled = false;
        player.enabled = false;
        cross.enabled = false;
        theText = textToDisplay.text;
        textToDisplay.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        if (startDelay > 0) {
            yield return new WaitForSeconds(startDelay);
        }
        foreach (char letter in theText.ToCharArray())
        {
           textToDisplay.text += letter;

            if(playSound) { 
                letterSound.pitch = Random.Range(minPitch, maxPitch);
				letterSound.Play();
               // SoundManager.instance.PlaySingle(letterSound);
				yield return 0;
            }
            if(letter.ToString() == ".") { 
                _pauseTime = Random.Range(minPause, maxPause)+fullStopPauseTime;
            } else {
                _pauseTime = Random.Range(minPause, maxPause);
            }
			yield return new WaitForSeconds (_pauseTime);
		}
        yield return new WaitForSeconds(3f);
        elevator.enabled = true;
        player.enabled = true;
        cross.enabled = true;
        background.enabled = true;
        introduction.SetActive(!showCanvasStart);
    }
}
