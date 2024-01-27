using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Pool;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject BlockPre;
    [SerializeField] private LayerMask barrier;
    [SerializeField] private GameObject winLine;
    [SerializeField] private GameObject BulletOb;
    [SerializeField] private float interval;
    private Rigidbody2D body;
    private GameObject lastBlock;
    private bool isWinLine = false;
    public bool isStop = false;
    private int speed = 5;
    private float nextBulletTime;
    private bool StartShooting = false;
    public static bool IsReset = false;
    public static bool IsRevive = false;
    private Transform TrapTrans;
    public AudioSource DeadAudio;
    public AudioSource[] eggdrop;
    private TrailRenderer trailRenderer;
    public bool ResetBlock;

    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
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
            if ((blockPosition.y < obstaclePosition.y)||((blockPosition.y - obstaclePosition.y) < 0.75f))
            {
                IdleDie();
                TrapTrans = collision.gameObject.transform;

            }else
            if(!isStop)
            ScoreManager.Instance.scoreNow += 10;
        }       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.CompareTag("WinLine"))
        {
            isWinLine = true;
            trailRenderer.enabled = false;
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
            }
            if (GameController.Instance.CountPerfect >= 3)
            {
                StartShooting = true;
                StartCoroutine(StopCreateBullet());
            }
            if (!trailRenderer.enabled)
                trailRenderer.colorGradient = SkinManager.instance.currentSkin.colorTrail;
            trailRenderer.enabled= true;        
        }
        if(IsReset)
        {
            IsReset= false;
            isStop = false;
            isWinLine = false;
        }
        if(IsRevive){
            IsRevive = false;
            transform.position = TrapTrans.position+ new Vector3(2,2,0);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            Debug.Log("run revive");
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
                if(!GameController.Instance.Mute)
                eggdrop[Random.Range(0,eggdrop.Length-1)].Play();
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
        ResetBlock = false;
        StartShooting = false;
        GameController.Instance.CountPerfect = 0;
        GameController.Instance.ResetBird = true;
        body.gravityScale = 1;
    }

    IEnumerator WaitForDestroyBlock()
    {
        yield return new WaitForSeconds(1);
        isWinLine = false;
        LeanPool.DespawnAll();
    }

    private void CreateBullet()
    {
        if (Time.time > nextBulletTime)
        {
            ResetBlock = true;
            nextBulletTime = Time.time + interval;
            var bullet = LeanPool.Spawn(BulletOb, transform.position + new Vector3(0.8f, -0.1f, 0), transform.rotation);
        }
    }
    private void IdleDie()
    {
        if (!GameController.Instance.Mute)
            DeadAudio.Play();
        isStop = true;
        AnimationDie();
        GameController.Instance.GameOver();
    }

    private void AnimationDie()
    {
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            transform.Rotate(new Vector3(0, 0f, Random.Range(90f, 180f)));
            body.AddForce(Vector3.left * Random.Range(2f, 3f), (ForceMode2D)ForceMode.Impulse);
        }
    }

    void Jump()
    {
         body.velocity = new Vector2(body.velocity.x, 2f);
    }
    void CreateBlockUnderPlayer()
    {
        if(!StartShooting){
        if (lastBlock == null)
        {
            lastBlock = LeanPool.Spawn(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity);
        }
        else
        {
            lastBlock.transform.position = lastBlock.transform.position - new Vector3(0, 0.7f, 0);
            GameObject newBlock = LeanPool.Spawn(BlockPre, transform.position - new Vector3(0.1f, 1, 0), Quaternion.identity);
            lastBlock = newBlock;
        }
        }
    }
}
