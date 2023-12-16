using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask barrier;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    bool isAttached = true; 
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        FindObjectOfType<PlayerManager>().eventDestroy.AddListener(DestroyByWin);
    }
    private void OnDestroy()
    {
        FindObjectOfType<PlayerManager>().eventDestroy.RemoveListener(DestroyByWin);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            StartCoroutine(DestroyObject());
        }
        if (collision.gameObject.CompareTag("Obstructions"))
        {
            Vector3 blockPosition = transform.position;
            Vector3 obstaclePosition = collision.gameObject.transform.position;
            if (blockPosition.y < obstaclePosition.y)
            {
                transform.parent = null;
                isAttached = false;               
            } 
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            if(transform.parent != null)
            {
                if(transform.position.x - collision.transform.position.x < 0.1f)
                transform.position = new Vector3(collision.transform.position.x,transform.position.y,transform.position.z);
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5);

    //    PlayerManager.listBlock.RemoveAll(x => x.gameObject.name.Equals(gameObject.name));
            Destroy(gameObject);
       
    }
    private void Update()
    {
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
    void DestroyByWin()
    {
        Destroy(gameObject);
    }


}
