using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;
using System;

public class FriedRice : MonoBehaviour
{
    Rigidbody2D rigid;
    private GameObject player;
    // Start is called before the first frame update
    private float offset = 3.3f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        float randX = Range(-18f, 18f);
        float randY = Range(-18f, 18f);
        rigid.AddForce(new Vector2(randX * 25, randY * 25));
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Force());
        StartCoroutine(Death());
    }
    IEnumerator Force()
    {
        yield return new WaitForSeconds(0.15f); // 0.5초 대기
        offset = Math.Min(offset / 1.06f, 0.1f);
        rigid.AddForce((player.transform.position-transform.position)/offset);
        transform.position += (player.transform.position-transform.position)/(offset*1000);
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
