using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class matchPuzzle : MonoBehaviour
{
    public Sprite[] sprites;
    public Image[] images;
    public Image image;

    public int[] H;
    public int V;

    private void Awake()
    {
        H = new int[images.Length];
        for(int i = 0; i < images.Length; i++)
        {
            H[i] = Random.Range(0, 100);
            H[i] %= images.Length;
            images[i].sprite = sprites[H[i]];
        }
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.anyKeyDown && Input.GetAxisRaw("Vertical") != 0)
        {
            V -= (int)Input.GetAxisRaw("Vertical");
            if(V < 0)
            {
                V = 0;
            }
            else if( V > 3)
            {
                V = 3;
            }
            image.rectTransform.position = images[V].rectTransform.position;
        }
        if(Input.anyKeyDown && Input.GetAxisRaw("Horizontal") != 0)
        {
            H[V] += H[V] == 0 && (int)Input.GetAxisRaw("Horizontal") < 0 ? 3 : (int)Input.GetAxisRaw("Horizontal");
            H[V] %= images.Length;
            images[V].sprite = sprites[H[V]];
        }
        End();
    }
    void End()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (images[i].sprite != sprites[i])
            {
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            puzzleManager.isClear[1] = true;
            puzzleManager.destory(gameObject, 1);
        }
    }
}
