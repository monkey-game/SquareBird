using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void UpdateUI();
    public abstract void BuyItem(int index);
    public abstract void RewardADS(int index);
}
