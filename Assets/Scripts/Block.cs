using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask barrier;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    bool isAttached = true; // Biến để xác định nếu block đang nằm trên player

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            StartCoroutine(DestroyObject());
        }
        if (collision.gameObject.CompareTag("Obstructions"))
        {
            Vector3 blockPosition = transform.position;
            Vector3 obstaclePosition = collision.gameObject.transform.position;
            if (blockPosition.y > obstaclePosition.y)
            {
                // Block ở trên vật cản, giữ nguyên
                isAttached = true;
            }
            else
            {
                // Block không ở trên vật cản, rời khỏi player
                transform.parent = null;
                rb.constraints = RigidbodyConstraints2D.None;
                isAttached = false;
            }           
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5);
 
            Destroy(gameObject);
       
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }

}
