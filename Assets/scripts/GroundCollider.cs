using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void OnTriggerStay2D(Collider2D other) {
        Animator playerAnimator = player.GetComponent<Animator>();
        if ((other.gameObject.tag == "Ground"||other.gameObject.tag == "entity") && playerAnimator != null){
            playerAnimator.SetBool("isJump",false);
            //Debug.Log("collide!");
        }
    }
}
