using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject titleScreen;
    private GameObject controlsScreen;
    private GameObject playingScreen;
    private GameObject settingsScreen;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        titleScreen = GameObject.Find("TitleScreen");
        controlsScreen = GameObject.Find("ControlsScreen");
        playingScreen = GameObject.Find("PlayingScreen");
        settingsScreen = GameObject.Find("SettingsScreen");
    }


    void Start()
    {
        titleScreen.SetActive(true);
        controlsScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }


    public void ControlsPressed()
    {
        titleScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }


    public void StartPressed()
    {
        titleScreen.SetActive(false);
        playingScreen.SetActive(true);

        gameManager.GetComponent<GameManager>().playing = true;
    }


    public void SettingsPressed()
    {
        titleScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }


    public void ReturnToTitleScreen()
    {
        titleScreen.SetActive(true);
        controlsScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(false);

        gameManager.GetComponent<GameManager>().playing = false;
    }
}
