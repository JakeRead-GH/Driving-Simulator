using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpdater : MonoBehaviour
{
    /* A controller for level updates that take place outside of normal game
       update times, such as changing traffic lights. */
    public void StartUpdates()
    {
        StartCoroutine(gameObject.GetComponent<ChangeTrafficLights>().ChangeLights());
    }
}
