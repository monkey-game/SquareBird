using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int scoreNow;
    public int BestScore;
    public string NamePlayer;
    public int CoinPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this&& Instance != null)
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
       LoadBestScore();
        CoinPlayer = 1000;
    }

    // Update is called once per frame
    void Update()
    {      
    }
    public void UpdateScore()
    {

    }
    public void SaveBestScore()
    {
        if (BestScore < scoreNow)
        {
            PlayerPrefs.SetInt("BestScorePlayer", BestScore);
            BestScore = scoreNow;
        }
    }
    public void LoadBestScore()
    {
        PlayerPrefs.GetInt("BestScorePlayer", BestScore);
    }
}
