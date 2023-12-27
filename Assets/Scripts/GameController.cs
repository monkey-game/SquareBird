using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Slider sliderBar;
    [SerializeField] private float timePlay;
    [SerializeField] private GameObject[] BirdPerfect;
    [SerializeField] private Text ScoreNow;
    [SerializeField]private Text LevelNow;
    [SerializeField] private Text NextLevel;
    public static short CountPerfect;
    public static short Score;
    public static bool ResetBird = false;
    private void Update()
    {
        if (MainMenu.isStartGame)
        {
            float currentTime = Time.time;

            float timeRatio = Mathf.Clamp01(currentTime / timePlay);

            UpdateTimeBar(timeRatio);
            if(CountPerfect > 0)
            {
                UpdateBirdBar(CountPerfect);
            }
            if(ResetBird)
            {
                ResetBirdBar();
            }
            ScoreNow.text = $"{Score}";
        }
    }
    private void UpdateBirdBar(short CountPerfect)
    {
        if(CountPerfect > 0&&CountPerfect < 3)
        {
            BirdPerfect[CountPerfect-1].SetActive(true);
        }
    }
    private void ResetBirdBar()
    {
        foreach(GameObject bird in BirdPerfect)
        {
            bird.SetActive(false);
        }
        ResetBird = false;
    }
    private void UpdateTimeBar(float value)
    {
        if (sliderBar != null)
        {
            sliderBar.value = value;
        }
    }
}
