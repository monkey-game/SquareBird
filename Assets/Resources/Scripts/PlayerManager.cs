﻿using System.Collections;
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
    private Rigidbody2D body;
    private GameObject lastBlock;
    private Animator animator;
    private bool isWinLine = false;
    private bool isStop = false;
    private int speed = 5;
    private float nextBulletTime;
    private bool StartShooting = false;
    public static bool IsReset = false;
    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            isStop = true;
            GameController.Instance.GameComplete();
            isStop = false;
        }     
        if (collision.gameObject.CompareTag("Trap"))
        {
            Vector3 blockPosition = transform.position;
            Vector3 obstaclePosition = collision.gameObject.transform.position;          
            if (blockPosition.y < obstaclePosition.y)
            {
                IdleDie();
            }
            else if((blockPosition.y - obstaclePosition.y) < 0.75f)
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
            StopBullet();
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            GameController.Instance.CountPerfect++;
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
                body.gravityScale = 4;
                eventDestroy?.Invoke();
            }
            if (GameController.Instance.CountPerfect >= 3)
            {
                StartShooting = true;
                StartCoroutine(StopCreateBullet());
            }        
        }
        if(IsReset)
        {
            animator.SetTrigger("IsReset");
            IsReset= false;
            isStop = false;
            isWinLine = false;
        }
    }

    private void CheckWin()
    {
        if (!isStop)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            if (Input.GetMouseButtonDown(0)&&!isWinLine)
            {
                Jump();
                CreateBlockUnderPlayer();
            }
        }
    }
    IEnumerator StopCreateBullet()
    {
        yield return new WaitForSeconds(5);
        StopBullet();
    }

    private void StopBullet()
    {
        StartShooting = false;
        GameController.Instance.CountPerfect = 0;
        GameController.Instance.ResetBird = true;
        body.gravityScale = 1;
    }

    IEnumerator WaitForDestroyBlock()
    {
        yield return new WaitForSeconds(2);
        eventDestroy?.Invoke();
        isWinLine = false;
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
        eventDestroy?.Invoke();
        int VaCham = Random.Range(2, 3);
        body.AddForce(Vector3.left * VaCham, (ForceMode2D)ForceMode.Impulse);
        GameController.Instance.GameOver();
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