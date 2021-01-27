using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetRefresh : MonoBehaviour
{
    public void RefreshAssets()
    {
        AssetDatabase.Refresh();
    }
}
