using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ShopHouse : Shop
{
    [SerializeField] private GameObject prefabHouse;
    [SerializeField] private Sprite[] ListImg;
    [SerializeField] private GameObject[] ListHouse;
    [SerializeField] private Sprite imageUsed;
    private Sprite SpriteChange;
    public ItemTemplate[] items;


    private void Awake()
    {
        for(int i = 0; i < ListHouse.Length; i++)
        {
            int index = i;
            ListHouse[index].GetComponentInChildren<Button>().onClick.AddListener(()=> BuyItem(index));
        }
        LoadItemFromJson();
        LoadObjectFromItem();

    }  

    public override void UpdateUI()
    {
        prefabHouse.GetComponent<SpriteRenderer>().sprite = SpriteChange;
        GameObject home = GameObject.FindGameObjectWithTag("Home");
        home.GetComponent<SpriteRenderer>().sprite = SpriteChange;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void BuyItem(int index)
    {
        Debug.Log(index);
        if (items[index].isUnlocker)
        {
            SpriteChange = ListImg[index];
            if (GameController.Instance.player != null)
           
                GameController.Instance.player.SpriteHouse = SpriteChange.name;
            ReloadList(items[index]);
            UpdateUI();
        }
        else
        {
        }
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
            JsonItem[i] = JsonUtility.ToJson(items[i]);
        }
        string combinedJson = string.Join("\n", JsonItem);
        File.WriteAllText(Application.persistentDataPath + "/dataShopHouse.json", combinedJson);
    }
    private void ReloadList(ItemTemplate itemGround)
    {
        foreach (var item in items)
        {
            if (item != itemGround && item.isUnlocker)
            {
                item.isUsed = false;
            }
        }
    }
    private void LoadObjectFromItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].isUnlocker)
            {
                ListHouse[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = imageUsed;
            }
        }
    }
    private void LoadItemFromJson()
    {
        string filePath = Application.persistentDataPath + "/dataShopHouse.json";

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

    // Start is called before the first frame update

}
