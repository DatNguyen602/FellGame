using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd;

    private float _speed = 5.0f;
    private float _health, _maxHealth;
    private float _jumpPower = 5.0f;
    private bool isJump;
    private Vector3 dirLook;
    private Vector3 dirMove;

    private float timeSlip, tSlipMax = 0.5f;

    void Start() {
        isJump = false;
        rd = GetComponent<Rigidbody2D>();
        timeSlip = 0;
        dirLook = Vector3.right;
    }

    void Update() {
        dirMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if(dirMove.x != 0){
            dirLook = dirMove;
            dirLook.Normalize();
        }
        if(Input.GetKeyDown("space")){
            this.Jump();
        }
        if(Input.GetKeyDown("f")){
            this.Slip();
        }
     }

    void FixedUpdate() {
        if(timeSlip > 0){
            timeSlip -= Time.fixedDeltaTime;
            transform.position += dirLook * _speed * 1.5f * Time.fixedDeltaTime;
        }
        else{
            timeSlip = 0;
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
            rd.gravityScale = 0;
            timeSlip = tSlipMax;
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
            }
        }
    }
}
