using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_AttackCollider : MonoBehaviour
{
    public Goblin goblin;
    public Animator goblinAnimator;
    public Animator animator;
    public bool attacking=false;
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
            //Debug.Log(other);
            goblin.attackReady = true;
        }
    }
}
