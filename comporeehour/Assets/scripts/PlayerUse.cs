using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerUse : MonoBehaviour
{
    private static bool isLight;
    public static bool IsLight
    {
        get
        {
            return isLight;
        }
    }

    public GameObject panel;

    private void Awake()
    {
        
    }

    private void Update()
    {
        OnLight();
        invenUP();
    }

    void OnLight()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(!panel.activeSelf);
            isLight = !panel.activeSelf;
        }
    }

    void invenUP()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.Instance.bag.SetActive(!Inventory.Instance.bag.activeSelf);
            Inventory.Instance.FreshSlots();
        }
    }

    void LoadSceneMode(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
        {
            panel.SetActive(false);
            isLight = !panel.activeSelf;
        }
        else
        {
            panel.SetActive(true);
            isLight = !panel.activeSelf;
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadSceneMode;
    }
}
