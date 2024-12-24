using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goblin_AttackCollider : MonoBehaviour
{
    public Goblin goblin;
    public Animator goblinAnimator;
    public Animator animator;
    public bool attacking=false;
    //public string collidertag;
    // Start is called before the first frame update
    void Start()
    {
        goblinAnimator = goblin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attacking=goblinAnimator.GetBool("isAttacking");
    }
    private void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Player"){
            //collidertag = other.tag;
            //Debug.Log(other);
            goblin.attackReady = true;
        }
        // else{
        //     collidertag = null;
        // }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            goblin.nextAttackTime += 0.1f*Time.deltaTime;
        }
    }
}
