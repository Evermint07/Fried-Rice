using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool reset=false;
    private Vector2 boxSize=new Vector2(0.83f,1.23f);
    public Vector3 boxCenter=new Vector3(0f,0.68f,0f);
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
    public RaycastHit2D boxHit ;
    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        //hitboxCollider = hitbox.GetComponent<BoxCollider2D>();
        playerHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        boxHit = Physics2D.BoxCast(transform.position+boxCenter, boxSize, 0, Vector2.zero, 0, LayerMask.GetMask("Hit"));
        //if (rigid.velocity.x != 0 && !anim.GetBool("isHit"))
            //rigid.velocity = Vector2.zero;
        //Debug.Log(transform.position);
        if (anim.GetBool("death")) {
            gameOver.SetActive(true);
            return;
        }
        float horizonalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.R)&& !reset && !anim.GetBool("ifHit"))//초기화
        {
            rigid.velocity = Vector2.zero;
            reset=true;
        }
        Vector3 moveTo = new Vector3(horizonalInput, 0f, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
        //flip
        //if(Input.GetButtonDown("Horizontal")){
        if(horizonalInput ==-1f){
            spriteRenderer.flipX =true;
            //rigid.AddForce(Vector2.right , ForceMode2D.Impulse);//디버깅
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
        //Debug.Log("aaaf");
        //Debug.Log(boxHit.collider);
        //if (other.gameObject.tag=="entity" && !anim.GetBool("isHit")){
        if (boxHit.collider!=null && other.gameObject.tag=="entity"){
            if (boxHit.collider.tag=="entity" && !anim.GetBool("isHit")){
                Animator targetAnimator = other.gameObject.GetComponent<Animator>();
                Frog frogcollider=other.gameObject.GetComponent<Frog>();
                if(targetAnimator.GetBool("isAttacking")){
                    anim.SetBool("isHit",true);
                    playerHealth -=1;
                    StartCoroutine(UnHit());
                    rigid.drag = 3f;
                    if (transform.position.x>frogcollider.transform.position.x)
                        rigid.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse); // 힘 설정
                    else
                        rigid.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
                }
            }
        }
        
    }
    
    private void OnTriggerStay2D(Collider2D other){
        if(boxHit.collider!=null && other.gameObject.tag=="EnemyAttack"){
        if (boxHit.collider.tag=="EnemyAttack" && !anim.GetBool("isHit") ){
            //Goblin_AttackCollider collider;
            
            Goblin_AttackCollider collider=other.GetComponent<Goblin_AttackCollider>();
            if(collider.attacking ){
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
        }
        if (boxHit.collider!=null && other.gameObject.tag=="StrongAttack"){
        if (boxHit.collider.tag=="StrongAttack" && !anim.GetBool("isHit")){
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
        if (boxHit.collider!=null && other.gameObject.tag=="Fire"){
        if (boxHit.collider.tag=="Fire" && !anim.GetBool("isHit")){
            //Goblin_AttackCollider collider;
            Poison Firing=other.GetComponent<Poison>();
            anim.SetBool("isHit",true);
            playerHealth -=1;
            StartCoroutine(UnHit());
            rigid.drag = 3f;
            if (transform.position.x>Firing.transform.position.x)
                rigid.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse); // 힘 설정
            else
                rigid.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
            Destroy(Firing.gameObject);
            

        }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
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

    }
    /*
    public void playerDamaged(Vector3 pos)
    {
        anim.SetBool("isHit",true);
        playerHealth -=1;
        StartCoroutine(UnHit());
        rigid.drag = 3f;
        if (transform.position.x>pos.x)
            rigid.AddForce(new Vector2(5f, 2f), ForceMode2D.Impulse); // 힘 설정
        else
            rigid.AddForce(new Vector2(-5f, 2f), ForceMode2D.Impulse);
    }
    */
    IEnumerator UnHit()
    {
        //hitting=true;
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        anim.SetBool("isHit", false); 
        //hitting=false;

        
        rigid.drag=0;
        rigid.velocity = Vector2.zero;
    }
    void OnDrawGizmos()
    {
        Debug.Log(boxSize);
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(transform.position+boxCenter, boxSize); // 정상 작동
    }
}
