using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    private ParticleSystem _particleSystem;
    private BoxCollider2D _boxCollider;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _particleSystem.Play();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
