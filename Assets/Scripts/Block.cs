using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask barrier;
    [SerializeField] private GameObject player;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    bool isAttached = true;
    private Vector3 PosParent;
    private Vector3 PosChild;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
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
        if (collision.gameObject.CompareTag("Trap"))
        {
            Vector3 blockPosition = transform.position;
            Vector3 obstaclePosition = collision.gameObject.transform.position;           
                if (blockPosition.y < obstaclePosition.y)
                {
                    isAttached = false;
                }
                if ((blockPosition.y - obstaclePosition.y) < 0.75f)
                {
                if (!isBarrierDown())
                {
                    isAttached = false;
                }
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
        if (isAttached)
        {
            PosParent = player.transform.position;
            PosChild = transform.position;
            PosChild.x = PosParent.x - 0.1f;
            transform.position = PosChild;
        }
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
    bool isBarrierleft()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(rb.velocity.x *-1,0), 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
    void DestroyByWin()
    {
        Destroy(gameObject);
    }


}
