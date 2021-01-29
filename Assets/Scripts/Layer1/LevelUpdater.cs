using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpdater : MonoBehaviour
{
    private GameObject gameManager;

    private bool updating;

    /* A controller for level updates that take place outside of normal game
       update times, such as changing traffic lights. */
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().playing && updating == false)
        {
            updating = true;
            StartUpdates();
        }
        else if (gameManager.GetComponent<GameManager>().playing == false && updating)
        {
            updating = false;
        }
    }

    public void StartUpdates()
    {
        StartCoroutine(gameObject.GetComponent<ChangeTrafficLights>().ChangeLightsStartGreen());
        StartCoroutine(gameObject.GetComponent<ChangeTrafficLights>().ChangeLightsStartRed());
    }
}
