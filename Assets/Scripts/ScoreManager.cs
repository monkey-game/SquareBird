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
        BestScore = LoadBestScore();
    }
    private void OnDestroy()
    {
        SaveBestScore();
    }

    // Update is called once per frame 
    public void UpdateScore()
    {

    }
    public void SaveBestScore()
    {
        if (BestScore < scoreNow)
        {
            BestScore = scoreNow;
            PlayerPrefs.SetInt("BestScore",BestScore);
            PlayerPrefs.Save();
            Social.ReportScore(BestScore, "CgkIr6b-jqcDEAIQAw", (bool isSucces) =>
            {
                Debug.Log("Success");
            });
            Debug.Log(BestScore);     
        }
    }
    public int LoadBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }
}
