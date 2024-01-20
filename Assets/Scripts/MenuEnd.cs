using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEnd : MonoBehaviour
{
    private Transform player;
    [SerializeField]private Transform HomePos;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textBest;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        textScore.text = ScoreManager.Instance.scoreNow.ToString();
        textBest.text = ScoreManager.Instance.BestScore.ToString();
        RevivePlayer();
    }
    public void OnClickRestart()
    {
        player.position = HomePos.position;
        PlayerManager.IsReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickRevive(){
        ADSManager.Instance.rewardedAds.LoadAd();
        ADSManager.Instance.rewardedAds.ShowAd();
    }
    private void RevivePlayer(){
        if(GameController.Instance.RewardADS){
            GameController.Instance.RewardADS = false;
            PlayerManager.IsReset = true;
            PlayerManager.IsRevive = true;
            gameObject.SetActive(false);
        }
    }
}
