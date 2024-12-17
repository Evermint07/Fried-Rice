using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cammove : MonoBehaviour
{
    public GameObject player;
    public float offestx;
    public float offesty;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
        if(player.transform.localScale.x>0f){
            playerPosition = new Vector3(playerPosition.x+offestx,playerPosition.y+offesty,playerPosition.z);
        }
        else{
            playerPosition = new Vector3(playerPosition.x-offestx,playerPosition.y-offesty,playerPosition.z);
        }
        transform.position = Vector3.Lerp(transform.position,playerPosition, offsetSmoothing*Time.deltaTime);
    }
}
