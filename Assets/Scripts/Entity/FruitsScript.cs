using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsScript : MonoBehaviour
{
    [SerializeField] private GameObject Collected;

    private float health = 1.0f;
    private float addPoint = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D c){
        if(c != null){
            GameObject player = c.gameObject;
            if(player != null && player.tag == "Player"){
                // Debug.Log("An trai cay");
                player.GetComponent<Player>().hit(health);
                player.GetComponent<Player>().addPoint = addPoint;
                if(Collected) Instantiate(Collected, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
