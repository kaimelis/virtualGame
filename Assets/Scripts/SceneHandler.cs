using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public Animator OptionsMenu;
    public Slider MouseSensitivityX;
    public Slider MouseSensitivityY;
    public Slider Volume;
    public Toggle fullscreen;

    public void LoadScene(int number)
    {
        SceneManager.LoadScene(number);

    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        OptionsMenu.enabled = true;
    }
    public void VolumeChange()
    {
        AudioListener.volume = Volume.value;
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }



}
