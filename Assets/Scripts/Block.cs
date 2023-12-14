using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask barrier;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBarrierRight()&& !isBarrierDown())
        {
            gameObject.transform.parent = null;
            StartCoroutine(DestroyObject());
        }
    }
    
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        if(gameObject.transform.parent == null) 
        {
            Destroy(gameObject);
        }
    }
    bool isBarrierRight()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }

}
