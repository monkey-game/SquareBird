using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject obj;
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject objSpawn = LeanPool.Spawn(obj,transform.position,Quaternion.identity);
            objSpawn.GetComponent<ParticleSystem>().Play();
            LeanPool.Despawn(objSpawn, 2);
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
