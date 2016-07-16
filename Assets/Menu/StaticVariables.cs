using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaticVariables : MonoBehaviour {
    public Slider x;
    public Slider y;
    public Slider vol;
    public static float sensitivityX;
    public static float sensitivityY;
    public static float Volume;
    // Use this for initialization
    void Start ()
    {
        sensitivityX = x.value;
        sensitivityY = y.value;
        Volume = vol.value;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        sensitivityX = x.value;
        sensitivityY = y.value;
        Volume = vol.value;
    }
}
