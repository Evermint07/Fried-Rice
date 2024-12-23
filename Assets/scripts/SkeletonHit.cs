using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHit : MonoBehaviour
{
    public GameObject frog;
    public GameObject player;
    private Animator frogAnimator;
    private Animator playerAnimator;
    private Skeleton frogScript;
    private SpriteRenderer skeletonRenderer;
    Rigidbody2D rigid;
    Rigidbody2D frogRb;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        frogAnimator = frog.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        frogScript = frog.GetComponent<Skeleton>();
        frogRb = frog.GetComponent<Rigidbody2D>();
        skeletonRenderer = frog.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(frogAnimator.GetBool("isShield"));
    }
    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Attack" && playerAnimator.GetBool("isAttack") && frogAnimator.GetBool("isHit") != true){
            //if(frogScript.health>1){
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
            //}
            if(frogScript.health<=2 && !frogAnimator.GetBool("isShield")){
                StartCoroutine(Shield());
                //Debug.Log("shield");
            }
        }
        
    }
    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        frogAnimator.SetBool("isHit", false); // isAttack을 false로 설정
        frogRb.drag=0.1f;
        frogRb.velocity = Vector2.zero;
    }
    IEnumerator Shield(){
        frogAnimator.SetBool("isShield",true);
        frogScript.moveSpeed=0;
        yield return new WaitForSeconds(5f);
        frogAnimator.SetBool("isShield",false);
        frogScript.moveSpeed=3;
        frogScript.health=8;
        skeletonRenderer.color = new Color (1f,1f,1f,1f);
    }
}
