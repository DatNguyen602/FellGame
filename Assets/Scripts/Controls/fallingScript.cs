using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingScript : MonoBehaviour
{
    private const float deepRaycast = 2.0f, startPoint = 0.2f;
    private const float dame = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * startPoint, Vector3.down, deepRaycast);
        // Debug.DrawRay(transform.position + Vector3.down * startPoint, Vector3.down * deepRaycast, Color.red);
        if (hit.collider != null) {
            
            if(hit.collider.gameObject.tag == "Player"){
                hit.collider.gameObject.GetComponent<Player>().hit(dame);
            }
        }
    }
}
