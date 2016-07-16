using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour
{
    public AudioSource OpenSound;
    public AudioSource CloseSound;
    public GameObject door1;
    public GameObject door2;
    public KeyCardPickUps Key;

    private bool _readyToOpen;
    private bool _readyToClose;
    private bool _hasOpened;
    private bool _open;
    private bool _close;
    private int _counter;
    private int _limit;

 void Start()
    {
        _hasOpened = false;
        _readyToOpen = true;
        _readyToClose = false;
        _open = false;
        _close = false;
        _counter = 0;
        _limit = 15;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Open();
        Close();
    }
    void OnTriggerEnter(Collider col)
    {
        if (Key.doorKey && col.gameObject.tag == "Player")
        {
            _open = true;
            OpenSound.Play();
            // Debug.Log("door has to open");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CloseSound.Play();
            _close = true;
        }
    }
    void Close()
    {
        if (_readyToClose && _close)
        {
            if (_counter <= _limit)
            {
                if (gameObject.tag == "doorX")
                {
                    Vector3 stepVector = new Vector3(0.05f, 0, 0);
                    door1.transform.position += stepVector;
                    door2.transform.position -= stepVector;
                }
                else if (gameObject.tag == "doorY")
                {
                    Vector3 stepVector = new Vector3(0, 0, 0.05f);
                    door1.transform.position += stepVector;
                    door2.transform.position -= stepVector;
                }
                   
                _counter += 1;
                if (_counter > _limit)
                {
                    _readyToOpen = true;
                    _readyToClose = false;
                    _close = false;
                    _counter = 0;
                }
            }
        }
    }
    void Open()
    {
        if (_readyToOpen && _open)
        {
            if (_counter <= _limit)
            {
                if (gameObject.tag == "doorX")
                {
                    Vector3 stepVector = new Vector3(0.05f, 0, 0);
                    door1.transform.position -= stepVector;
                    door2.transform.position += stepVector;
                }
                else if (gameObject.tag == "doorY")
                {
                    Vector3 stepVector = new Vector3(0, 0, 0.05f);
                    door1.transform.position -= stepVector;
                    door2.transform.position += stepVector;
                }

                    _counter += 1;
                if (_counter > _limit)
                {
                    _readyToClose = true;
                    _readyToOpen = false;
                    _open = false;
                    _counter = 0;
                }
            }
        }
    }

}
