using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScr : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timeSetBullet = 1.5f;
    [SerializeField] private Vector3 dirLook = new Vector3(1,0,0);
    [SerializeField] private float timeDelayBullet = 0.5f;

    private Animator at;
    private float startPoint = 0.4f, deepRaycast = 5.0f;

    private float timeBL;
    
    void Start()
    {
        at = GetComponent<Animator>();
        timeBL = 0f;
    }

    void Update()
    {
        if(timeBL > 0) timeBL -= Time.deltaTime;
        // Debug.DrawRay(transform.position + dirLook * startPoint, dirLook * deepRaycast, Color.red);
        if(timeBL <= 0){
            RaycastHit2D hit = Physics2D.Raycast(transform.position + dirLook * startPoint, dirLook, deepRaycast, 1 << 3);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "Player"){
                    at.SetBool("attack", true);
                    Invoke("attack", timeDelayBullet);
                    timeBL = timeSetBullet;
                    Debug.Log("Plant attck player!");
                }
            }
        }
    }

    void attack(){
        GameObject bl = Instantiate(bullet, transform.position + dirLook * startPoint, Quaternion.identity);
        bl.GetComponent<Rigidbody2D>().AddForce(dirLook * 5.0f, ForceMode2D.Impulse);
    }
}
