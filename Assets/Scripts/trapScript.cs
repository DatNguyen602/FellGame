using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapScript : MonoBehaviour
{
    void Start() { }

    void Update() { }

    void OnCollisionEnter2D(Collision2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().addForce(7.5f);
            }
        }
    }
}
