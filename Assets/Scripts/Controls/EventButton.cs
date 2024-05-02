using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EventButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject desappearing;
    [SerializeField] private GameObject UIPlay;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UISetting;
    [SerializeField] private GameObject AudioSlider;
    [SerializeField] private TextMeshProUGUI AudioText;
    [SerializeField] private TextMeshProUGUI HealthPointText;
    [SerializeField] private GameObject CameraUI;
    [SerializeField] private AudioSource BgAudio;

    private Player PlayerCtrl;
    private float audioValue;

    void Start()
    {
        if(player != null) PlayerCtrl = player.GetComponent<Player>();
        audioValue = 25;
        AudioSlider.GetComponent<Slider>().value = audioValue / 100;
        BgAudio.GetComponent<AudioSource>().volume = audioValue / 100;
    }

    void Update()
    {
        if(player != null && PlayerCtrl.isDie){
            Instantiate(desappearing,player.transform.position,Quaternion.identity);
            if(CameraUI != null){
                CameraUI.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            }
            Destroy(player);
            Invoke("LoadStartScene",2);
        }
        if(player != null) HealthPointText.text = PlayerCtrl.getHealth.ToString("F2");
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene("StartScence");
    }

    public void btnLeftDown(){
        PlayerCtrl.setDirMove(-1);
        PlayerCtrl.isMobile = true;
    }

    public void btnLeftUp(){
        PlayerCtrl.setDirMove(0);
        PlayerCtrl.isMobile = false;
    }

    public void btnRightDown(){
        PlayerCtrl.setDirMove(1);
        PlayerCtrl.isMobile = true;
    }

    public void btnRightUp(){
        PlayerCtrl.setDirMove(0);
        PlayerCtrl.isMobile = false;
    }

    public void btnJumpClick(){
        PlayerCtrl.Jump();
    }

    public void btnSlipClick(){
        PlayerCtrl.Slip();
    }

    public void btnPlaytoPauseClick(){
        if(UIPause != null) UIPause.SetActive(true);
        if(player != null) player.SetActive(false);
        if(UIPlay != null) UIPlay.SetActive(false);
    }

    public void btnSettingClick(){
        if(UISetting != null) UISetting.SetActive(true);
        if(player != null) player.SetActive(false);
        if(UIPlay != null) UIPlay.SetActive(false);
        if(UIPause != null) UIPause.SetActive(false);
    }

    public void btnPausetoPlayClick(){
        if(UIPlay != null) UIPlay.SetActive(true);
        if(player != null) player.SetActive(true);
        if(UIPause != null) UIPause.SetActive(false);
        if(UISetting != null) UISetting.SetActive(false);
    }

    public void btnQuitClick(){
        SceneManager.LoadScene("StartScence");
    }

    public void sliderAudioChange(){
        if (AudioSlider != null) {
            audioValue = AudioSlider.GetComponent<Slider>().value * 100;
            BgAudio.GetComponent<AudioSource>().volume = audioValue / 100;
            AudioText.text = ((int)audioValue).ToString();
        } else {
            Debug.Log("AudioSlider is null");
        }
    }
}
