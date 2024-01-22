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
    [SerializeField] private GameObject UIRevive;
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
        GameController.Instance.ResetCamera= true;
        StartCoroutine(ResetCamara());
        PlayerManager.IsReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickRevive(){
        ADSManager.Instance.rewardedAds.LoadAd();
        ADSManager.Instance.rewardedAds.ShowAd();
    }
    private void RevivePlayer(){
        if(GameController.Instance.RewardADS){
            GameController.Instance.ResetCamera = true;
            StartCoroutine(ResetCamara());
            GameController.Instance.RewardADS = false;
            PlayerManager.IsReset = true;
            PlayerManager.IsRevive = true;
            UIRevive.SetActive(true); 
            gameObject.SetActive(false);
        }
    }
    IEnumerator ResetCamara()
    {
        yield return new WaitForSeconds(2);
        GameController.Instance.ResetCamera = false;
    }
}
