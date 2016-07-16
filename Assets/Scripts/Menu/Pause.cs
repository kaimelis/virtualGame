using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private PlayerControls2 movment;
    private Crosshair cross;
    private bool isPaused;
    public GameObject granadesHud;
    public GameObject devicesHud;

    void Start()
    {
        isPaused = false;
        movment = GetComponent<PlayerControls2>();
        cross = GetComponent<Crosshair>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
            PauseGame();
        else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
            UnPause();
    }

    void PauseGame ()
    {
        granadesHud.SetActive(false);
        devicesHud.SetActive(false);
        pauseMenuPanel.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
        movment.enabled = false;
        cross.enabled = false;
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        granadesHud.SetActive(true);
        devicesHud.SetActive(true);
        pauseMenuPanel.SetActive(false);
        Cursor.visible = false;
        movment.enabled = true;
        cross.enabled = true;
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
	
}
