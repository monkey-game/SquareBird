﻿using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopGround : Shop
{
    [SerializeField] private GameObject[] SkinGround;
    [SerializeField] private Sprite[] BGround;
    [SerializeField] private Sprite[] Ground;
    [SerializeField] private GameObject prefabGround;
    [SerializeField] private GameObject UISHOP;
    [SerializeField] private GameObject prefabBGround;
    [SerializeField] private GameObject TrapGround;
    [SerializeField] private GameObject TrapBGround;
    [SerializeField] private Sprite spriteGround;
    [SerializeField] private Sprite spriteBGround;
    [SerializeField] private Sprite imageUsed;
    [SerializeField] private Text textCoin;
    public ItemTemplate[] items;
    private int indexWait;

    private void Awake()
    {
        LoadItemFromJson();
        for(int i = 0; i < SkinGround.Length; i++)
        {
            int index = i;
            SkinGround[i].GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(index));
        }
        LoadObjectFromItem();
        
    }
    private void Update()
    {
        textCoin.text = GameController.Instance.player.Coin.ToString();

        RewardADS(indexWait);

    }
    private void OnDisable()
    {
        SaveToJson();
    }
    public override void UpdateUI()
    {
        UISHOP.SetActive(false);
        prefabGround.GetComponent<SpriteRenderer>().sprite = spriteGround;
        prefabBGround.GetComponent<SpriteRenderer>().sprite = spriteBGround;
        TrapBGround.GetComponent<SpriteRenderer>().sprite = spriteGround;
        TrapGround.GetComponent<SpriteRenderer>().sprite = spriteBGround;
        UpdateSpriteTrap();
        UpdateSpriteGround();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void UpdateSpriteTrap()
    {
        GameObject[] trap = GameObject.FindGameObjectsWithTag("Trap");
        foreach(GameObject obj in trap) 
        {
            if(obj.layer == 7)
            {
                obj.GetComponent<SpriteRenderer>().sprite = spriteGround;
            }else if(obj.layer == 11)
            {
                obj.GetComponent<SpriteRenderer>().sprite = spriteBGround;
            }
        }
    }
    private void UpdateSpriteGround()
    {
        GameObject[] trap = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in trap)
        {
            if (obj.layer == 8)
            {
                obj.GetComponent<SpriteRenderer>().sprite = spriteGround;
            }
            else if (obj.layer == 11)
            {
                obj.GetComponent<SpriteRenderer>().sprite = spriteBGround;
            }
        }
    }

    public override void BuyItem(int index)
    {
        if (items[index].isUnlocker)
        {
            items[index].isUsed = true;
            spriteGround = Ground[index];
            spriteBGround = BGround[index];
            ReloadList(items[index]);
            if (GameController.Instance.player != null)
            {
                GameController.Instance.player.SpriteGround = spriteGround.name;
                GameController.Instance.player.spriteBGround = spriteBGround.name;
            }
            UpdateUI();
        }
        if (items[index].price == 0&& !items[index].isUnlocker)
        {
            ADSManager.Instance.rewardedAds.LoadAd();
            ADSManager.Instance.rewardedAds.ShowAd();
            indexWait = index;
        }
        if (GameController.Instance.player.Coin >= items[index].price&& items[index].price > 0)
        {
            GameController.Instance.player.Coin-= items[index].price;
            items[index].isUnlocker = true;
            SkinGround[index].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
            if(SkinGround[index].GetComponentInChildren<Text>() != null)
            SkinGround[index].GetComponentInChildren<Text>().enabled = false;
        }
    }
    public override void SaveToJson()
    {
        string[] JsonItem = new string[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            JsonItem[i] = JsonUtility.ToJson(items[i]);
        }
        string combinedJson = string.Join("\n", JsonItem);
        File.WriteAllText(Application.persistentDataPath + "/dataShopGround.json", combinedJson);
    }
    private void ReloadList(ItemTemplate itemGround)
    {
        foreach (var item in items)
        {
            if(item.Id != itemGround.Id && item.isUnlocker)
            {
                item.isUsed = false;
            }
        }
    }
    public override void LoadObjectFromItem()
    {
        for(int i = 0;i < items.Length;i++)
        {
            if (items[i].isUnlocker) 
            {
                SkinGround[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
                if(SkinGround[i].GetComponentInChildren<Text>() != null)
                SkinGround[i].GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
    public override void LoadItemFromJson()
    {
        string filePath = Application.persistentDataPath + "/dataShopGround.json";

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

    public override void RewardADS(int index)
    {
        if (GameController.Instance.RewardADS)
        {
            items[index].isUnlocker = true;
            SkinGround[index].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
            if (SkinGround[index].GetComponentInChildren<Text>() != null)
                SkinGround[index].GetComponentInChildren<Text>().enabled = false;
            GameController.Instance.RewardADS = false;
        }
    }
}
