using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance {  get; private set; }
    public SkinData[] listSkin;
    public SkinData currentSkin;
    private int indexSkin;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance != null && instance != this)
        {
            Destroy(instance);
        }
        LoadSkin();
    }
    private void OnDestroy()
    {
        Util.SaveDataInt("indexSkin", indexSkin);
    }
    private void LoadSkin()
    {
        indexSkin = Util.LoadDataInt("indexSkin");
        currentSkin = listSkin[indexSkin];
    }
    public void ChangeSkin(int index)
    {
        currentSkin = listSkin[index];
        indexSkin = index;
    }
}
