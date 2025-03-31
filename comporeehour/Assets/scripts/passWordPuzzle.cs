using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class passWordPuzzle : MonoBehaviour
{
    public int[] correct = { 7, 5, 3 };
    public int[] answer = new int[3];

    public Text numText;
    public int n;

    public void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void number(int i)
    {
        if(n < 3)
        {
            answer[n] = i;
            n++;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            backSpace();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter();
        }
        numText.text = answer[0].ToString() + answer[1].ToString() + answer[2].ToString();
    }
    public void enter()
    {
        for (int i = 0; i < correct.Length; i++)
        {
            if (answer[i] != correct[i])
            {
                for (int j = 0; j < answer.Length; j++)
                {
                    answer[j] = 0;
                }
                n = 0;
                return;
            }
        }
        puzzleManager.isClear[3] = true;
        puzzleManager.destory(gameObject, 3);
    }
    public void backSpace()
    {
        if(n > 0)
        {
            n--;
            answer[n] = 0;
        }
    }
}
