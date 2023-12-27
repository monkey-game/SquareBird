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
    [SerializeField] private GameObject CanvasNext;
    public UnityEvent eventDestroy;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private GameObject lastBlock;
    private Animator animator;
    private bool isWinLine = false;
    private bool isStop = false;
    private int speed = 3;
    private float nextBulletTime;
    private bool StartShooting = false;
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
            CanvasNext.SetActive(true);
        }     
        if (collision.gameObject.CompareTag("Trap"))
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
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.CompareTag("WinLine"))
        {
            isWinLine = true;
            StartCoroutine(WaitForDestroyBlock());
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Perfect"))
        {
            GameController.CountPerfect++;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenu.isStartGame)
        {
            CheckWin();
            if (StartShooting)
            {
                CreateBullet();
                eventDestroy?.Invoke();
            }
            if (GameController.CountPerfect >= 3)
            {
                StartShooting = true;
                StartCoroutine(StopCreateBullet());
            }
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
    IEnumerator StopCreateBullet()
    {
        yield return new WaitForSeconds(5);
        StartShooting = false;
        GameController.CountPerfect = 0 ;
        GameController.ResetBird = true;
    }
    IEnumerator WaitForDestroyBlock()
    {
        yield return new WaitForSeconds(2);
        eventDestroy?.Invoke();
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
        int VaCham = Random.Range(2, 3);
        body.AddForce(Vector3.left * VaCham, (ForceMode2D)ForceMode.Impulse);
    }

    void Jump()
    {
         body.velocity = new Vector2(body.velocity.x, 2f);
    }
    void CreateBlockUnderPlayer()
    {
        if (lastBlock == null)
        {
            lastBlock = Instantiate(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity);
        }
        else
        {
            lastBlock.transform.position = lastBlock.transform.position - new Vector3(0, 0.7f, 0);
            GameObject newBlock = Instantiate(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity);
            lastBlock = newBlock;
        }
    }
}
