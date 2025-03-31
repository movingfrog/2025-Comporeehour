using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    private static PlayerHP instance;
    public static PlayerHP Instance
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
    public float HP { get; set; }
    public float MaxHP;
    public float Oxygen { get; set; }
    public float MaxOxygen;

    public bool isUnderground;
    public int stageLevel;

    public bool isHide;

    public bool[] OxygenSize = new bool[3];

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
        HP = MaxHP;
        Oxygen = MaxOxygen;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            HP = MaxHP;
            Oxygen = MaxOxygen;
        }
        if (OxygenSize[0])
        {
            MaxOxygen = 200;
            if (OxygenSize[1])
            {
                MaxOxygen += 150;
                if (OxygenSize[2])
                {
                    MaxOxygen += 150;
                }
            }
        }
        Inventory.Instance.HPbar.fillAmount = HP / MaxHP;
        Inventory.Instance.OxygenBar.fillAmount = Oxygen / MaxOxygen;
        if(HP >= MaxHP)
        {
            HP = MaxHP;
        }
        if(Oxygen >= MaxOxygen)
        {
            Oxygen = MaxOxygen;
        }
        if (isUnderground)
        {
            Oxygen -= Time.deltaTime * stageLevel;
        }
        else
        {
            Oxygen += Time.deltaTime * MaxOxygen;
        }
        if(HP == 0 || Oxygen == 0)
        {
            CP.instance.die("Lobby", Vector2.zero, 0.5f);
        }
    }
    public void getHeart()
    {
        HP -= 20;
        StartCoroutine(HPdown(1.5f));
    }
    public IEnumerator HPdown(float T)
    {
        isHide = true;
        yield return new WaitForSeconds(T);
        isHide = false;
    }
    void LoadSceneMode(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Lobby")
        {
            isUnderground = true;
        }
        else
        {
            isUnderground = true;
            switch (scene.name)
            {
                case "map1":
                    stageLevel = 1;
                    break;
            }
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadSceneMode;
    }
}
