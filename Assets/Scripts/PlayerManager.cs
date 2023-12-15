using Microsoft.Unity.VisualStudio.Editor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private LayerMask barrier;
    [SerializeField] private GameObject winLine;
    public UnityEvent eventDestroy;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private GameObject lastBlock;
    private Animator animator;
    private bool isWinLine = false;
    private bool isStop = false;
    private int speed = 5;
    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            isStop = true;
        }
        if (collision.gameObject.CompareTag("WinLine"))
        {            
            isWinLine = true;
            winLine.GetComponent<BoxCollider2D>().enabled = false;
            eventDestroy?.Invoke();
        }
        if (collision.gameObject.CompareTag("Obstructions"))
        {
            Vector3 blockPosition = transform.position;
            Vector3 obstaclePosition = collision.gameObject.transform.position;
            if (blockPosition.y < obstaclePosition.y)
            {
                IdleDie();
            }
        }
    }

    private void IdleDie()
    {
        isStop = true;
        animator.SetTrigger("IsDie");
        body.AddForce(Vector3.left * 7f, (ForceMode2D)ForceMode.Impulse);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWinLine&& !isStop)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }else if (isWinLine&& !isStop)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
        if (Input.GetMouseButtonDown(0))
        {
            // body.AddForce(Vector3.up);
            //   block[isCount].SetActive(true);
            //   isCount++;
            //body.velocity = new Vector2(body.velocity.x, 5f);
            Jump();
            CreateBlockUnderPlayer();

        }
    }

    void Jump()
    {
         body.velocity = new Vector2(body.velocity.x, 2f);
    }

    void CreateBlockUnderPlayer()
    {
        if (lastBlock == null)
        {
            // Nếu chưa có block nào, tạo block dưới player
            lastBlock = Instantiate(blockPrefab, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity,transform);
            lastBlock.SetActive(true);
        }
        else
        {
            // Nếu đã có block, tạo block mới ở dưới block trước đó
            lastBlock.transform.position = lastBlock.transform.position - new Vector3(0, 0.7f, 0);
            GameObject newBlock = Instantiate(blockPrefab, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity,transform);
            lastBlock = newBlock; // Gán lastBlock là block vừa tạo
            lastBlock.SetActive(true);
        }
    }
    bool isBarrierRight()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
}
