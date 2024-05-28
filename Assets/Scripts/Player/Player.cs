using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator at;

    private AudioSource jumpSound;

    private float _speed = 3.0f;
    private float _health, _maxHealth = 10.0f;
    private float point;
    public float addPoint{
        get{
            return point;
        }
        set{
            if(value >= 0){
                point += value;
                EventButton.instance.pointText.text = point.ToString("F1");
            }
        }
    }
    private float timeInt, tMaxInt = 2.0f;
    private float _jumpPower = 4.5f;
    private bool isJump, isDoubleJump;
    private int _doubleJumpCount;
    public int doubleJumpCount{
        get{
            return _doubleJumpCount;
        }
        set{
            this._doubleJumpCount += value;
            EventButton.instance.countJump.text = string.Format("{0:00}", _doubleJumpCount);
        }
    }
    private Vector3 dirLook;
    private Vector3 dirMove;
    public bool isMobile{get; set;}
    private bool isSlip;
    public void setDirMove(float x){
        this.dirMove = new Vector3(x, 0, 0);
    }
    public float getHealth => this._health;
    public bool isDie => this._health <= 0;
    public bool isFullHealth => this._health == this._maxHealth;
    private float timeSlip, tSlipMax = 0.5f;

    void Awake(){
        _doubleJumpCount = 1;
        point = 0;
        timeSlip = 0;
        timeInt = 0;
        isMobile = false;
        isSlip = false;
        isJump = false;
        isDoubleJump = false;
    }

    void Start() {
        rd = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();
        jumpSound.Stop();
        dirLook = Vector3.right;
        _health = _maxHealth;
        EventButton.instance.healthBar.fillAmount = _health / _maxHealth;
        EventButton.instance.countJump.text = string.Format("{0:00}", _doubleJumpCount);
    }

    void Update() {
        float Horizontal = keyHorizontal();
        if(timeInt > 0) timeInt -= Time.deltaTime;
        if(!isMobile) dirMove = new Vector3(Horizontal, 0, 0);
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
        }
        if(Input.GetKeyDown("f")){
            this.Slip();
        }

        if(rd.velocity.y > 0){
            at.SetInteger("state", 1);
            if(isDoubleJump) at.SetInteger("state",4);
        }
        else if(rd.velocity.y < 0){
            at.SetInteger("state", -1);
        }

        if(timeSlip > 0) at.SetInteger("state", 3);

        at.SetBool("isJump", isJump);
        at.SetBool("isSlip", isSlip);
     }

    void FixedUpdate() {
        if(timeSlip > 0){
            timeSlip -= Time.fixedDeltaTime;
            transform.position += dirLook * _speed * 2.0f * Time.fixedDeltaTime;
        }
        else{
            timeSlip = 0;
            isSlip = false;
            at.SetBool("isJump", false);
            rd.gravityScale = 1;
            transform.position += dirMove * _speed * Time.fixedDeltaTime;
        }
    }

    private int keyHorizontal(){
        if(Input.GetKey("d")) return 1;
        else if(Input.GetKey("a")) return -1;
        else return 0;
    }

    public void Jump(){
        if(!isJump || (!isDoubleJump && _doubleJumpCount > 0)){
            rd.velocity = new Vector3(0,0,0);
            rd.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
            jumpSound.Play();
            if(isJump) {
                if(!isDoubleJump) {
                    _doubleJumpCount = Mathf.Clamp(_doubleJumpCount - 1, 0, 255);
                    EventButton.instance.countJump.text = string.Format("{0:00}", _doubleJumpCount);
                    // Debug.Log("count jump: " + _doubleJumpCount);
                }
                isDoubleJump = true;
                return;
            }
            isJump = true;
        }
    }

    public void Slip(){
        if(timeSlip == 0){
            rd.velocity = new Vector3(0,0,0);
            rd.gravityScale = 0;
            timeSlip = tSlipMax;
            isSlip = true;
        }
    }

    public void addForce(float power){
        if(power > 0){
            rd.AddForce(Vector3.up * power, ForceMode2D.Impulse);
        }
    }

    public void hit(float dame){
        if(dame == 0) return;
        if(timeInt > 0) return;
        timeInt = tMaxInt;
        _health = Mathf.Clamp(_health + dame, 0, _maxHealth);
        EventButton.instance.healthBar.fillAmount = _health / _maxHealth;
        at.SetTrigger("hit");
    }

    public void bomHit(float dame){
        if(dame == 0) return;
        _health = Mathf.Clamp(_health + dame, 0, _maxHealth);
        EventButton.instance.healthBar.fillAmount = _health / _maxHealth;
        at.SetTrigger("hit");
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other != null){
            if(other.gameObject.tag == "Ground"){
                isJump = false;
                isDoubleJump = false;
            }
        }
    }
}
