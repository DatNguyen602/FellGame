using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dameZone : MonoBehaviour
{
    private float dame = -1.5f;

    void Start() {
        
    }

    void Update() { 
    }

    void OnTriggerStay2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().hit(dame);
            }
        }
    }

    void OnCollisionStay2D(Collision2D c) {
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().hit(dame);
            }
        }
    }
}
