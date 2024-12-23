using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackCollider : MonoBehaviour
{
    public Skeleton goblin;
    public Animator goblinAnimator;
    public Animator animator;
    public bool attacking=false;
    private bool attack=false;
    // Start is called before the first frame update
    void Start()
    {
        goblinAnimator = goblin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(string.Format("attacking{0}", attacking));
        if(goblinAnimator.GetBool("isAttacking")&& !attack){
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        //Debug.Log(other.tag);
        //
        if (other.tag == "Player"){
            //Debug.Log(other);
            
            goblin.attackReady = true;
        }
    }
    IEnumerator Attack(){
        if(!goblinAnimator.GetBool("isShield")){
            attack = true;
            yield return new WaitForSeconds(0.3f);
            if(!goblinAnimator.GetBool("isHit")){
                attacking = true;
            }
            yield return new WaitForSeconds(0.35f);
            attacking = false;
            attack = false;
        }
    }
}
