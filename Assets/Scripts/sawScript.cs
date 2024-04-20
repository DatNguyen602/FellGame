using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawScript : MonoBehaviour
{
    private float dame = -5f;
    private float speed = 2.5f, tChange = 2.0f, time;
    private int dirMove = 1;
    [SerializeField] private bool isVertical = true;
    [SerializeField] private GameObject chain;
    // Start is called before the first frame update
    void Start()
    {
        time = tChange;
        if(!isVertical) chain.transform.localRotation = new Quaternion(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 5.0f);
        Debug.DrawRay(transform.position, Vector3.back * 5.0f, Color.red);
        if (hit.collider != null && hit.collider.gameObject.name != "Chain") {
            dirMove *= -1;
        }
        Vector3 dir = isVertical ? Vector3.up : Vector3.right;
        transform.position += dir * speed * Time.deltaTime * dirMove;

        // time -= Time.deltaTime;
        // if(time > 0){
        //     transform.position += Vector3.up * speed * Time.deltaTime * dirMove;
        // }
        // else{
        //     time = tChange;
        //     dirMove *= -1;
        // }
    }

    void OnTriggerStay2D(Collider2D c){
        if(c != null){
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<Player>().hit(dame);
            }
        }
    }
}
