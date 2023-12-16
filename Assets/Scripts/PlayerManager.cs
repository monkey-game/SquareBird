using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject BlockPre;
    [SerializeField] private LayerMask barrier;
    [SerializeField] private GameObject winLine;
    [SerializeField] private GameObject BulletOb;
    [SerializeField] private float interval;
    public UnityEvent eventDestroy;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private GameObject lastBlock;
    private Animator animator;
    private bool isWinLine = false;
    private bool isStop = false;
    private int speed = 5;
    private float nextBulletTime;
    private bool StartShooting = false;
    private int count = 0;
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
            if((blockPosition.y - obstaclePosition.y) < 0.95f)
            {
                IdleDie();
            }
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();     
        if (StartShooting)
        {
            CreateBullet();
        }
        if(count == 3)
        {
            StartShooting = true;
        }
    }

    private void CheckWin()
    {
        if (!isWinLine && !isStop)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
                CreateBlockUnderPlayer();
            }
        }
        else if (isWinLine && !isStop)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
    }

    private void CreateBullet()
    {
        if (Time.time > nextBulletTime)
        {
            nextBulletTime = Time.time + interval;
            var bullet = Instantiate(BulletOb, transform.position + new Vector3(0.8f, -0.1f, 0), transform.rotation);
        }
    }
    private void IdleDie()
    {
        isStop = true;
        animator.SetTrigger("IsDie");
        body.AddForce(Vector3.left * 4f, (ForceMode2D)ForceMode.Impulse);
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
            lastBlock = Instantiate(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity,transform);
        }
        else
        {
            // Nếu đã có block, tạo block mới ở dưới block trước đó
            lastBlock.transform.position = lastBlock.transform.position - new Vector3(0, 0.7f, 0);
            GameObject newBlock = Instantiate(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity,transform);
            lastBlock = newBlock; // Gán lastBlock là block vừa tạo
        }
    }
    bool isBarrierDown()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrier);
        return raycastHit2D.collider != null;
    }
}
