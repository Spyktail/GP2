using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static bool paused = false; 

    public GameObject menu;
    PauseAction action;

    //private bool paused;
    private void Awake()
    {
        action = new PauseAction();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        paused = true;
        menu.SetActive(true);
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        paused = false;
        menu.SetActive(false);
    }
}
