using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;
using System;

public class Poison : MonoBehaviour
{
    //Rigidbody2D rigid;
    private GameObject playerObj;
    private Player player;
    public float speed = 5f;
    private Vector3 direction;
    public bool way=false;
    //private Rigidbody2D rigidPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameManager.instance.player;
        player = playerObj.GetComponent<Player>(); // Player 객체 초기화
        direction = (playerObj.transform.position+new Vector3(0f,1f,0f) - transform.position).normalized;
        //rigidPlayer = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x>0){
            way=true;
        }
        else{
            way=false;
        }
        transform.position += direction * speed * Time.deltaTime;
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2f); // 0.5초 대기
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            //player.playerDamaged(transform.position);
        }
    }
}
