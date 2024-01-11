using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
     private Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ScoreManager.Instance.Coin++;
        Animator.SetTrigger("IsClose");
        Destroy(gameObject, 2);
    }
}
