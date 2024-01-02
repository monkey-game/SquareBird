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
        PlayerPrefs.SetInt("BestScorePlayer", BestScore);
    }
    public void LoadBestScore()
    {
        PlayerPrefs.GetInt("BestScorePlayer", BestScore);
    }
}
