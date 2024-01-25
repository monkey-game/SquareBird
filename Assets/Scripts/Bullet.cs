using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 10f;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeanPool.Despawn(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed *Time.deltaTime);
    }
}
