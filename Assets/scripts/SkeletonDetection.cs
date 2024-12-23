using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDetection : MonoBehaviour
{
    
    public Skeleton frog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(frog.detection);

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            frog.detection = true;
            //Debug.Log("detect!");
        }
    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            frog.detection = false;
            //Debug.Log("detect!");
        }
    
    }

}
