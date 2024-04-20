using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomScr : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private Animator at;
    private const float deepRaycast = 5.0f, startPoint = 0.4f;
    private const float dame = -1.0f;

    private Vector3 dirLook;
    // Start is called before the first frame update
    void Start()
    {
        at = GetComponent<Animator>();
        dirLook = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dirLook * startPoint, dirLook, deepRaycast);
        Debug.DrawRay(transform.position + dirLook * startPoint, dirLook * deepRaycast, Color.red);
        if (hit.collider != null) {
            
            if(hit.collider.gameObject.tag == "Player"){
                
            }
        }
    }
}
