using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenShow : MonoBehaviour {

    private string theText = "";
    private float minPause = 0.01f; //The higher the values the slower...
    private float maxPause = 0.1f; //...the typing will be
    private float fullStopPauseTime = 1f; //How long should we pause after a full stop?
    private float startDelay = 1f; //Add a start delay?
    private float minPitch = 1f; //Pitch of the...
    private float maxPitch = 1f; //...audiosource
    private float _pauseTime;
    public bool playSound = true; //Play a sound or silent?
    public AudioSource letterSound;

    public AudioClip missionAcomplished;
    [SerializeField]private AudioClip youdidwell;
    public Crosshair cross;
    public AudioSource explosion;
    private bool play;
    [Header("Win panel")]
    [SerializeField] private GameObject _gameWinCanvas;
    [SerializeField] private Text _gameWinText;
    public AudioSource background;
    void Start()
    {

        background.enabled = false;
        cross.enabled = false;
         theText = _gameWinText.text;
         _gameWinText.text = "";
        StartCoroutine(TypeText());
        
    }


    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySingle(missionAcomplished);
        yield return new WaitForSeconds(3.8f);
        _gameWinCanvas.SetActive(true);
        explosion.Play();
        SoundManager.instance.PlaySingle(youdidwell);
        if (startDelay > 0)
        {
            yield return new WaitForSeconds(startDelay);
        }
        foreach (char letter in theText.ToCharArray())
        {
            _gameWinText.text += letter;

            if (playSound)
            {
                letterSound.pitch = Random.Range(minPitch, maxPitch);
                letterSound.Play();
                // SoundManager.instance.PlaySingle(letterSound);
                yield return 0;
            }
            if (letter.ToString() == ".")
            {
                _pauseTime = Random.Range(minPause, maxPause) + fullStopPauseTime;
            }
            else
            {
                _pauseTime = Random.Range(minPause, maxPause);
            }
            yield return new WaitForSeconds(_pauseTime);
        }
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }


}
