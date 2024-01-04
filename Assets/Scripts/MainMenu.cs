using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    public static bool isStartGame = false;
    private GameObject CanvasStartGame;
    private GameObject ObSetting;
    private bool isPlay;
    [SerializeField]private GameObject CanvasPlayer;
    [SerializeField] private GameObject UiShopGround;
    private void Awake()
    {
        CanvasStartGame = GameObject.Find("CanvasStartGame");
        ObSetting = GameObject.Find("Setting");
    }
    public void OnClickSetting()
    {
        Animation animation = ObSetting.GetComponent<Animation>();
        if (isPlay)
        {
            animation.Play("SettingClose");
            isPlay = false;
        }
        else
        {
            animation.Play();
            isPlay = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isStartGame = true;       
        Debug.Log("StartGame");
        CanvasStartGame.SetActive(false);
        CanvasPlayer.SetActive(true);
    }
}
