using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject hitbox;
    Rigidbody2D rigid;
    Animator anim;
    private BoxCollider2D hitboxCollider;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    public float horizon;
    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        hitboxCollider = hitbox.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizonalInput = Input.GetAxisRaw("Horizontal");
        horizon=horizonalInput;
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizonalInput, 0f, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
        //flip
        if(Input.GetButtonDown("Horizontal")){
            spriteRenderer.flipX = horizonalInput == -1;
            if(spriteRenderer.flipX){
                hitbox.transform.position=transform.position+ new Vector3(-0.5f, 0.7f, 0);
            }
            else{
                hitbox.transform.position=transform.position+ new Vector3(0.5f, 0.7f, 0);
            }
        }

        //run
        if(horizonalInput == 0) {
            anim.SetBool("isRun",false);
        }
        else{
            anim.SetBool("isRun",true);
        }
        if (Input.GetButtonDown("Vertical") && !anim.GetBool("isJump")){
            rigid.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            anim.SetBool("isJump",true);

        //landing
        //Debug.DrawRay(rigid.position,Vector3.down, new Color(0,1,0));
        //RaycastHit2D rayHit = Physics2D.Raycast(rigid.position,Vector3.down,1,LayerMask.GetMask("Ground"));
        //if (rayHit.collider != null){
            //if (rayHit.distance <0.7f){
                //anim.SetBool("isJump",false);
            //}
        //}
        }
        if(Input.GetButtonDown("Fire1") && anim.GetBool("isAttack") != true){
            anim.SetBool("isAttack",true);
            StartCoroutine(ResetAttack());
        }

        
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        anim.SetBool("isAttack", false); // isAttack을 false로 설정
    }
    //private void OnCollisionEnter2D(Collision2D other) {
    //    if (other.gameObject.tag == "Ground"){
    //        anim.SetBool("isJump",false);
    //    }
    //}
}
