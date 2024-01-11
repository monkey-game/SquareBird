using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnd : MonoBehaviour
{
    private Transform player;
    [SerializeField]private Transform HomePos;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    public void OnClickRestart()
    {
        player.position = HomePos.position;
        player.Rotate(0, 0, 0);
    }
}
