using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHit : MonoBehaviour
{
    public GameObject frog;
    public GameObject player;
    private Animator frogAnimator;
    private Animator playerAnimator;
    private Mushroom frogScript;
    Rigidbody2D rigid;
    Rigidbody2D frogRb;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        frogAnimator = frog.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        frogScript = frog.GetComponent<Mushroom>();
        frogRb = frog.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Attack" && playerAnimator.GetBool("isAttack") && frogAnimator.GetBool("isHit") != true){
            frogAnimator.SetBool("isHit",true);
            //frogScript.ApplyForce();
            Vector2 force;
            if (player.transform.position.x>frog.transform.position.x)
                force = new Vector2(-13f, 7f); // 힘 설정
            else
                force = new Vector2(13f, 7f); // 힘 설정
            frogRb.drag=3f;
            frogScript.ApplyForce(force);
            StartCoroutine(ResetHit());
        }
        
    }
    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        frogAnimator.SetBool("isHit", false); // isAttack을 false로 설정
        frogRb.drag=0.1f;
        frogRb.velocity = Vector2.zero;
    }
}
