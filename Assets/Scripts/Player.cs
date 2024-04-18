using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator at;

    private float _speed = 3.0f;
    private float _health, _maxHealth;
    private float _jumpPower = 4.5f;
    private bool isJump;
    private Vector3 dirLook;
    private Vector3 dirMove;
    public bool isMobile{get; set;}
    public void setDirMove(float x){
        this.dirMove = new Vector3(x, 0, 0);
    }

    private float timeSlip, tSlipMax = 0.5f;

    void Start() {
        isJump = false;
        rd = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        timeSlip = 0;
        dirLook = Vector3.right;
        isMobile = false;
    }

    void Update() {
        if(!isMobile) dirMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if(transform.localScale.x * dirMove.x < 0) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if(dirMove.x != 0){
            dirLook = dirMove;
            dirLook.Normalize();
            at.SetInteger("state", 1);
        }
        else{
            at.SetInteger("state", 0);
        }
        if(Input.GetKeyDown("space")){
            this.Jump();
            at.SetBool("isJump", true);
        }
        if(Input.GetKeyDown("f")){
            this.Slip();
        }

        if(rd.velocity.y > 0){
            at.SetInteger("state", 1);
        }
        else if(rd.velocity.y < 0){
            at.SetInteger("state", -1);
        }
     }

    void FixedUpdate() {
        if(timeSlip > 0){
            timeSlip -= Time.fixedDeltaTime;
            transform.position += dirLook * _speed * 2.0f * Time.fixedDeltaTime;
        }
        else{
            timeSlip = 0;
            at.SetBool("isJump", false);
            rd.gravityScale = 1;
            transform.position += dirMove * _speed * Time.fixedDeltaTime;
        }
    }

    public void Jump(){
        if(!isJump){
            rd.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    public void Slip(){
        if(timeSlip == 0){
            rd.velocity = new Vector3(0,0,0);
            rd.gravityScale = 0;
            timeSlip = tSlipMax;
            at.SetBool("isJump", true);
            at.SetInteger("state", 3);
        }
    }

    public void addForce(float power){
        if(power > 0){
            rd.AddForce(Vector3.up * power, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other != null){
            if(other.gameObject.tag == "Ground"){
                isJump = false;
                at.SetBool("isJump", false);
            }
        }
    }
}
