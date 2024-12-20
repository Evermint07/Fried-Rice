using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Mushroom : MonoBehaviour
{
    public bool attackReady = false;
    public float attackCooldown = 1.8f;
    private float nextAttackTime = 0f; // 다음 공격 가능 시간
    public Animator frogAnimator; // Animator 컴포넌트    
    
    [SerializeField]
    private GameManager playerp;
    private GameObject player;
    
    private Animator playerAnimator;
    private uint health = 6;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    public bool current_state;
    public bool current2_state;
    public bool detection;
    private int flip = -1;
    Vector3 currentPosition;
    public Vector2 forceRight = new Vector2(3f, 4f); // 오른쪽 방향 힘
    public Vector2 forceLeft = new Vector2(-3f, 4f); // 왼쪽 방향 힘
    //Vector3 moveTo = new Vector3(x, 0f, 0f); // 이게 매 프레임 초기화돼서 그런거임 start나 아예 밖으로 빼버리셈
    Vector3 move = new Vector3(1f, 0f, 0f);
    Vector3 move2 = new Vector3(1f, 0f, 0f);
    public GameObject itemPrefab;
    public GameObject poison;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        playerAnimator = player.GetComponent<Animator>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(current_state);
        anim.SetBool("isRunning", true);
        //anim.SetBool("isRuning",true);
        spriteRenderer.flipX = flip == -1;
        //MySpriteComponent otherComponent = FindObjectOfType<MySpriteComponent>();
        //float playerx = otherComponent.transform.position.x;

        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 8, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 4, LayerMask.GetMask("Ground"));
        if (!anim.GetBool("isHit"))
        {
            if (!detection)
            {
                if (rayHit.collider == null)
                {
                    current2_state = false;
                    if (current_state)
                    {
                        flip = flip * (-1);
                        move.x = move.x * (-1f);
                        currentPosition = transform.position;
                        current_state = false;
                    }
                    transform.position += move * moveSpeed * Time.deltaTime;
                    //Debug.Log("turn!");
                }
                else
                {
                    current2_state = true;
                    if (Mathf.Abs(currentPosition.x - transform.position.x) >= 4)
                    {
                        move.x = -move.x;
                    }
                    if (move.x > 0)
                    {
                        flip = 1;
                    }
                    else
                    {
                        flip = -1;
                    }
                }
                transform.position += move * moveSpeed / 3 * Time.deltaTime;
            }
            else
            {
                //Debug.Log("detection");
                currentPosition = transform.position;
                //float playerX = player.transform.position.x;
                if (player.transform.position.x >= transform.position.x)
                {
                    transform.position += move2 * moveSpeed * Time.deltaTime;
                    flip = 1;
                }
                else
                {
                    transform.position -= move2 * moveSpeed * Time.deltaTime;
                    flip = -1;
                }
            }

            if (rayHit.collider == null && detection && !anim.GetBool("isJumping"))
            {
                current_state = false;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                //Debug.Log("jump!");
                anim.SetBool("isJumping", true);
            }
        }
        current_state = current2_state;
        
        if (rigid.velocity.x != 0 && !anim.GetBool("isHit"))
            rigid.velocity = Vector2.zero;
        if (transform.position.y <= -2.8 || health == 0)
        {
            if (!anim.GetBool("isDie")){
                anim.SetBool("isDie", true);
                for (int i = 0; i < 25; i++)
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
                GameManager.instance.AddMoney(1500);
                StartCoroutine(Die());
            }

        }
        if (attackReady){
            if ( Time.time*Time.deltaTime >= nextAttackTime*Time.deltaTime)
            {
                //Debug.Log("attack!");
                nextAttackTime = Time.time + attackCooldown; // 다음 공격 시간 설정
                anim.SetBool("isAttacking", true);
                attackReady = false;
                StartCoroutine(Unattack());
            }
        }
    }
    public void ApplyForce(Vector2 force)
    {
        health -= 1;
        rigid.AddForce(force, ForceMode2D.Impulse);
        spriteRenderer.color += new Color(0f, -0.2f, -0.2f, 0f);
    }

    IEnumerator Unattack()
    {
        
        yield return new WaitForSeconds(0.7f); // 0.5초 대기
        Instantiate(poison, transform.position, Quaternion.identity);
        anim.SetBool("isAttacking", false); // isAttack을 false로 설정
    }
    IEnumerator Die(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
