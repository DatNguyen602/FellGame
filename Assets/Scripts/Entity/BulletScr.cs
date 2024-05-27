using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    private float timeStart;
    private const float dame = -1.5f;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        timeStart = Time.time;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.x - startPosition.x) >= 5.0f){
            gameObject.SetActive(false);
            transform.position = startPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().bomHit(dame);
                gameObject.SetActive(false);
                transform.position = startPosition;
            }
        }
    }
}
