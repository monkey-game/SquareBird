using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayerManager.IsReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
