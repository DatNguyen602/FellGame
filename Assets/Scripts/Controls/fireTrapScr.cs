using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTrapScr : MonoBehaviour
{
    private Animator at;
    
    void Start()
    {
        at = GetComponent<Animator>();
    }

    void Update() {
        
    }

    public void setFire(bool fire){
        at.SetBool("fire",fire);
    }
}
