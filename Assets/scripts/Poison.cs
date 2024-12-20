using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;
using System;

public class Poison : MonoBehaviour
{
    //Rigidbody2D rigid;
    private GameObject player;
    public float speed = 5f;
    private Vector3 direction;
    public bool way=false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        direction = (player.transform.position+new Vector3(0f,1f,0f) - transform.position).normalized;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        // frog 또는 frogAnimator가 null이면 실행 중단
        if (other.gameObject.tag == "Player")
            Destroy(gameObject);

    }
}
