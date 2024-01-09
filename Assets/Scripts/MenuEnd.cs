using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnd : MonoBehaviour
{
    private Transform player;
    [SerializeField]private Transform HomePos;
    public void OnClickRestart()
    {
        player.position = HomePos.position;
    }
}
