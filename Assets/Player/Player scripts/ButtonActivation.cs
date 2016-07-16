using UnityEngine;
using System.Collections;
using PickUps;
using UnityEngine.UI;


public class ButtonActivation : MonoBehaviour
{
    public AudioClip button1;
    public AudioClip button2;
    public AudioClip button3;
    public AudioClip button4;
    private bool b1;
    private bool b2;
    private bool b3;
    private bool b4;
    public float buttoncounter;

    public Renderer sliderorange;
    public Renderer sliderYellow;
    public Renderer sliderPink;
    public Renderer sldierBlue;

    public Material Blue;
    public Material yelellow;
    public Material Pink;
    public Material Orange;

    public float Range = 10;
    public Transform Camera;
    private GameObject _button;
    private RaycastHit _hitInfo;
    private Ray _ray;
    public EndDoorTrigger EndDoorTrigger;
    public GameObject PressButton;
    private bool _pressedB, _pressedP, _pressedY, _pressedO;
    public WinScreenShow winning;

    void Star()
    {
        buttoncounter = 0;
        SetPressedButtonsToFalse();
    }

    private void PlayOnButtonPressSpeech()
    {       
        if (buttoncounter == 1)
            SoundManager.instance.PlaySingle(button1);
        else if(buttoncounter == 2)
            SoundManager.instance.PlaySingle(button2);
        else if (buttoncounter == 3)
            SoundManager.instance.PlaySingle(button3);
        else if (buttoncounter == 4)
            SoundManager.instance.PlaySingle(button4);
    }
    private void SetPressedButtonsToFalse()
    {
        _pressedB = false;
        _pressedP = false;
        _pressedY = false;
        _pressedO = false;
        b1 = false;
        b2 = false;
        b3 = false;
        b4 = false;
    }

    void FixedUpdate()
    {
        //if(Input.GetKeyDown(KeyCode.F)) 
        LineOfAimHandler(); 
    }
    

    void LineOfAimHandler() // Makes the gun always point at the end of ray (crosshair point)
    {
        _ray = new Ray(Camera.transform.position, Camera.transform.forward);
        if (Physics.Raycast(_ray, out _hitInfo, Range))
        {
            ShowPressFText();
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_hitInfo.collider.gameObject.tag == "buttonYellow")
                {
                    if (!b1)
                    {
                        sliderYellow.material = yelellow;
                        buttoncounter += 1;
                        b1 = true;
                        PlayOnButtonPressSpeech();
                    }
                    _button = _hitInfo.collider.gameObject;
                    _button.GetComponent<Animator>().enabled = true;
                    EndDoorTrigger.YellowButton = true;
                    _pressedY = true;
                    ButtonFalse();
                    //Debug.Log("yellow true");
                }
                else if (_hitInfo.collider.gameObject.tag == "buttonBlue")
                {
                    if (!b2)
                    {
                        sldierBlue.material = Blue;
                        buttoncounter += 1;
                        b2 = true;
                        PlayOnButtonPressSpeech();
                    }
                    _button = _hitInfo.collider.gameObject;
                    _button.GetComponent<Animator>().enabled = true;
                    EndDoorTrigger.BlueButton = true;
                    _pressedB = true;
                    ButtonFalse();
                    //Debug.Log("blue true");
                }
                else if (_hitInfo.collider.gameObject.tag == "buttonOrange")
                {
                    if (!b3)
                    {
                        sliderorange.material = Orange;
                        buttoncounter += 1;
                        b3 = true;
                        PlayOnButtonPressSpeech();
                    }
                    _button = _hitInfo.collider.gameObject;
                    _button.GetComponent<Animator>().enabled = true;
                    EndDoorTrigger.OrangeButton = true;
                    _pressedO = true;
                    ButtonFalse();
                   // Debug.Log("orange true");
                }
                else if (_hitInfo.collider.gameObject.tag == "buttonPink")
                {
                    if (!b4)
                    {
                        sliderPink.material = Pink;
                        buttoncounter += 1;
                        b4 = true;
                        PlayOnButtonPressSpeech();
                    }
                    _button = _hitInfo.collider.gameObject;
                    _button.GetComponent<Animator>().enabled = true;
                    EndDoorTrigger.PinkButton = true;
                    _pressedP = true;
                    ButtonFalse();
                    //Debug.Log("pink true");
                }
                else if (_hitInfo.collider.gameObject.tag == "button")
                {
                    _button = _hitInfo.collider.gameObject;
                    _button.GetComponent<Animator>().enabled = true;
                    winning.enabled = true;
                    // Debug.Log("end button. game over dude for you");

                    //show game over play sound
                }
            }
        }
    }

    void ButtonTrue()
    {
        PressButton.SetActive(true);
    }

    void ButtonFalse()
    {
        PressButton.SetActive(false);
    }

    private void ShowPressFText()
    {
        if (_hitInfo.collider.gameObject.tag == "buttonPink" && !_pressedP)
        {
            ButtonTrue();
        }
        else if (_hitInfo.collider.gameObject.tag == "buttonOrange" && !_pressedO)
        {
            ButtonTrue();
        }
        else if (_hitInfo.collider.gameObject.tag == "buttonYellow" && !_pressedY)
        {
            ButtonTrue();
        }
        else if (_hitInfo.collider.gameObject.tag == "buttonBlue" && !_pressedB)
        {
            ButtonTrue();
        }
        else
        {
            ButtonFalse();
        }
    }
}
                