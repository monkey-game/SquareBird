using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoin : MonoBehaviour
{
    [SerializeField] private List<Text> coinList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var coin in coinList)
        {
            if(coin != null && coin.enabled)
            {
                coin.text = GameController.Instance.player.Coin.ToString();
            }
        }
    }
}
