using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime * speed);
    }
}
