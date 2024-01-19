using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float frameRate = 0.1f; 

    private SpriteRenderer spriteRenderer;
    private Sprite[] frames; 
    private int currentFrame = 0;
    private float timer = 0f;
    private PlayerManager playerManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frames = SkinManager.instance.currentSkin.animationSprites;
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (SkinManager.instance.currentSkin.hasAnimation&& !playerManager.isStop)
        {
            timer += Time.deltaTime;
            if (timer > frameRate)
            {
                timer = 0f;

                currentFrame = (currentFrame + 1) % frames.Length;

                spriteRenderer.sprite = frames[currentFrame];
            }
        }else if (playerManager.isStop)
        {
            spriteRenderer.sprite = SkinManager.instance.currentSkin.deathSprite;          
        }
        else
        {
            spriteRenderer.sprite = frames[0];
        }  
    }
}
