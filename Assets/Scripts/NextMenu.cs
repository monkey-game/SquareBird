using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform HomePos;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] listMap;
    private Transform TransOld;
    private int Level;

    private void Awake()
    {
       // Player = GameObject.Find(GameController.Instance.NameBird);
    }
    private void Update()
    {
        Level = 1;
    }
    public void OnClickNextButton()
    {
        TransOld = listMap[Level - 1].gameObject.transform;
        Destroy(listMap[Level-1].gameObject);
        Instantiate(listMap[Level].gameObject, TransOld.position,Quaternion.identity);
        Player.transform.position = HomePos.position;
    }
}
