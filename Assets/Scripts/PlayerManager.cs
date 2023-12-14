using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private LayerMask barrier;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private GameObject lastBlock;
    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 5);
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
        // Đặt vận tốc Y để player nhảy lên
         body.velocity = new Vector2(body.velocity.x, 2f);
       // body.AddForce(new Vector3(0, 3, 0));
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
