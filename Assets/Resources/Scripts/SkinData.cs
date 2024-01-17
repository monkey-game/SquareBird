using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Custom/Skin")]
public class SkinData : ScriptableObject
{
    public string skinName;
    public Sprite[] animationSprites;
    public Sprite deathSprite;
    public bool hasAnimation;
}
