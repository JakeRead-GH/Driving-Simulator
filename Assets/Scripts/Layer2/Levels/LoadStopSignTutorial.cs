using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStopSignTutorial : MonoBehaviour
{
    public GameObject straightRoad;
    public GameObject tIntRoad;
    private GameObject roadsParent;
    private Vector3 roadLength = new Vector3(0, 0, 10);
    private Vector3 spawnPos;

    public void Load()
    {
        roadsParent = GameObject.Find("Roads");

        for (int a = 0; a < 10; a++)
        {
            spawnPos = roadLength * a;
            Instantiate(straightRoad, spawnPos, straightRoad.transform.rotation, roadsParent.transform);
        }

        spawnPos += new Vector3(-15, 2, 15);
        Quaternion tIntRotation = straightRoad.transform.rotation * Quaternion.Euler(0, 90, 0);

        Instantiate(tIntRoad, spawnPos, tIntRotation, roadsParent.transform);
    }
}
