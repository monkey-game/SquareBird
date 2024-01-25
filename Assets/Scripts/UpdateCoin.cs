using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoin : MonoBehaviour
{
    // Start is called before the first frame update
    private Text coin;
    private void Awake()
    {
        coin = GetComponent<Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coin != null)
            coin.text = GameController.Instance.player.Coin.ToString();
    }
}
