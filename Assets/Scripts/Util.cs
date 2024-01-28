using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Util:MonoBehaviour
{
    // Start is called before the first frame update
   public static void SaveDataString(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }
    public static string LoadDataString(string name) {  return PlayerPrefs.GetString(name); }
    public static void SaveDataInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }
    public static int LoadDataInt(string name) { return PlayerPrefs.GetInt(name); }
    public static void SaveToPlayerJson(PlayerBase pl)
    {
        string combinedJson = JsonUtility.ToJson(pl);
        File.WriteAllText(Application.persistentDataPath + "/player.json", combinedJson);
    }
    public static void GetPlayerFromJson(PlayerBase pl)
    {
        string Path = Application.persistentDataPath + "/player.json";
        if (File.Exists(Path))
        {
            GameController.Instance.player = new PlayerBase(pl.Id,pl.name);
            string Player = File.ReadAllText(Path);
            if(GameController.Instance.player.Coin >= 1000){
                Social.ReportProgress("CgkIr6b-jqcDEAIQBg",100.0f,(bool Success)=>{});
            }
            JsonUtility.FromJsonOverwrite(Player, GameController.Instance.player);
        }
        else
        {
            SaveToPlayerJson(pl);
            Social.ReportProgress("CgkIr6b-jqcDEAIQBQ",0.0f,(bool Success)=>{});
            Social.ReportProgress("CgkIr6b-jqcDEAIQBg",0.0f,(bool Success)=>{});
            MainMenu.isNewBie = true;
            GameController.Instance.player = pl;
        }
    }
}
