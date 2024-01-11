using System;
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
        textCoin.text = ScoreManager.Instance.Coin.ToString();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void BuyItem(int index)
    {
        if (items[index].isUnlocker)
        {
            items[index].isUsed = true;
            spriteGround = Ground[index];
            spriteBGround = BGround[index];
            UpdateUI();
        }
        if(ScoreManager.Instance.Coin >= items[index].price)
        {
            ScoreManager.Instance.Coin-= items[index].price;
            items[index].isUnlocker = true;
            SkinGround[index].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
            SkinGround[index].GetComponentInChildren<Text>().enabled = false;
        }
        Debug.Log(index);
    }
    private void SaveToJson()
    {
        string[] JsonItem = new string[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            JsonItem[i] = JsonUtility.ToJson(items[i]);
        }
        string combinedJson = string.Join("\n", JsonItem);
        File.WriteAllText(Application.persistentDataPath + "/dataShopGround.json", combinedJson);
    }
    private void LoadObjectFromItem()
    {
        for(int i = 0;i < items.Length;i++)
        {
            if (items[i].isUnlocker) 
            {
                SkinGround[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
                SkinGround[i].GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
    private void LoadItemFromJson()
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
}
