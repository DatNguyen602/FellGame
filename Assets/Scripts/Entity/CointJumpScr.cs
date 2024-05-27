using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointJumpScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                Player playerScr = c.gameObject.GetComponent<Player>();
                if(playerScr!=null) playerScr.doubleJumpCount = 1;
                Destroy(gameObject);
            }
        }
    }
}
