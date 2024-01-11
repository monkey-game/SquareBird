using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int scoreNow;
    public int BestScore;
    public string NamePlayer;
    public int Coin;
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
        LoadCoin();
    }

    // Update is called once per frame
    public void UpdateCoin()
    {
        PlayerPrefs.SetInt("Coin",Coin);
    }
    public void LoadCoin()
    {
        PlayerPrefs.GetInt("Coin", Coin);
    }
    public void UpdateScore()
    {

    }
    public void SaveBestScore()
    {
        if (BestScore < scoreNow)
        {
            PlayerPrefs.SetInt("BestScore_"+name, BestScore);
            BestScore = scoreNow;
            Social.ReportScore(BestScore, "CgkIr6b-jqcDEAIQAw", (bool isSucces) =>
            {
                Debug.Log("Success");
            });
        }
    }
    public void LoadBestScore()
    {
        PlayerPrefs.GetInt("BestScore_"+name, BestScore);
    }
}
