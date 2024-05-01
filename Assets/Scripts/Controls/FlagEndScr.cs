using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagEndScr : MonoBehaviour
{
    private Animator at;

    [SerializeField] string nextScence;
    // Start is called before the first frame update
    void Start()
    {
        at = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c!=null){
            if(c.gameObject.tag == "Player"){
                this.onFlag();
            }
        }
    }

    public void onFlag(){
        at.SetBool("flag", true);
        Invoke("LoadNextScene",3);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextScence);
    }
}
