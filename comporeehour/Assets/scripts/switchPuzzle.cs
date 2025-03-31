using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class switchPuzzle : MonoBehaviour
{
    [Serializable]
    public struct Load
    {
        public GameObject load;
        public bool isOpen;
    }
    public Load[] load;
    public GameObject[] button;
    public Image successbar;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void one()
    {
        for(int i = 1; i < 3; i++)
        {
            Open(i);
        }
        Close(3);

        button[0].SetActive(false);
    }
    public void two()
    {
        Close(1);
        Close(3);
        Open(2);

        button[1].SetActive(false);
    }
    public void three()
    {
        Close(0);
        Close(2);
        Open(4);

        button[2].SetActive(false);
    }
    public void four()
    {
        Open(1);
        Open(3);
        Close(2);

        button[3].SetActive(false);
    }
    public void five()
    {
        Open(0);
        Open(3);

        button[4].SetActive(false);
    }

    public void Open(int i)
    {
        load[i].load.SetActive(false);
        load[i].isOpen = true;
    }
    public void Close(int i)
    {
        load[i].load.SetActive(true);
        load[i].isOpen = false;
    }
    private void Update()
    {
        end();
    }

    void end()
    {
        for(int i = 0; i < button.Length; i++)
        {
            if (button[i].activeSelf)
            {
                return;
            }
        }
        for(int i = 0; i < load.Length; i++)
        {
            if (load[i].isOpen)
            {
                successbar.fillAmount += (i + 2) / 6f;
            }
            else
            {
                break;
            }
        }
        if(successbar.fillAmount >= 0.9f)
        {
            Time.timeScale = 1;
            puzzleManager.isClear[0] = true;
            puzzleManager.destory(gameObject, 0);
        }
        for(int i = 0; i < load.Length; i++)
        {
            Close(i);
            button[i].SetActive(true);
        }
        successbar.fillAmount = 0.17f;
        gameObject.SetActive(false);
    }
}
