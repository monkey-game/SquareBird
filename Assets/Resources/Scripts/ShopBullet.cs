using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopBullet : Shop
{
    [SerializeField] private GameObject[] SkinBullets;
    [SerializeField] private Sprite[] spriteBullet;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject UIShopBullet;
    [SerializeField] private Sprite ButtonUse;
    public ItemTemplate[] items;
    private Sprite SpriteChange;
    private int indexWait;

    private void Start()
    {
        for(int i = 0; i < SkinBullets.Length; i++)
        {
            int index = i;
            SkinBullets[i].GetComponent<Button>().onClick.AddListener(() => BuyItem(index));
            Button[] childButtons = SkinBullets[i].GetComponentsInChildren<Button>();
            foreach (Button childButton in childButtons)
            {
                if (childButton.transform != SkinBullets[i].transform)
                {
                    childButton.onClick.AddListener(() => BuyItem(index));
                }
            }
        }     
        LoadItemFromJson();
        LoadObjectFromItem();
    }
    private void Update()
    {
        RewardADS(indexWait);
    }
    public override void BuyItem(int index)
    {
        if (items[index].isUnlocker)
        {
            SpriteChange = spriteBullet[index];
            ReloadList(items[index]);
            if (GameController.Instance.player != null)
          
                GameController.Instance.player.SpriteBullet = SpriteChange.name;
            UpdateUI();
        }
        else
        {
            ADSManager.Instance.rewardedAds.LoadAd();
            ADSManager.Instance.rewardedAds.ShowAd();
            indexWait = index;
        }
    }

    public override void UpdateUI()
    {
        prefabBullet.GetComponent<SpriteRenderer>().sprite = SpriteChange;
        GameObject obj = GameObject.FindGameObjectWithTag("Bullet");
        if (obj != null)
        {
            obj.GetComponent<SpriteRenderer>().sprite = SpriteChange;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    private void OnDisable()
    {
        SaveToJson();
    }
    private void SaveToJson()
    {
        string[] JsonItem = new string[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Id = i;
            JsonItem[i] = JsonUtility.ToJson(items[i]);
        }
        string combinedJson = string.Join("\n", JsonItem);
        File.WriteAllText(Application.persistentDataPath + "/dataShopBullet.json", combinedJson);
    }
    private void ReloadList(ItemTemplate itemGround)
    {
        foreach (var item in items)
        {
            if (item.Id != itemGround.Id && item.isUnlocker)
            {
                item.isUsed = false;
            }
        }
    }
    private void LoadObjectFromItem()
    {
       for(int index = 0; index < items.Length; index++)
        {
            if (items[index].isUnlocker)
            {
                Button[] childButtons = SkinBullets[index].GetComponentsInChildren<Button>();
                foreach (Button childButton in childButtons)
                {
                    if (childButton.transform != SkinBullets[index].transform)
                    {
                        childButton.GetComponent<Image>().sprite = ButtonUse;
                    }
                }
            }
        }
    }
    private void LoadItemFromJson()
    {
        string filePath = Application.persistentDataPath + "/dataShopBullet.json";

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
            Button[] childButtons = SkinBullets[index].GetComponentsInChildren<Button>();
            foreach (Button childButton in childButtons)
            {
                if (childButton.transform != SkinBullets[index].transform)
                {
                    childButton.GetComponent<Image>().sprite = ButtonUse;
                }
            }
            GameController.Instance.RewardADS= false;
        }
    }
}
