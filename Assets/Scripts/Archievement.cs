using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archievement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text[] text;
    void Start()
    {
        for (int i = 0; i < ScoreManager.Instance.ListScore.Count; i++)
        {
            if (i == 10) return;
            text[i].text = ScoreManager.Instance.ListScore[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
