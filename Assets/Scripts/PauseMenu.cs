using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    private bool muted = false;
    public GameObject pauseMenuUI;

    public GameObject audio;

    public void OnPause(){
        if(GameIsPaused){
            Resume();
        } 
        else{
            Pause();
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void HandleAudio(){
        FindObjectOfType<AudioManager>().MuteAll();
        muted = !muted;
        if(muted)
            audio.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "ENABLE AUDIO";
        else
            audio.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "DISABLE AUDIO";
    }

    public void GoToMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainVR");
    }

}
