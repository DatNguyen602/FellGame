using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dameZone : MonoBehaviour
{
    [SerializeField] private float dame = -1.5f;
    [SerializeField] private float dameBom = 0f;
    private bool isHit;

    void Start() {
        isHit = false;
    }

    void Update() { 
    }

    void OnTriggerStay2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player" && !isHit){
                c.gameObject.GetComponent<Player>().hit(dame);
                c.gameObject.GetComponent<Player>().bomHit(dameBom);
                isHit = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                isHit = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D c) {
        if(c != null){
            if(c.gameObject.tag == "Player"  && !isHit){
                c.gameObject.GetComponent<Player>().hit(dame);
                c.gameObject.GetComponent<Player>().bomHit(dameBom);
                isHit = true;
            }
        }
    }
}
