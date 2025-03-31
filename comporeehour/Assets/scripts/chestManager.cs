using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chestManager : MonoBehaviour
{
    private static chestManager instance;
    public static chestManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    Animator ani;

    [Serializable]
    public struct chest
    {
        public bool[] isOpen;
    }

    public chest[] isOpen;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ani = GetComponentInChildren<Animator>();
    }

    public IEnumerator stageChange( string sceneName, Vector2 position, float T)
    {
        ani.SetTrigger("End");
        yield return new WaitForSeconds(T);
        ani.SetTrigger("Start");
        SceneManager.LoadScene(sceneName);
        PlayerHP.Instance.transform.position = position;
    }
}
