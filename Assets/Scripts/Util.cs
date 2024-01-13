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
            JsonUtility.FromJsonOverwrite(Player, GameController.Instance.player);
        }
        else
        {
            SaveToPlayerJson(pl);
            GameController.Instance.player = pl;
        }
    }
}
