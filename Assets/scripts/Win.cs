using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image 클래스를 사용하기 위해 추가

public class Win : MonoBehaviour
{
    public Player player;
    public GameObject win;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    
    void Start()
    {
        //gameObject.SetActive(false);
        //spriteRenderer = GetComponent<SpriteRenderer>();
        win.SetActive(false);
    }

    void Update()
    {
        Debug.Log(player.playerHealth);
        //
        if (player.win)
        {
            Debug.Log(player.win);
            //spriteRenderer.color = new Color(1, 1, 1, 1);
            win.SetActive(true);
            
        }
        else{
            win.SetActive(false);
            //spriteRenderer.color = new Color(1, 1, 1, 0);

        }
    }
}