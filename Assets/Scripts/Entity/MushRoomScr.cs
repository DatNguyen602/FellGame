using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomScr : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private Animator at;
    private const float deepRaycast = 2.0f, startPoint = 0.4f;
    private const float dame = -1.0f;
    // private float speed;
    private bool isHit;

    private Vector3 firstPosition;
    private Vector3 dirLook, dirMove;
    
    void Start()
    {
        at = GetComponent<Animator>();
        dirLook = Vector3.left;
        dirMove = new Vector3(0, 0, 0);
        firstPosition = transform.position;
        // speed = 0;
        isHit = false;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dirLook * startPoint, dirLook, deepRaycast, 1 << 3);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + dirLook * -1f * startPoint, dirLook * -1f, deepRaycast, 1 << 3);
        // Debug.DrawRay(transform.position + dirLook * startPoint, dirLook * deepRaycast, Color.red);
        // Debug.DrawRay(transform.position + dirLook * -1f * startPoint, dirLook * -1f * deepRaycast, Color.red);
        if (hit.collider != null) {
            if(hit.collider.gameObject.tag == "Player" && !isHit){
                float x = transform.position.x - hit.collider.gameObject.transform.position.x;
                if(Mathf.Abs(x) < 1.0f){
                    at.SetBool("hit", true);
                    this.isHit = true;
                    Invoke("Explosion", 0.3f);
                }
            }
        }

        if (hit2.collider != null) {
            
            if(hit2.collider.gameObject.tag == "Player" && !isHit){
                float x = transform.position.x - hit2.collider.gameObject.transform.position.x;
                if(Mathf.Abs(x) < 1.0f){
                    at.SetBool("hit", true);
                    this.isHit = true;
                    Invoke("Explosion", 0.3f);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D c){
        if (c != null) {
            if(c.gameObject.tag == "Player" && !isHit){
                float x = transform.position.x - c.gameObject.transform.position.x;
                if(Mathf.Abs(x) < 1.0f){
                    at.SetBool("hit", true);
                    this.isHit = true;
                    Invoke("Explosion", 0.3f);
                }
            }
        }
    }

    void Explosion(){
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
