using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSkin : Shop
{
    [SerializeField] private GameObject[] ListSkin;
    [SerializeField] private Sprite spriteUse;
    [SerializeField] private ItemTemplate[] items;
    [SerializeField] private GameObject prefabPlayer;
    private Sprite spritePlayer;
    private int IndexItem;
    
    // Start is called before the first frame update
    void Start()
    {
        AddEvent();
        LoadItemFromJson();
        LoadObjectFromItem();
    }


    // Update is called once per frame
    void Update()
    {
        RewardADS(IndexItem);
        if(GameController.Instance.player.NoADS)
        {
            items[37].isUnlocker = true;
            items[38].isUnlocker = true;
        }
        if (GameController.Instance.player.GoldPack)
        {
            items[36].isUnlocker = true;
        }
    }
    private void OnDestroy()
    {
        SaveToJson();
    }
    private void AddEvent()
    {
        for (int i = 0; i < ListSkin.Length; i++)
        {
            int index = i;
            Button[] buttons = ListSkin[i].GetComponentsInChildren<Button>();
            foreach (Button b in buttons)
            {
                b.onClick.AddListener(() => BuyItem(index));
            }
        }
    }
    public override void BuyItem(int index)
    {
        if (items[index].isUnlocker)
        {
            items[index].isUsed = true;
            SkinManager.instance.ChangeSkin(index);
            spritePlayer = SkinManager.instance.currentSkin.animationSprites[0];
            UpdateUI();
        }else if (items[index].price == 0)
        {
            ADSManager.Instance.rewardedAds.LoadAd();
            ADSManager.Instance.rewardedAds.ShowAd();
            IndexItem = index;
        }
        else
        {
            if(GameController.Instance.player.Coin > items[index].price)
            {
                GameController.Instance.player.Coin -= items[index].price;
                items[index].isUnlocker = true;
                UpdateButton(index);
            }
        }
    }

    public override void RewardADS(int index)
    {
        if (GameController.Instance.RewardADS)
        {
            GameController.Instance.RewardADS = false;
            items[index].isUnlocker = true;
            UpdateButton(index);
        }
    }

    private void UpdateButton(int index)
    {
        Button[] buttons = ListSkin[index].GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.transform != ListSkin[index].transform)
            {
                button.GetComponent<Image>().sprite = spriteUse;
            }
        }
        Text text = ListSkin[index].GetComponentInChildren<Text>();
        if(text != null)
        text.transform.gameObject.SetActive(false);
    }

    public override void UpdateUI()
    {
        prefabPlayer.GetComponent<SpriteRenderer>().sprite = spritePlayer;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void SaveToJson()
    {
        string[] JsonItem = new string[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Id = i;
            JsonItem[i] = JsonUtility.ToJson(items[i]);
        }
        string combinedJson = string.Join("\n", JsonItem);
        File.WriteAllText(Application.persistentDataPath + "/dataShopSkin.json", combinedJson);
    }

    public override void LoadItemFromJson()
    {
        string filePath = Application.persistentDataPath + "/dataShopSkin.json";

        if (File.Exists(filePath))
        {
            string[] JsonItem = File.ReadAllLines(filePath);
            if (JsonItem != null)
            {
                for (int i = 0; i < JsonItem.Length; i++)
                {
                    JsonUtility.FromJsonOverwrite(JsonItem[i], items[i]);
                }
            }
            else
                return;
        }
    }

    public override void LoadObjectFromItem()
    {
        for(int i = 0;i < items.Length;i++)
        {
            if (items[i].isUnlocker)
            {
                UpdateButton(i);
            }
        }
    }
}
