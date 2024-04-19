using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EventButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIPlay;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UISetting;
    [SerializeField] private GameObject AudioSlider;
    [SerializeField] private TextMeshProUGUI AudioText;

    private Player PlayerCtrl;
    private float audioValue;

    void Start()
    {
        PlayerCtrl = player.GetComponent<Player>();
        audioValue = 25;
        AudioSlider.GetComponent<Slider>().value = audioValue / 100;
    }

    void Update()
    {
        if(PlayerCtrl.isDie){
            SceneManager.LoadScene("StartScence");
        }
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
            AudioText.text = ((int)audioValue).ToString();
        } else {
            Debug.Log("AudioSlider is null");
        }
    }
}
