using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject player;
    private AudioSource engineSound;

    private float speed;

    private bool audioPlaying;

    void Awake()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        engineSound = player.GetComponent<AudioSource>();
    }


    void Update()
    {
        speed = player.GetComponent<CarController>().speed;

        if (gameManager.GetComponent<GameManager>().playing && audioPlaying == false)
        {
            audioPlaying = true;
            engineSound.Play();
        }
        else if (gameManager.GetComponent<GameManager>().playing == false && audioPlaying)
        {
            audioPlaying = false;
            engineSound.Stop();
        }

        if (speed > 110)
        {
            speed = 110;
        }
        else
        {
            engineSound.pitch = speed / 120 + 0.1f;
        }
    }
}
