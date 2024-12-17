using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject playerObject;
    private Player player;
    // Start is called before the first frame update
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Player player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.horizon==-1){
            transform.position=player.transform.position+new Vector3(-1f,0f,0f);
        }
        else{
            transform.position=player.transform.position;
        }
    }
}
