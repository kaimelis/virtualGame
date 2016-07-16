using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverShow : MonoBehaviour {

    private string theText = "";
    private float minPause = 0.01f; //The higher the values the slower...
    private float maxPause = 0.1f; //...the typing will be
    private float fullStopPauseTime = 1f; //How long should we pause after a full stop?
    private float startDelay = 1f; //Add a start delay?
    private float minPitch = 1f; //Pitch of the...
    private float maxPitch = 1f; //...audiosource
    private float _pauseTime;
    [SerializeField]
    private bool playSound = true; //Play a sound or silent?
    [SerializeField]
    private AudioSource letterSound;
    [SerializeField] private Crosshair cross;
    [Header("GameOver panel")]
    [SerializeField]
    private GameObject _gameOverCanvas;
    [SerializeField]
    private Text _gameOverText;
    public AudioSource background;

    void Start()
    {
        background.enabled = false;
        cross.enabled = false;
            _gameOverCanvas.SetActive(true);
            theText = _gameOverText.text;
            _gameOverText.text = "";
            StartCoroutine(TypeText());
       
        
    }


    IEnumerator TypeText()
    {

        if (startDelay > 0)
        {
            yield return new WaitForSeconds(startDelay);
        }
        foreach (char letter in theText.ToCharArray())
        {
            _gameOverText.text += letter;

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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
