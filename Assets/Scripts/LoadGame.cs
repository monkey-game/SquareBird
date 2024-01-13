using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite BGround;
    private Sprite Ground;
    private Sprite Player;
    private Sprite House;
    private Sprite Bulllet;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LoadGameFormPlayer();
    }

    private void LoadGameFormPlayer()
    {
        if (GameController.Instance.LoadGame&& GameController.Instance.player != null)
        {
            if (GameController.Instance.player.spriteBGround != null && GameController.Instance.player.SpriteGround != null)
            {
                BGround = Resources.Load<Sprite>("image/" + GameController.Instance.player.spriteBGround);
                Ground = Resources.Load<Sprite>("image/" + GameController.Instance.player.SpriteGround);
                UpdateSpriteTrap();
                UpdateSpriteGround();              
            }
            if (GameController.Instance.player.SpritePlayer != null)
            {

            }
            if (GameController.Instance.player.SpriteBullet != null)
            {
                Bulllet = Resources.Load<Sprite>("image/" + GameController.Instance.player.SpriteBullet);
                GameObject Bullet = GameObject.FindGameObjectWithTag("Bullet");
                if (Bullet != null)
                Bullet.GetComponent<SpriteRenderer>().sprite = Bulllet;
            }
            if (GameController.Instance.player.SpriteHouse != null)
            {
                House = Resources.Load<Sprite>("image/" + GameController.Instance.player.SpriteHouse);
                GameObject home = GameObject.FindGameObjectWithTag("Home");
                if (home != null) 
                home.GetComponent<SpriteRenderer>().sprite = House;
            }
            GameController.Instance.LoadMap(GameController.Instance.player.Level);
            GameController.Instance.LoadGame = false;
        }
    }

    private void UpdateSpriteTrap()
    {
        GameObject[] trap = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject obj in trap)
        {
            if (obj.layer == 7)
            {
                obj.GetComponent<SpriteRenderer>().sprite = Ground;
            }
            else if (obj.layer == 11)
            {
                obj.GetComponent<SpriteRenderer>().sprite = BGround;
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
                obj.GetComponent<SpriteRenderer>().sprite = Ground;
            }
            else if (obj.layer == 11)
            {
                obj.GetComponent<SpriteRenderer>().sprite = BGround;
            }
        }
    }
}
