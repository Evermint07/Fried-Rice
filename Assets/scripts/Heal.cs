using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Vector2 pose;
    public Rigidbody2D rd;
    private GameObject player;
    private bool healed = false;
    // Start is called before the first frame update
    void Start()
    {
        pose = transform.position;
        rd = GetComponent<Rigidbody2D>();
        rd.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < pose.y-0.4)
        {
            rd.velocity= Vector2.zero;
            rd.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !healed)
        {
            player.GetComponent<Player>().playerHealth += 1;
            healed = true;
            Destroy(gameObject);
        }
    }   
}
