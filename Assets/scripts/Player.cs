using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //private bool hitting=false;
    private bool goal=false;
    public uint playerHealth;
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
    [SerializeField]
    private GameObject gameOver;
    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        hitboxCollider = hitbox.GetComponent<BoxCollider2D>();
        playerHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.x != 0 && !anim.GetBool("isHit"))
            rigid.velocity = Vector2.zero;
        //Debug.Log(transform.position);
        if (anim.GetBool("death")) {
            gameOver.SetActive(true);
            return;
        }
        float horizonalInput = Input.GetAxisRaw("Horizontal");
        //horizon=horizonalInput;
        Vector3 moveTo = new Vector3(horizonalInput, 0f, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
        //flip
        //if(Input.GetButtonDown("Horizontal")){
        if(horizonalInput ==-1f){
            spriteRenderer.flipX =true;
        }
        if(horizonalInput ==1f){
            spriteRenderer.flipX =false;
        }
        if(spriteRenderer.flipX){
            hitbox.transform.position=transform.position+ new Vector3(-0.5f, 0.7f, 0);
        }
        else{
            hitbox.transform.position=transform.position+ new Vector3(0.5f, 0.7f, 0);
        }
        //}

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
        if(Input.GetButtonDown("Fire1") && !anim.GetBool("isAttack")){
            anim.SetBool("isAttack",true);
            StartCoroutine(ResetAttack());
        }
        if(playerHealth<=0 || transform.position.y<-3.5f){
            playerHealth = 0;
            GameManager.instance.money = 0;
            anim.SetBool("death",true);
        }
        
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        anim.SetBool("isAttack", false); // isAttack을 false로 설정
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (anim.GetBool("death")){
            return;
        }
        if (other.gameObject.tag=="entity" && !anim.GetBool("isHit")){
            Animator targetAnimator = other.gameObject.GetComponent<Animator>();
            if(targetAnimator.GetBool("isAttacking")){
                anim.SetBool("isHit",true);
                playerHealth -=1;
                StartCoroutine(UnHit());
                rigid.drag = 3f;
                if (spriteRenderer.flipX)
                    rigid.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse); // 힘 설정
                else
                    rigid.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        Debug.Log("trigger");
        if (anim.GetBool("death")){
            return;
        }
        
        if (other.gameObject.tag=="EnemyAttack" && !anim.GetBool("isHit")){
            //Goblin_AttackCollider collider;
            Goblin_AttackCollider collider=other.GetComponent<Goblin_AttackCollider>();
            if(collider.attacking){
                anim.SetBool("isHit",true);
                playerHealth -=1;
                StartCoroutine(UnHit());
                rigid.drag = 3f;
                if (transform.position.x>collider.transform.position.x)
                    rigid.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse); // 힘 설정
                else
                    rigid.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
            }
        }
        if (other.gameObject.tag=="StrongAttack" && !anim.GetBool("isHit")){
            //Goblin_AttackCollider collider;
            SkeletonAttackCollider colliding=other.GetComponent<SkeletonAttackCollider>();
            if(colliding.attacking){
                anim.SetBool("isHit",true);
                playerHealth -=1;
                StartCoroutine(UnHit());
                rigid.drag = 3f;
                if (transform.position.x>colliding.transform.position.x)
                    rigid.AddForce(new Vector2(15f, 18f), ForceMode2D.Impulse); // 힘 설정
                else
                    rigid.AddForce(new Vector2(-15f, 18f), ForceMode2D.Impulse);
            }

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // 충돌한 오브젝트의 태그 확인
        if (collider.tag == "Goal" && !goal) {
            playerHealth = 5;
            GameManager.instance.AddMoney(2000);
            goal = true;
            SceneManager.LoadScene("2Round");
            goal=false;
        }
        if (collider.tag == "Goal2" && !goal) {
            playerHealth = 5;
            GameManager.instance.AddMoney(3000);
            goal = true;
            SceneManager.LoadScene("Final Round");
        }
        if(collider.gameObject.tag=="Fire"&& !anim.GetBool("isHit")){
            Poison poison = collider.gameObject.GetComponent<Poison>();
            anim.SetBool("isHit",true);
            //playerHealth -=1;
            StartCoroutine(UnHit());
            rigid.drag = 3f;
            if (poison.way)
                rigid.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse); // 힘 설정
            else
                rigid.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
        }
    }
    IEnumerator UnHit()
    {
        //hitting=true;
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        anim.SetBool("isHit", false); 
        //hitting=false;

        
        rigid.drag=0;
        rigid.velocity = Vector2.zero;
    }
}
