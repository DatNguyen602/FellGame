using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator at;

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private ParticleSystem particleRun;

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
                pointText.text = point.ToString("F1");
            }
        }
    }
    private float timeInt, tMaxInt = 2.0f;
    private float _jumpPower = 4.5f;
    private bool isJump, isDoubleJump;
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

    void Start() {
        point = 0;
        isJump = false;
        isDoubleJump = false;
        rd = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        if(particleRun != null) particleRun.Stop();
        timeSlip = 0;
        dirLook = Vector3.right;
        isMobile = false;
        isSlip = false;
        _health = _maxHealth;
        timeInt = 0;
        healthBar.fillAmount = _health / _maxHealth;
    }

    void Update() {
        if(timeInt > 0) timeInt -= Time.deltaTime;
        if(Input.GetAxis("Horizontal") != 0 && dirMove.x == 0 && particleRun != null) particleRun.Play();
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
            if(particleRun != null) particleRun.Stop();
        }

        // RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 0.41f, Vector3.down, 0.01f);
        // Debug.DrawRay(transform.position + Vector3.down * 0.41f, Vector3.down * 0.01f, Color.red);
        // if (hit.collider != null && hit.collider.gameObject.tag == "Ground") {
        //     Debug.Log(hit.collider.gameObject.name);
        //     isJump = false;
        //     isDoubleJump = false;
        // }
        // else Debug.Log("null");

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

    public void Jump(){
        if(!isJump || !isDoubleJump){
            rd.velocity = new Vector3(0,0,0);
            rd.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
            if(isJump) {
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
        healthBar.fillAmount = _health / _maxHealth;
        Debug.Log(_health + " / " + _maxHealth);
        at.SetTrigger("hit");
    }

    public void bomHit(float dame){
        if(dame == 0) return;
        _health = Mathf.Clamp(_health + dame, 0, _maxHealth);
        healthBar.fillAmount = _health / _maxHealth;
        Debug.Log(_health + " / " + _maxHealth);
        at.SetTrigger("hit");
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other != null){
            if(other.gameObject.tag == "Ground"){
                Debug.Log("In ground!");
                isJump = false;
                isDoubleJump = false;
            }
        }
    }
}