using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    private float timeStart;
    private const float dame = -1.5f;
    
    void Start()
    {
        timeStart = Time.time;
    }

    void Update()
    {
        if(Mathf.Abs(Time.time - timeStart) >= 2.0f){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().bomHit(dame);
                Destroy(gameObject);
            }
        }
    }
}
