using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class memoryPuzzle : MonoBehaviour
{
    public Color[] correctColor = { Color.white, Color.green, Color.red, Color.white };
    public Color[] color = { Color.red, Color.white, Color.white, Color.green };

    public Image[] ansColor;
    public int[] num;

    public Image correctimage;
    public GameObject panel;

    private void Awake()
    {
        num = new int[ansColor.Length];
    }

    private void OnEnable()
    {
        StartCoroutine(ansCorrect());
    }

    IEnumerator ansCorrect()
    {
        for(int i = 0; i < correctColor.Length; i++)
        {
            correctimage.color = correctColor[i];
            Debug.Log("sldkfj");
            yield return new WaitForSeconds(0.5f);
        }
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void click(int i)
    {
        ansColor[i].color = color[num[i] % color.Length];
        num[i]++;
    }
    public void End()
    {
        for(int i = 0; i < correctColor.Length; i++)
        {
            if (ansColor[i].color != correctColor[i])
            {
                Time.timeScale = 1;
                panel.SetActive(false);
                transform.parent.gameObject.SetActive(false);
                return;
            }
        }
        puzzleManager.isClear[2] = true;
        puzzleManager.destory(gameObject, 2);
    }
}
