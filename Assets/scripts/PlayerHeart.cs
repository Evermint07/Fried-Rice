using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    public uint health;
    public uint numOfHearts;
    public Player player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        health = player.playerHealth;
        if (health > numOfHearts)
            health= numOfHearts;
        
        for (int i  = 0; i < hearts.Length; i++) {
            if (i-1< health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
            
            if (i-1 < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
