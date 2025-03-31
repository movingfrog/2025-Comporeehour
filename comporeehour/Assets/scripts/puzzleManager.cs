using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleManager
{
    public static bool[] isClear = new bool[4];

    public static void destory(GameObject GO, int i)
    {
        if (isClear[i])
        {
            Time.timeScale = 1;
            GO.transform.parent.gameObject.SetActive(false);
            MonoBehaviour.Destroy(GO);
        }
    }
}
