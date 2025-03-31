using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Search.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CP : MonoBehaviour
{
    private static CP cp;
    public static CP instance
    {
        get
        {
            if (cp == null)
            {
                return null;
            }
            return cp;
        }
    }

    [Serializable]
    public struct chest
    {
        public bool[] isOpen;
    }
    public chest[] isOpen;
    public bool[] puzzle;
    public Vector2 position;
    public float HP;
    public float Oxygen;
    public string thisScene;

    private void Awake()
    {
        if (cp == null)
        {
            cp = this;
            DontDestroyOnLoad(cp);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += LoadSceneMode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            die(thisScene, position, 1);
        }
    }

    public void die(string scene, Vector2 position, float t)
    {
        if(PlayerHP.Instance.HP <= 0)
        {
            Inventory.Instance.sellItem(false);
        }
        PlayerHP.Instance.HP = HP;
        PlayerHP.Instance.Oxygen = Oxygen;
        for (int i = 0; i < isOpen.Length; i++)
        {
            for (int j = 0; j < isOpen[i].isOpen.Length; j++)
            {
                chestManager.Instance.isOpen[i].isOpen[j] = isOpen[i].isOpen[j];
            }
        }
        for (int i = 0; i < puzzle.Length; i++)
        {
            puzzleManager.isClear[i] = puzzle[i];
        }
        StartCoroutine(chestManager.Instance.stageChange(scene, position, t));
    }

    void LoadSceneMode(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Lobby")
        {
            for(int i = 0; i < isOpen.Length; i++)
            {
                for(int j = 0; j < isOpen[i].isOpen.Length; j++)
                {
                    isOpen[i].isOpen[j] = chestManager.Instance.isOpen[i].isOpen[j];
                }
            }
            for(int i = 0; i < puzzle.Length; i++)
            {
                puzzle[i] = puzzleManager.isClear[i];
            }
        }
        position = PlayerHP.Instance.transform.position;
        thisScene = scene.name;
        HP = PlayerHP.Instance.HP;
        Oxygen = PlayerHP.Instance.Oxygen;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadSceneMode;
    }
}
